using AddOnCore;
using System;
using System.Globalization;

namespace AppSettingsConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AppSettings.ClearSettings();

            AppSettings.ConnectionString = "Some connectionString";
            AppSettings.LastObjectTypeId = 164;
            AppSettings.ShowSplash = true;
            AppSettings.LastActivatedRoundMethod = AddOnCore.Enums.RoundMethods.TwoDigits;

            Console.WriteLine(AppSettings.ConfigFileName);

            Console.ReadKey();
        }
    }
}
