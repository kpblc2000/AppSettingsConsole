using AddOnCore;
using AddOnCore.Enums;
using NUnit.Framework;
using System.Reflection;

namespace AppSettingsTests
{
    public class AppSettingsCheck
    {
        [OneTimeSetUp]
        public void Startup()
        {
            AppSettings.ClearSettings();
        }

        [Test]
        public void ConnectionString()
        {
            string connString = "Some connection string";
            AppSettings.ConnectionString = connString;
            Assert.AreEqual(connString, AppSettings.ConnectionString);
            connString = "Some another connection string";
            AppSettings.ConnectionString = connString;
            Assert.AreEqual(connString, AppSettings.ConnectionString);
        }

        [Test]
        public void LastObjectTypeId()
        {
            int id = 987;
            AppSettings.LastObjectTypeId = id;
            Assert.AreEqual(id, AppSettings.LastObjectTypeId);
            id = 123;
            AppSettings.LastObjectTypeId = id;
            Assert.AreEqual(id, AppSettings.LastObjectTypeId);
        }

        [Test]
        public void ShowSplash()
        {
            bool showSplash = false;
            AppSettings.ShowSplash = showSplash;
            Assert.AreEqual(showSplash, AppSettings.ShowSplash);
            showSplash = true;
            AppSettings.ShowSplash = showSplash;
            Assert.AreEqual(showSplash, AppSettings.ShowSplash);
        }

        [Test]
        public void LastActivatedRoundMethod()
        {
            RoundMethods method = RoundMethods.Stuff;
            AppSettings.LastActivatedRoundMethod = method;
            Assert.AreEqual(method, AppSettings.LastActivatedRoundMethod);
            method = RoundMethods.TwoDigits;
            AppSettings.LastActivatedRoundMethod = method;
            Assert.AreEqual(method, AppSettings.LastActivatedRoundMethod);
        }

        [Test]
        public void ConfigFileName()
        {
            Assert.AreEqual(AppSettings.ConfigFileName,
                Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "AppSettings.user.config"));
        }

        [Test]
        public void PrinterNameNoLocalization()
        {
            string printerName = "PrinterNoLocalization";
            CadEnvironment env = new CadEnvironment("Cad", "Verstion");
            AppSettings.SetDefaultPrinterName(env, printerName);
            Assert.AreEqual(AppSettings.GetDefaultPrinterName(env), printerName);
        }

        [Test]
        public void PrinterNameWithLocalization()
        {
            string printerName = "PrinterWithLocalization";
            CadEnvironment env = new CadEnvironment("Cad", "Verstion", "Rus");
            AppSettings.SetDefaultPrinterName(env, printerName);
            Assert.AreEqual(AppSettings.GetDefaultPrinterName(env), printerName);
        }
    }
}
