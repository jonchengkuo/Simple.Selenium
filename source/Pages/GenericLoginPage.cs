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
        public readonly CheckBox RememberMeCheckBox;
        public readonly Button LogInButton;

        public GenericLoginPage(
            By userNameTextFieldLocator,
            By passwordTextFieldLocator,
            By loginButtonLocator,
            By rememberMeCheckBoxLocator = null,
            IWebDriver webDriver = null)
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
            if (rememberMeCheckBoxLocator != null)
            {
                RememberMeCheckBox = new CheckBox(rememberMeCheckBoxLocator, webDriver);
            }

            // Use the visibility of the User Name text field to determine the availability of this page.
            IndicatingLocator = userNameTextFieldLocator;
        }

        public void LogInAs(string username, string password, bool rememberMe = false)
        {
            UserNameTextField.EnterText(username);
            PasswordTextField.EnterText(password);
            if (RememberMeCheckBox != null)
            {
                if (rememberMe)
                {
                    RememberMeCheckBox.Check();
                }
                else
                {
                    RememberMeCheckBox.Uncheck();
                }
            }
            LogInButton.Click();
        }
    }
}