using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using InoveTeste;
using InoveTeste.Page_Object;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Configuration;


namespace ST01Contato
{
    [TestFixture]
    public class CT02ValidarCamposObrigatorios
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
        public void TheCT02ValidarCamposObrigatoriosTest()
        {
            // Acessa o site
            driver.Navigate().GoToUrl(baseURL + "/contato");
            
            // Clica no botão Salvar sem preencher os campos obrigatórios
            driver.FindElement(By.CssSelector("input.wpcf7-form-control.wpcf7-submit")).Click();

            //Page Object
            // Inicialize a classe Contato com o driver
            Contato contato = new Contato(driver);
            contato.BuscarElementosDeErro();

            // Verifique se os elementos estão visíveis
            Assert.That(contato.requiredFieldError.Enabled, Is.True);
            Assert.That(contato.emailFieldError.Enabled, Is.True);
            Assert.That(contato.subjectFieldError.Enabled, Is.True);
            Assert.That(contato.messageFieldError.Enabled, Is.True);
            Assert.That(contato.formError.Enabled, Is.True);

            contato.ValidarMensagensDeErro();
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