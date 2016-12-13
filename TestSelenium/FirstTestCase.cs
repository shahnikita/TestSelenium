using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System.IO;
using System.Reflection;
namespace TestSelenium
{
    class FirstTestCase
    {
        static void Main(string[] args)
        {
            string appDirectory = System.IO.Path.GetDirectoryName(Environment.CurrentDirectory);
            var driverService = FirefoxDriverService.CreateDefaultService(appDirectory + @"\geckodriver-v0.11.1-win64", "geckodriver.exe");
            driverService.FirefoxBinaryPath = @"C:\Program Files (x86)\Mozilla Firefox\firefox.exe";

            driverService.HideCommandPromptWindow = true;
            driverService.SuppressInitialDiagnosticInformation = true;


            FirefoxOptions prof = new FirefoxOptions();

            var driver = new FirefoxDriver(driverService, prof, TimeSpan.FromSeconds(60));




            driver.Url = "https://wit.ai";

            var github_button = driver.FindElement(By.XPath(".//*[@id='hero']/div/div/div/div[1]/div[1]"));
            github_button.Click();

            driver.SwitchTo().Window(driver.WindowHandles.Last());

            #region login

            driver.Manage().Window.Maximize();
            System.Threading.Thread.Sleep(10000);
            Console.WriteLine("Child Window :-" + driver.Title.ToString());

            var sign_up_username_text = driver.FindElement(By.XPath(".//*[@id='login_field']"));
            sign_up_username_text.SendKeys("username");

            var sign_up_password_text = driver.FindElement(By.XPath(".//*[@id='password']"));
            sign_up_password_text.SendKeys("password");

            driver.FindElement(By.XPath(".//*[@id='login']/form/div[3]/input[3]")).Click();

            #endregion

            driver.SwitchTo().Window(driver.WindowHandles.First());

            

            #region goto Entities page
            System.Threading.Thread.Sleep(10000);
            driver.Url="https://wit.ai/shahnikita/Candidate_Experience/entities/JobTitle";
         
            #endregion

            IList<string> keywordList = GetJobTitle();
            #region Insert Keyword
            foreach (string key in keywordList)
            {
                System.Threading.Thread.Sleep(1000);
                
                driver.FindElement(By.XPath(".//*[@id='main-wrapper']/div/div[3]/div/div[2]/div/button")).Click();
                System.Threading.Thread.Sleep(1000);
                var insert_keyword_text = driver.FindElement(By.XPath(".//*[@id='main-wrapper']/div/div[3]/div/div[2]/div/div/div/div/div[2]/div/div/div/div"));
                
                insert_keyword_text.SendKeys(key.Trim());
                System.Threading.Thread.Sleep(10);
                insert_keyword_text.SendKeys(Keys.Enter);
                System.Threading.Thread.Sleep(100);
            }
            #endregion
        }


        public static IList<string> GetJobTitle()
        {

            var filePath = @"D:\Nikita\DemoProjects\TestSelenium\Filter\TitleKB.TitleField-typeaheads-AU.txt";
            var result = ReadFile(filePath).ToList();
            return result;
        }


        private static HashSet<string> ReadFile(string path)
        {
            StreamReader sr = new StreamReader(path);
            HashSet<string> lines = new HashSet<string>();
            string line;
            // Read and display lines from the file until 
            // the end of the file is reached. 
            while ((line = sr.ReadLine()) != null)
            {
                lines.Add(line);
            }
            return lines;
        }

    }
}
