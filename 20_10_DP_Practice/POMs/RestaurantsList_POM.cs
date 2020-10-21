using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _20_10_DP_Practice
{
    class RestaurantsList_POM
    {
        IWebDriver chrome;

        By _cousinSelector = By.CssSelector("body > div.container > div:nth-child(8) > div.contentbox2 > div:nth-child(4) > table > tbody > tr:nth-child(2) > td > select");
        By _searchTitle = By.CssSelector(".head1");
        By _firstListItem = By.XPath("/html/body/div[2]/div[7]/div[2]/center/div/table[3]/tbody/tr/td[1]/h3/a");
        By _dropDownPath = By.Name("select");


        public RestaurantsList_POM(IWebDriver driver)
        {
            this.chrome = driver;
        }
       
        public RestaurantsList_POM ChooseCousine(int number)
        {
            chrome.FindElement(_cousinSelector).Click();
            IWebElement dropDown = chrome.FindElement(_dropDownPath);
            SelectElement selectElement = new SelectElement(dropDown);
            selectElement.SelectByIndex(number);
            return this;
        }

        public RestaurantsList_POM GoToTheRestaurant()
        {
            chrome.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
            chrome.FindElement(_firstListItem).Click();
            return this;
        }

        public string LookForKey()
        {
            string searcPhrase = chrome.FindElement(_searchTitle).Text;
            return searcPhrase;
        }
    }
}
