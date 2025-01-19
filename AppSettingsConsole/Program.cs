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

            Console.WriteLine(AppSettings.ConfigFileName);

            Console.ReadKey();
        }
    }
}
