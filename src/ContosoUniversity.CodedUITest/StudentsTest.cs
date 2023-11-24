using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.ObjectModel;

namespace ContosoUniversity.CodedUITest
{
    [TestClass]
    public class StudentsTest
    {
        // https://techcommunity.microsoft.com/t5/testingspot-blog/continuous-testing-with-selenium-and-azure-devops/ba-p/3143366
        private string baseURL = "https://ase-contosouniversityapp-uat.azurewebsites.net/";
        private RemoteWebDriver driver;
        private string browser = string.Empty;

        public TestContext TestContext { get; set; }

        [TestMethod]
        [TestCategory("CodedUI")]
        [Priority(1)]
        public void StudentIndex()
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

                driver.FindElementById("link-students").Click();
                string resDep = driver.FindElementById("title").Text;
                Assert.AreEqual("Students", resDep);
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
        public void StudentEdit()
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

                driver.FindElementById("link-students").Click();
                string resDep = driver.FindElementById("title").Text;
                Assert.AreEqual("Students", resDep);

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
        public void StudentDetails()
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

                driver.FindElementById("link-students").Click();
                string resDep = driver.FindElementById("title").Text;
                Assert.AreEqual("Students", resDep);

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
        public void StudentCreate()
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

                driver.FindElementById("link-students").Click();
                string resDep = driver.FindElementById("title").Text;
                Assert.AreEqual("Students", resDep);

                // clica no link create new
                driver.FindElementById("link-create").Click();

                // input das informações
                driver.FindElement(By.Name("Student.LastName")).SendKeys("Prado");
                driver.FindElement(By.Name("Student.FirstName")).SendKeys("Leandro");

                // muda o tipo do campo para text para poder inserir o valor
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                string txt = (string)js.ExecuteScript("document.getElementsByName('Student.EnrollmentDate')[0].type=\"text\"");
                driver.FindElement(By.Name("Student.EnrollmentDate")).SendKeys("23/02/2023");

                // clica no botão create
                driver.FindElement(By.Id("btn-create")).Click();

                string resStu = driver.FindElementById("title").Text;
                Assert.AreEqual("Students", resStu);

                // localiza a tablea e recupera o body
                IWebElement body = driver.FindElementById("table-list").FindElement(By.TagName("tbody"));

                // Pega a todas as linhas da tabela
                ReadOnlyCollection<IWebElement> trElement = body.FindElements(By.TagName("tr"));

                // Pega a última linha da tabela
                ReadOnlyCollection<IWebElement> tdElement = trElement[trElement.Count - 1].FindElements(By.TagName("td"));

                // pega a primeira coluna da última linha, que é o LastName
                string ultimoLastName = tdElement[0].Text;

                // Compara os valores
                Assert.AreEqual("Prado", ultimoLastName);

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
