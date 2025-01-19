using AddOnCore.Enums;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;

namespace AddOnCore
{
    public static class AppSettings
    {

        /// <summary> Строка подключения к БД </summary>
        public static string ConnectionString
        {
            get => GetValue(nameof(ConnectionString));
            set => SetValue(nameof(ConnectionString), value);
        }

        /// <summary> Id последнего использованного типа объекта. Значение по умолчанию - 0 </summary>
        public static int LastObjectTypeId
        {
            get
            {
                if (int.TryParse(GetValue(nameof(LastObjectTypeId)), out var res))
                    return res;
                return 0;
            }
            set => SetValue(nameof(LastObjectTypeId), value.ToString());
        }

        /// <summary>Показывать или нет окно заставки. Значение по умолчанию - true</summary>
        public static bool ShowSplash
        {
            get
            {
                if (bool.TryParse(GetValue(nameof(ShowSplash)), out var res))
                    return res;
                return true;
            }
            set => SetValue(nameof(ShowSplash), value.ToString());
        }

        /// <summary>Получение имени плоттера по умолчанию</summary>
        /// <param name="Env"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static string GetDefaultPrinterName(CadEnvironment Env)
        {
            return GetValue(EvaluateSettingName(Env, _printerSettingName));
        }

        /// <summary>Назачение имени плоттера по умолчанию</summary>
        /// <param name="Env"></param>
        /// <param name="Value"></param>
        /// <exception cref="NotImplementedException"></exception>
        public static void SetDefaultPrinterName(CadEnvironment Env, string Value)
        {
            SetValue(EvaluateSettingName(Env, _printerSettingName), Value);
        }

        public static RoundMethods LastActivatedRoundMethod
        {
            get
            {
                if (Enum.TryParse(GetValue(nameof(LastActivatedRoundMethod)), out RoundMethods res))
                    return res;
                return RoundMethods.Unknown;
            }
            set => SetValue(nameof(LastActivatedRoundMethod), value.ToString());
        }

        /// <summary> Очищает настройки, удаляя файл с сохраненными настройками </summary>
        public static void ClearSettings()
        {
            if (File.Exists(_configFileName))
            {
                File.Delete(_configFileName);
            }
        }

        /// <summary> Возвращает полный путь к файлу с настройками </summary>
        public static string ConfigFileName => _configFileName;

        private static void SetValue(string key, string value)
        {
            if (!Directory.Exists(Path.GetDirectoryName(_configFileName)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(_configFileName));
            }

            XmlSerializer ser = new XmlSerializer(typeof(configuration));
            configuration conf = new configuration();

            if (!File.Exists(_configFileName))
            {
                conf.userSettings = new configurationSetting[]
                {
             new configurationSetting()
             {
                 name = key,
                 value = value,
             }
                };
                using (FileStream fs = new FileStream(_configFileName, FileMode.Create))
                {
                    ser.Serialize(fs, conf);
                }
            }
            else
            {
                using (FileStream fs = new FileStream(_configFileName, FileMode.Open))
                {
                    conf = ser.Deserialize(fs) as configuration;
                }

                configurationSetting item = conf.userSettings.FirstOrDefault(o => o.name == key);
                if (item == null)
                {
                    conf.userSettings = conf.userSettings.Append(new configurationSetting()
                    {
                        name = key,
                        value = value,
                    }).ToArray();
                }
                else
                {
                    item.value = value;
                }

                File.Delete(_configFileName);
                using (FileStream fs = new FileStream(_configFileName, FileMode.Create))
                {
                    ser.Serialize(fs, conf);
                }
            }
        }

        private static string GetValue(string key, string defaultValue = null)
        {
            if (!File.Exists(_configFileName))
            {
                return defaultValue;
            }

            XmlSerializer ser = new XmlSerializer(typeof(configuration));
            configuration conf = new configuration();
            using (FileStream fs = new FileStream(_configFileName, FileMode.Open))
            {
                conf = ser.Deserialize(fs) as configuration;
            }

            return conf.userSettings.FirstOrDefault(o => o.name == key)?.value ?? defaultValue;
        }

        /// <summary>Вычисление имени настройки, зависящей от окружения</summary>
        /// <param name="Env"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        private static string EvaluateSettingName(CadEnvironment Env, string Name)
        {
            string separator = ".";
            return Env.Name 
                + (string.IsNullOrWhiteSpace(Env.Version)? "" : (separator + Env.Version))
                + (string.IsNullOrWhiteSpace(Env.Localization) ? "" : (separator + Env.Localization))
                + Name;
        }

        private static readonly string _configFileName =
            Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), $"{nameof(AppSettings)}.user.config");
        private static readonly string _printerSettingName = "Printer";

        #region Seralization

        // Примечание. Для запуска созданного кода может потребоваться NET Framework версии 4.5 или более поздней версии и .NET Core или Standard версии 2.0 или более поздней.
        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
        public partial class configuration
        {

            private configurationSetting[] userSettingsField;

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("setting", IsNullable = false)]
            public configurationSetting[] userSettings
            {
                get
                {
                    return this.userSettingsField;
                }
                set
                {
                    this.userSettingsField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class configurationSetting
        {

            private string nameField;

            private string valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string name
            {
                get
                {
                    return this.nameField;
                }
                set
                {
                    this.nameField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }
        }


        #endregion
    }
}
