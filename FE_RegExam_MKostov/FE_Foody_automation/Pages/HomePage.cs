using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;

namespace FE_Foody_automation.Pages
{
    public class HomePage : BasePage
    {
        public HomePage(IWebDriver driver) : base(driver)
        {
            Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        }

        WebDriverWait Wait;

        public string Url = BaseUrl;

        public IWebElement SearchField => driver.FindElement(By.CssSelector("input[placeholder='Search']"));
        public IWebElement SearchButton => driver.FindElement(By.CssSelector("button[type='submit']"));

        public IWebElement FoodNotFoundMsg => driver.FindElement(By.CssSelector("h2.display-4"));
        public IWebElement AddFoodWhenNotFound_Button => driver.FindElement(By.CssSelector("a.btn.btn-primary.btn-xl.rounded-pill.mt-5"));

        public ReadOnlyCollection<IWebElement> FoodsAdded => this.Wait.Until(driver => 
                driver.FindElements(By.CssSelector(".row.gx-5.align-items-center")));

        public IWebElement LastFoodAddedContainer => FoodsAdded.Last();

        public IWebElement LastFoodAddedName => LastFoodAddedContainer.FindElement(By.CssSelector("h2.display-4"));
        public IWebElement LastFoodAddedDescr => LastFoodAddedContainer.FindElement(By.CssSelector("p.flex-lg-wrap"));

        public IWebElement LastFoodEditButton => LastFoodAddedContainer.FindElement(By.CssSelector("a[href^=\"/Food/Edit\"]"));
        public IWebElement LastFoodDeleteButton => LastFoodAddedContainer.FindElement(By.CssSelector("a[href^=\"/Food/Delete\"]"));


        public void OpenPage()
        { 
            driver.Navigate().GoToUrl(Url);
        }
    }
}
