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
    public class DepartmentsTest
    {
        private string baseURL = "https://ase-contosouniversityapp-uat.azurewebsites.net/";
        private RemoteWebDriver driver;
        private string browser = string.Empty;

        public TestContext TestContext { get; set; }

        [TestMethod]
        [TestCategory("CodedUI")]
        [Priority(1)]
        public void DepartamentIndex()
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

                driver.FindElementById("link-departments").Click();
                string resDep = driver.FindElementById("title").Text;
                Assert.AreEqual("Departments", resDep);
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

        [TestMethod]
        [TestCategory("CodedUI")]
        [Priority(1)]
        public void DepartamentEdit()
        {
            try
            {
                string driverPath = GetDriverPath();
                driver = new ChromeDriver(driverPath);
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
                driver.Navigate().GoToUrl(this.baseURL);

                //disable cookie alert
                //driver.FindElement(By.Id("btn-cookie")).Click();

                driver.FindElementById("link-departments").Click();
                string resDep = driver.FindElementById("title").Text;
                Assert.AreEqual("Departments", resDep);

                // localiza a tablea e recupera o body
                IWebElement body = driver.FindElementById("table-list").FindElement(By.TagName("tbody"));

                // Pega a primeira linha da tabela e clica no link
                body.FindElements(By.TagName("tr"))[0].FindElement(By.LinkText("Edit")).Click();

                //procura o titulo da pagina
                string title = driver.FindElementById("title").Text;
                Assert.AreEqual("Edit", title);

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

        [TestMethod]
        [TestCategory("CodedUI")]
        [Priority(1)]
        public void DepartamentDetails()
        {
            try
            {
                string driverPath = GetDriverPath();
                driver = new ChromeDriver(driverPath);
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
                driver.Navigate().GoToUrl(this.baseURL);

                //disable cookie alert
                //driver.FindElement(By.Id("btn-cookie")).Click();

                driver.FindElementById("link-departments").Click();
                string resDep = driver.FindElementById("title").Text;
                Assert.AreEqual("Departments", resDep);

                // localiza a tablea e recupera o body
                IWebElement body = driver.FindElementById("table-list").FindElement(By.TagName("tbody"));

                // Pega a primeira linha da tabela e clica no link
                body.FindElements(By.TagName("tr"))[0].FindElement(By.LinkText("Details")).Click();

                //procura o titulo da pagina
                string title = driver.FindElementById("title").Text;
                Assert.AreEqual("Details", title);

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


        [TestMethod]
        [TestCategory("CodedUI")]
        [Priority(1)]
        public void DepartamentCreate()
        {
            try
            {
                string driverPath = GetDriverPath();
                driver = new ChromeDriver(driverPath);
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
                driver.Navigate().GoToUrl(this.baseURL);

                //disable cookie alert
                //driver.FindElement(By.Id("btn-cookie")).Click();

                driver.FindElementById("link-departments").Click();
                string resDep = driver.FindElementById("title").Text;
                Assert.AreEqual("Departments", resDep);

                // clica no link create new
                driver.FindElementById("link-create").Click();

                // input das informações
                driver.FindElement(By.Name("Department.Name")).SendKeys("name");
                driver.FindElement(By.Name("Department.Budget")).SendKeys("5000");
                driver.FindElement(By.Name("Department.StartDate")).SendKeys("03/08/2018 03:20 AM");

                // seleciona o item zero da combo
                driver.FindElement(By.Name("Department.Instructor.ID")).FindElements(By.XPath("//option"))[1].Click();

                // clica no botão create
                driver.FindElement(By.Id("btn-create")).Click();
               
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
                if (driver != null)
                {
                    driver.Quit();
                    driver.Dispose();
                }

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
