using NUnit.Framework;
using OpenQA.Selenium;

namespace FE_Foody_automation.Pages
{
    public class AddFoodPage : BasePage
    {
        public AddFoodPage(IWebDriver driver) : base(driver)
        {   }

        public string Url = BaseUrl + "/Food/Add";

        public IWebElement FoodNameInput => driver.FindElement(By.Id("name"));
        public IWebElement FoodDescrInput => driver.FindElement(By.Id("description"));
        public IWebElement FoodImageInput => driver.FindElement(By.Id("url"));
        public IWebElement AddFoodButton => driver.FindElement(By.CssSelector(".btn-primary[type=\"submit\"]"));
        public IWebElement MainErrorMessage =>
           driver.FindElement(By.CssSelector("div.text-danger.validation-summary-errors li"));
        public IWebElement NameErrorMessage => 
            driver.FindElements(By.CssSelector("span.text-danger.field-validation-error"))[0];
        public IWebElement DescrErrorMessage =>
            driver.FindElements(By.CssSelector("span.text-danger.field-validation-error"))[1];

        public void CreateIdea(string name, string imageurl, string description)
        {
            FoodNameInput.SendKeys(name);
            FoodImageInput.SendKeys(imageurl);
            FoodDescrInput.SendKeys(description);
            AddFoodButton.Click();
        }

        public void AssertErrorMessages()
        {
            Assert.True(MainErrorMessage.Text.Equals("Unable to add this food revue!"), "Main error message is not as expected");

            Assert.True(NameErrorMessage.Text.Equals("The Name field is required."), "Food name error message is not as expected");

            Assert.True(DescrErrorMessage.Text.Equals("The Description field is required."), "Food description error message is not as expected");
        }

        public void OpenPage()
        {
            driver.Navigate().GoToUrl(Url);
        }
    }
}
