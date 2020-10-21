using _20_10_DP_Practice.POMs;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace _20_10_DP_Practice.Steps
{
    [Binding]
    public class WhereToGoModuleSteps
    {
        IWebDriver chrome;
        Restaurant_POM rest_POM;
        RestaurantPage_POM retPage_POM;
        RestaurantsList_POM restList_POM;
        KydaPoyti_POM k_POM;
        List<string> titles;


        [Given(@"Site Gorod\.dp is open on restaurant page")]
        public void GivenSiteGorod_DpIsOpenOnRestaurantPage()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("--lang=ru");
            chrome = new ChromeDriver(@"D:\Selenium\chromedriver_win32", options);
            chrome.Navigate().GoToUrl("https://gorod.dp.ua/restoran/");
            chrome.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
        }

        [When(@"put keyword into search input field")]
        public void WhenPutKeywordIntoSearchInputField()
        {
            rest_POM = new Restaurant_POM(chrome);
            string searchPhrase = "кафе";
            rest_POM.FillSearchInput(searchPhrase).PressSearchBtn();
        }

        [Then(@"search page has looked title")]
        public void ThenSearchPageHasLookedTitle()
        {
            string searched = rest_POM.LookForKey();
            Assert.AreEqual("Вы ищете:  кафе", searched);
            chrome.Quit();
        }
        
        
        // Cusin choosing check
        [Given(@"Web-site open on restaurants page")]
        public void GivenWeb_SiteOpenOnRestaurantsPage()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("--lang=ru");
            chrome = new ChromeDriver(@"D:\Selenium\chromedriver_win32", options);
            chrome.Navigate().GoToUrl("https://gorod.dp.ua/restoran/catalog/?place_type=2");
            chrome.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
        }


        [When(@"I choose cusine")]
        public void WhenIChooseCusine()
        {
            restList_POM = new RestaurantsList_POM(chrome);
            restList_POM.ChooseCousine(5);
        }

        [Then(@"I see list of restaurant for this cusin")]
        public void ThenISeeListOfRestaurantForThisCusin()
        {
            restList_POM = new RestaurantsList_POM(chrome);
            string searched = restList_POM.LookForKey();
            Assert.AreEqual("Вы ищете:  Арабская", searched);
        }

        [When(@"I navigate to page of restaurant")]
        public void WhenINavigateToPageOfRestaurant()
        {
            restList_POM.GoToTheRestaurant();
        }

        [Then(@"I see cousin that I was choosing")]
        public void ThenISeeCousinThatIWasChoosing()
        {
            retPage_POM = new RestaurantPage_POM(chrome);
            string cusine = retPage_POM.ReadCusine();
            Assert.AreEqual("Арабская", cusine);
            chrome.Quit();
        }

        // Cheking correctness of navigation to restaurants from where to go 
        [Given(@"Site Gorod\.dp is open on where to go")]
        public void GivenSiteGorod_DpIsOpenOnWhereToGo()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("--lang=ru");
            chrome = new ChromeDriver(@"D:\Selenium\chromedriver_win32", options);
            chrome.Navigate().GoToUrl("https://gorod.dp.ua/out/");
            chrome.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
        }


        [When(@"go to page restorans")]
        public void WhenGoToPageRestorans()
        {
            KydaPoyti_POM k_POM = new KydaPoyti_POM(chrome);
            k_POM.ClickButtonRestoran();
        }

        [Then(@"went to the page restorans")]
        public void ThenWentToThePageRestorans()
        {
            Assert.AreEqual("Рестораны | Городской сайт Днепра", chrome.Title);
            chrome.Quit();
        }

        // Checking correctness of navigation on page
        [When(@"I click on the restaurants tab in the sidebar")]
        public void WhenIClickOnTheRestaurantsTabInTheSidebar()
        {
            k_POM = new KydaPoyti_POM(chrome);
            k_POM.ClickButtonRestoran();
            titles = new List<string>();
            titles.Add(chrome.Title);
        }

        [When(@"Back to the page where to go")]
        public void WhenBackToThePageWhereToGo()
        {
            k_POM.ClickHeaderKydaPoyti();
        }

        [When(@"I click on the restaurants tab in the body")]
        public void WhenIClickOnTheRestaurantsTabInTheBody()
        {
            k_POM.ClickButtonRestoranBody();
            titles.Add(chrome.Title);
        }

        [Then(@"Title of page that opens from side bar the same as title of page opened from body")]
        public void ThenTitleOfPageThatOpensFromSideBarTheSameAsTitleOfPageOpenedFromBody()
        {
            Assert.AreEqual(titles[0], titles[1]);
            chrome.Quit();
        }
    }
}
