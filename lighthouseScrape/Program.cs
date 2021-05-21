using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace lighthouseScrape
{
    class Program
    {
        static void Main(string[] args)
        {
            FirefoxOptions options = new FirefoxOptions();
            options.AddArgument("--headless");
            options.AcceptInsecureCertificates = true;
            options.AddArgument("ignore-certificate-errors");
            Proxy proxy = new Proxy();
            proxy.Kind = ProxyKind.Manual;
            proxy.IsAutoDetect = false;
            proxy.SslProxy = "localhost:8080";
            options.Proxy = proxy;
            options.Profile = new FirefoxProfile();
            options.Profile.SetPreference("devtools.netmonitor.har.enableAutoExportToFile", true);
            options.Profile.SetPreference("devtools.netmonitor.har.defaultLogDir", @"C:\Users\iansu\Desktop");
            options.Profile.SetPreference("devtools.netmonitor.har.defaultFileName", "network-log-file-%Y-%m-%d-%H-%M-%S");

            try
            {
                CodePagesEncodingProvider.Instance.GetEncoding(437);
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                IWebDriver driver = new FirefoxDriver(@"D:\geckodriver-v0.27.0-win64", options);
                driver.Navigate().GoToUrl(@"https://www.selenium.dev/documentation/en/webdriver/http_proxies/");
                Actions keyAction = new Actions(driver);
                keyAction.KeyDown(Keys.LeftControl).KeyDown(Keys.LeftShift).SendKeys("q").KeyUp(Keys.LeftControl).KeyUp(Keys.LeftShift).Perform();
                string url = driver.Url;
                string source = driver.PageSource;

                Console.WriteLine("Reached site");

                driver.Quit();
            }
            catch(Exception exception){
                Console.WriteLine($"{exception.Message}\n{exception.InnerException.Message}");
            }

            Console.ReadLine();
        }
    }
}
