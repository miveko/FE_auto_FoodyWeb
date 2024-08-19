using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace FE_Foody_automation.Pages
{
    public class BasePage
    {
        protected IWebDriver driver;
        protected WebDriverWait wait;
        protected static readonly string BaseUrl = "http://softuni-qa-loadbalancer-2137572849.eu-north-1.elb.amazonaws.com:85";

        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        }

        public IWebElement HomeLink =>
           driver.FindElement(By.ClassName("navbar-brand"));

        public IWebElement MyProfileLink =>
           driver.FindElement(By.CssSelector("a.nav-link[href=\"/Profile\"]"));

        public IWebElement AddFoodLink =>
           driver.FindElement(By.CssSelector("a.nav-link[href=\"/Food/Add\"]"));

        public IWebElement LogoutLink =>
           driver.FindElement(By.CssSelector("a.nav-link[href=\"/User/Logout\"]"));

        public IWebElement LoginLink =>
           driver.FindElement(By.CssSelector("a.nav-link[href=\"/User/Login\"]"));

        public IWebElement SignUpLink =>
           driver.FindElement(By.CssSelector("a.nav-link[href=\"/User/Register\"] "));
    }
}
