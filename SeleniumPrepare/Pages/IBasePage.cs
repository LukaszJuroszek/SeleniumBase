using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Pages
{
    public interface IBasePage
    {
        string BaseUrl { get; }
        IWebDriver Driver { get; set; }
        WebDriverWait Wait { get; set; }

        void Click(IWebElement element);
        IWebElement FindElement(By locator);
        string GetTitle();
        void NavigateToUrl(string url);
        void WaitForPageToLoad();
        void WaitUntilElementIsVisible(By locator);
    }
}
