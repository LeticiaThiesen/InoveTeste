using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;

namespace InoveTeste.Page_Object
{
    internal class Contato
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        // Construtor que recebe o WebDriver
        public Contato(IWebDriver driver)
        {
            this.driver = driver;
            this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            // Inicialize os elementos manualmente e espera até estarem visíveis
            name = wait.Until(d => d.FindElement(By.Name("your-name")));
            email = wait.Until(d => d.FindElement(By.Name("your-email")));
            subject = wait.Until(d => d.FindElement(By.Name("your-subject")));
            massage = wait.Until(d => d.FindElement(By.Name("your-message")));
            enviar = wait.Until(d => d.FindElement(By.CssSelector("input.wpcf7-form-control.wpcf7-submit")));
        }

        public void BuscarElementosDeErro()
        {
            //this.driver = driver;
            this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            requiredFieldError = wait.Until(d => d.FindElement(By.CssSelector("span.wpcf7-not-valid-tip")));
            emailFieldError = wait.Until(d => d.FindElement(By.CssSelector("span.wpcf7-form-control-wrap.your-email > span.wpcf7-not-valid-tip")));
            messageFieldError = wait.Until(d => d.FindElement(By.CssSelector("span.wpcf7-form-control-wrap.your-message > span.wpcf7-not-valid-tip")));
            subjectFieldError = wait.Until(d => d.FindElement(By.CssSelector("span.wpcf7-form-control-wrap.your-subject > span.wpcf7-not-valid-tip")));
            formError = wait.Until(d => d.FindElement(By.XPath("//div[@id='wpcf7-f372-p24-o1']/form/div[2]")));
        }

        public void PreencherFormulario(string nome, string email, string assunto, string mensagem)
        {
            name.SendKeys(nome);
            this.email.SendKeys(email);
            subject.SendKeys(assunto);
            massage.SendKeys(mensagem);
        }

        public IWebElement name { get; private set; }
        public IWebElement email { get; private set; }
        public IWebElement subject { get; private set; }
        public IWebElement massage { get; private set; }
        public IWebElement enviar { get; private set; }

        // Elementos de erro
        public IWebElement requiredFieldError { get; private set; }
        public IWebElement emailFieldError { get; private set; }
        public IWebElement messageFieldError { get; private set; }
        public IWebElement subjectFieldError { get; private set; }
        public IWebElement formError { get; private set; }

        public void ValidarMensagensDeErro()
        {
            wait.Until(d => requiredFieldError.Displayed);
            wait.Until(d => emailFieldError.Displayed);
            wait.Until(d => messageFieldError.Displayed);
            wait.Until(d => subjectFieldError.Displayed);
            wait.Until(d => formError.Displayed);

            Assert.That("O campo é obrigatório.", Is.EqualTo(requiredFieldError.Text));
            Assert.That("O campo é obrigatório.", Is.EqualTo(emailFieldError.Text));
            Assert.That("O campo é obrigatório.", Is.EqualTo(messageFieldError.Text));
            Assert.That("O campo é obrigatório.", Is.EqualTo(subjectFieldError.Text));
            Assert.That("Um ou mais campos possuem um erro. Verifique e tente novamente.", Is.EqualTo(formError.Text));
        }

        public void ValidarMensagensDeSucesso()
        {
            By successMessageLocator = By.XPath("//div[@id='wpcf7-f372-p24-o1']/form/div[2]");
            wait.Until(d => d.FindElement(successMessageLocator).Displayed);

            // Valida a mensagem de sucesso
            string actualMessage = driver.FindElement(successMessageLocator).Text;
            string expectedMessage = "Agradecemos a sua mensagem. Responderemos em breve.";

            Assert.That(actualMessage, Is.EqualTo(expectedMessage));
        }
    }
}