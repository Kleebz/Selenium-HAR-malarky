using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Remote;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace lighthouseScrape
{
    class Program
    {
        private static readonly string ExtensionPath = @""; //Path to browser extension which is included in cloned files, EX: C:\har_export_trigger-0.6.1-an+fx.xpi
        private static readonly string LogDir = @""; //Set Logging Dir, EX: C:\LoggingDir

        static void Main(string[] args)
        {
            FirefoxOptions options = new FirefoxOptions();
            //options.AddArgument("--headless");
            options.AcceptInsecureCertificates = true;
            options.AddArgument("ignore-certificate-errors");
            options.AddArguments("--devtools", "");

            options.Profile = new FirefoxProfile();
            options.Profile.AddExtension(ExtensionPath);

            options.SetPreference("extensions.firebug.allPagesActivation", "on");
            options.SetPreference("devtools.netmonitor.enabled", true);
            options.SetPreference("extensions.netmonitor.har.enableAutomation", true);
            options.SetPreference("extensions.netmonitor.har.contentAPIToken", "test");
            options.SetPreference("extensions.netmonitor.har.autoConnect", true);
            options.SetPreference("devtools.netmonitor.har.compress", false);
            options.SetPreference("devtools.netmonitor.har.defaultFileName", "Autoexport_%y%m%d_%H%M%S");
            options.SetPreference("devtools.netmonitor.har.defaultLogDir", LogDir);
            options.SetPreference("devtools.netmonitor.har.enableAutoExportToFile", true);//If you set to false, it wont create multiple files. ToDo: Look into exportingt a single output instead of page by page.
            options.SetPreference("devtools.netmonitor.har.forceExport", true);
            options.SetPreference("devtools.netmonitor.har.includeResponseBodies", true);
            options.SetPreference("devtools.netmonitor.har.jsonp", false);
            options.SetPreference("devtools.netmonitor.har.jsonpCallback", false);
            options.SetPreference("devtools.netmonitor.har.pageLoadedTimeout", "10000");

            try
            {
                CodePagesEncodingProvider.Instance.GetEncoding(437);
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                FirefoxDriver driver = new FirefoxDriver(options);

                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                driver.Navigate().GoToUrl("https://homestarrunner.com/toons#Teen%20Girl%20Squad");

                System.Threading.Thread.Sleep(10000);

                driver.Close();
                driver.Quit();
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{exception.Message}\n{exception.InnerException.Message}");
            }
        }
    }
}
