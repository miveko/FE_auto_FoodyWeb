using NUnit.Framework;
using FE_Foody_automation.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace FE_Foody_automation.Tests
{
    public class BaseTest
    {
        private string username = "MAINBRAIN";
        private string password = "Abc54321";
        public IWebDriver driver;
        public static string lastAddedFoodName;
        public static string LastAddedFoodDescr;
        public LoginPage loginPage;
        public AddFoodPage addFoodPage;
        public HomePage homePage;
        public EditFootPage editFoodPage;

        ChromeOptions chromeOptions;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            chromeOptions = new ChromeOptions();
            chromeOptions.AddUserProfilePreference("profile.password_manager_enabled", false);
            chromeOptions.AddArgument("--disable-search-engine-choice-screen");
            driver = new ChromeDriver(chromeOptions);
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            loginPage = new LoginPage(driver);
            addFoodPage = new AddFoodPage(driver);
            homePage = new HomePage(driver);
            editFoodPage = new EditFootPage(driver);
        }

        [SetUp]
        public void Login()
        {
            // Log in to the application
            loginPage.OpenPage();
            loginPage.Login(username, password);
        }

        [TearDown]
        public void TearDown()
        {
            homePage.LogoutLink.Click();
            if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                TakeScreenshot(TestContext.CurrentContext.Test.Name);
            }
            //driver.Quit();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            driver?.Quit();
            driver?.Dispose();
        }

        public string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private void TakeScreenshot(string testName)
        {
            try
            {
                Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
                string screenshotName = $"{testName}_{timestamp}.png";
                string filePath = Path.Combine(TestContext.CurrentContext.WorkDirectory, screenshotName);
                screenshot.SaveAsFile(filePath);
                Console.WriteLine($"Screenshot saved as {filePath}");
            }
            catch (WebDriverException e)
            {
                Console.WriteLine($"Failed to take screenshot: {e}");
            }
        }
    }
}
