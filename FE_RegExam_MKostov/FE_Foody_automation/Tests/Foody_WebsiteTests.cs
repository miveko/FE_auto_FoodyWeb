using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using FE_Foody_automation.Pages;

namespace FE_Foody_automation.Tests
{
    public class Foody_WebsiteTests : BaseTest
    {
        private static string lastAddedFoodName;
        private static string lastAddedFoodDescr;

        [Test, Order(1)]
        public void AddFoodwithInvalidDataTest()
        {
            addFoodPage.OpenPage();
            addFoodPage.CreateIdea("", "", "");
            addFoodPage.AssertErrorMessages();
            Assert.AreEqual(addFoodPage.Url, driver.Url);
        }

        [Test, Order(2)]
        public void AddRandomFoodTest()
        {
            lastAddedFoodName = "FoodName_" + GenerateRandomString(5);
            lastAddedFoodDescr = "RandomDescription_" + GenerateRandomString(20);

            addFoodPage.OpenPage();

            addFoodPage.CreateIdea(lastAddedFoodName, "http://www.pictures.com/picture.jpg", lastAddedFoodDescr);

            Assert.That(driver.Url, Is.EqualTo(homePage.Url + "/"), "User is not redirected to Home page as expected!");

            string errorMsg = "The newly added food was not found or is incorrectly listed.";

            Assert.That(homePage.LastFoodAddedName.Text.Trim(), Is.EqualTo(lastAddedFoodName), errorMsg);
            
            Assert.That(homePage.LastFoodAddedDescr.Text.Trim(), Is.EqualTo(lastAddedFoodDescr), errorMsg);
        }


        [Test, Order(3)]
        public void EditLastAddedFoodTest()
        {
            homePage.OpenPage();

            Assert.IsTrue(homePage.FoodsAdded.Count > 0, "No idea cards were found on the page.");

            new Actions(driver)
                .ScrollToElement(homePage.LastFoodEditButton)
                .Perform();


            if (string.IsNullOrEmpty(lastAddedFoodName))
                lastAddedFoodName = homePage.LastFoodAddedName.Text;

            homePage.LastFoodEditButton.Click();

            string newTitle = "Changed Title: " + lastAddedFoodName;
            editFoodPage.FoodNameInput.Clear();
            editFoodPage.FoodNameInput.SendKeys(newTitle);
            editFoodPage.EditFoodButton.Click();

            homePage.OpenPage();

            Assert.That(homePage.LastFoodAddedName.Text.Trim(), Is.EqualTo(lastAddedFoodName), 
                "The food name remains the same after the edit!!!!");
            Console.WriteLine("Edit functionality doesn't work");
        }

        [Test, Order(4)]
        public void SearchForFoodTitleTest()
        {
            if (string.IsNullOrEmpty(lastAddedFoodName))
                lastAddedFoodName = homePage.LastFoodAddedName.Text;

            homePage.SearchField.SendKeys(lastAddedFoodName);
            homePage.SearchButton.Click();
            Thread.Sleep(3000);
            Assert.AreEqual(1, homePage.FoodsAdded.Count, "Expected one food found, Actual: " + homePage.FoodsAdded.Count);
            Assert.IsTrue(homePage.LastFoodAddedName.Text.Equals(lastAddedFoodName));
        }

        [Test, Order(5)]
        public void DeleteLastIdeaTest()
        {
            Assert.IsTrue(homePage.FoodsAdded.Count > 0, "No idea cards were found on the page.");

            new Actions(driver)
                .ScrollToElement(homePage.LastFoodDeleteButton)
                .Perform();

            if(string.IsNullOrEmpty(lastAddedFoodName))
                lastAddedFoodName = homePage.LastFoodAddedName.Text;

            homePage.LastFoodDeleteButton.Click();

            bool isIdeaDeleted = homePage.FoodsAdded.All(card => !card.Text.Contains(lastAddedFoodName));

            Assert.IsTrue(isIdeaDeleted, "The idea was not deleted successfully or is still visible in the list.");
        }


        [Test, Order(6)]
        public void SearchforDeletedFoodTest()
        {
            homePage.SearchField.SendKeys(lastAddedFoodName);
            homePage.SearchButton.Click();
            Thread.Sleep(3000);
            Assert.AreEqual("There are no foods :(", homePage.FoodNotFoundMsg.Text, "Error message Not found");
            Assert.AreEqual("ADD FOOD", homePage.AddFoodWhenNotFound_Button.Text);
        }
    }
}