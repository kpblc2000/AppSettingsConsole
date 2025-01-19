using AddOnCore;
using System;

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

            CadEnvironment env = new CadEnvironment("CadName", "CadVersion");
            AppSettings.SetDefaultPrinterName(env, "some plotter");

            Console.WriteLine(AppSettings.ConfigFileName);

            Console.ReadKey();
        }
    }
}
