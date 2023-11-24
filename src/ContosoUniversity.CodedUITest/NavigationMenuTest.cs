using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using System;

namespace ContosoUniversity.CodedUITest
{
    [TestClass]
    public class NavigationMenuTest
    {
        private string baseURL = "https://ase-contosouniversityapp-uat.azurewebsites.net/";
        private RemoteWebDriver driver;
        private string browser = string.Empty;

        public TestContext TestContext { get; set; }

        [TestMethod]
        [TestCategory("CodedUI")]
        [Priority(1)]
        public void MenuNavigate()
        {
            try
            {
                string driverPath = GetDriverPath();
                driver = new ChromeDriver(driverPath);

                driver.Manage().Window.Maximize();
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
                driver.Navigate().GoToUrl(this.baseURL);

                //disable cookie alert
                //driver.FindElement(By.Id("btn-cookie")).Click();

                driver.FindElement(By.Id("link-home")).Click();
                string resHome = driver.FindElementById("text-welcome").Text;
                Assert.AreEqual("Contoso University", resHome);

                driver.FindElementById("link-about").Click();
                string resAbout = driver.FindElementById("title").Text;
                Assert.AreEqual("About", resAbout);

                driver.FindElementById("link-departments").Click();
                string resDep = driver.FindElementById("title").Text;
                Assert.AreEqual("Departments", resDep);

                driver.FindElementById("link-courses").Click();
                string resCou = driver.FindElementById("title").Text;
                Assert.AreEqual("Courses", resCou);

                driver.FindElementById("link-instructors").Click();
                string resIns = driver.FindElementById("title").Text;
                Assert.AreEqual("Instructors", resIns);

                driver.FindElementById("link-students").Click();
                string resStu = driver.FindElementById("title").Text;
                Assert.AreEqual("Students", resStu);

                driver.FindElementById("link-contact").Click();
                string resCon = driver.FindElementById("title").Text;
                Assert.AreEqual("Contact", resCon);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
            finally
            {
                if (driver != null)
                {
                    driver.Quit();
                    driver.Dispose();
                }
            }
        }


        [TestInitialize()]
        public void MyTestInitialize()
        {
            //if (this.TestContext.Properties["Url"] != null) //Set URL from a build
            //{
            //    this.baseURL = this.TestContext.Properties["Url"].ToString();
            //}   
        }

        [TestCleanup()]
        public void MyTestCleanup()
        {
            try
            {
                driver.Quit();
                driver.Dispose();
            }
            catch (Exception)
            {
            }
        }

        private string GetDriverPath()
        {
            try
            {
                string res = Environment.GetEnvironmentVariable("ChromeWebDriver");
                return res;
            }
            catch
            {
                throw new Exception("Variável de ambiente ChromeWebDriver não encontrado");
            }
        }

    }

    
}