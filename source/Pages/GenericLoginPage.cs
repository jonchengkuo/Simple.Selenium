using System;
using OpenQA.Selenium;
using Simple.Selenium.Controls;
using Simple.Selenium.Pages;

namespace Simple.Selenium.Pages
{
    public class GenericLoginPage : BasePage<GenericLoginPage>
    {
        public readonly TextField UserNameTextField;
        public readonly TextField PasswordTextField;
        public readonly Button LogInButton;

        /// <summary>
        /// A generic Page Object class for a typical login page that contains 3 components:
        /// 1) a user name text field, 2) a password text field, and 3) a log-in button.
        /// A login Page Object of this class is specified using the element locators of these 3 components.
        /// It uses the visibility of the user name text field to determine the availability of this page.
        /// </summary>
        /// <param name="userNameTextFieldLocator"></param>
        /// <param name="passwordTextFieldLocator"></param>
        /// <param name="loginButtonLocator"></param>
        /// <param name="webDriver"></param>
        public GenericLoginPage(
            By userNameTextFieldLocator,
            By passwordTextFieldLocator,
            By loginButtonLocator,
            IWebDriver webDriver = null)
            : base(userNameTextFieldLocator, webDriver)
        {
            if (userNameTextFieldLocator == null)
            {
                throw new ArgumentNullException("The userNameTextFieldLocator given to the LoginPage constructor is null.");
            }
            if (passwordTextFieldLocator == null)
            {
                throw new ArgumentNullException("The passwordTextFieldLocator given to the LoginPage constructor is null.");
            }
            if (loginButtonLocator == null)
            {
                throw new ArgumentNullException("The loginButtonLocator given to the LoginPage constructor is null.");
            }

            UserNameTextField = new TextField(userNameTextFieldLocator, webDriver);
            PasswordTextField = new TextField(passwordTextFieldLocator, webDriver);
            LogInButton = new Button(loginButtonLocator, webDriver);
        }

        /// <summary>
        /// Simulates the Web UI interactions of logging in to the login page.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public void LogInAs(string username, string password)
        {
            UserNameTextField.EnterText(username);
            PasswordTextField.EnterText(password);
            LogInButton.Click();
        }
    }
}