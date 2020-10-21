
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace _20_10_DP_Practice
{
    class Hooks
    {
        [AfterScenario]
        public void BrowserClose()
        {
            //chrome.Quit;
        }
    }
}
