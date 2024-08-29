using System;
using System.Configuration;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using InoveTeste.Page_Object;
using OpenQA.Selenium.Support.PageObjects;
using InoveTeste;

namespace ST01Contato
{
    [TestFixture]
    public class CT01ValidarLayoutTela
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;
        
        [SetUp]
        public void SetupTest()
        {
            driver = Comandos.GetBrowserLocal(driver, ConfigurationManager.AppSettings["browser"]);
            baseURL = "https://livros.inoveteste.com.br/";
            verificationErrors = new StringBuilder();
        }
        
        [TearDown]
        public void TeardownTest()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.That("", Is.EqualTo(verificationErrors.ToString()));
        }
        
        [Test]
        public void TheCT01ValidarLayoutTelaTest()
        {
            // Acessa o site 
            driver.Navigate().GoToUrl(baseURL + "/contato");

            // Valida o layout da tela
            Assert.That("Envie uma mensagem", Is.EqualTo(driver.FindElement(By.CssSelector("h1")).Text));

            //Page Object
            // Inicialize a classe Contato com o driver
            Contato contato = new Contato(driver);

            // Verifique se os elementos estão visíveis
            Assert.That(contato.name.Enabled, Is.True);
            Assert.That(contato.email.Enabled, Is.True);
            Assert.That(contato.subject.Enabled, Is.True);
            Assert.That(contato.massage.Enabled, Is.True);
            Assert.That(contato.enviar.Enabled, Is.True);
        }
        private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
        private bool IsAlertPresent()
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }        
        private string CloseAlertAndGetItsText() {
            try {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert) {
                    alert.Accept();
                } else {
                    alert.Dismiss();
                }
                return alertText;
            } finally {
                acceptNextAlert = true;
            }
        }
    }
}
