using OpenQA.Selenium;

namespace FE_Foody_automation.Pages
{
    public class EditFootPage : BasePage
    {
        public EditFootPage(IWebDriver driver) : base(driver)
        {   }

        public string Url = BaseUrl + "/Food/Edit";

        public IWebElement FoodNameInput => driver.FindElement(By.Id("name"));
        public IWebElement FoodDescrInput => driver.FindElement(By.Id("description"));
        public IWebElement FoodImageInput => driver.FindElement(By.Id("url"));
        public IWebElement EditFoodButton => driver.FindElement(By.CssSelector(".btn-primary[type=\"submit\"]"));
    }
}
