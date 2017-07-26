using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pages
{
    public abstract class BasePage : IDisposable, IBasePage
    {
        private readonly string _baseUrl;
        private WebDriverWait _wait;
        private IWebDriver _driver;

        public IWebDriver Driver { get { return _driver; } set { _driver = value; } }

        public string BaseUrl { get { return _baseUrl; } }

        public WebDriverWait Wait { get { return _wait; } set { _wait = value; } }

        public string MainWindowHander { get; set; }

        public BasePage(string baseUrl, IWebDriver driver, int timeToWait = 4)
        {
            _baseUrl = baseUrl;
            Driver = driver;
            InitWebDriverWait(timeToWait);
            InitWindow();
            MainWindowHander = Driver.CurrentWindowHandle;
        }

        private void InitWindow()
        {
            Driver.Manage().Window.Maximize();
        }

        private void InitWebDriverWait(int timeToWait)
        {
            if (timeToWait <= 0)
                throw new ArgumentException("timeToWait should be greater than 0");
            Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeToWait))
            {
                PollingInterval = TimeSpan.FromMilliseconds(100)
            };
        }

        public void Click(IWebElement element)
        {
            element.Click();
            WaitForPageToLoad();
        }

        public void NavigateToMainPage()
        {
            Driver.Navigate().GoToUrl(BaseUrl);
            WaitForPageToLoad();
        }

        public void NavigateToUrl(string url)
        {
            Driver.Navigate().GoToUrl(url);
            WaitForPageToLoad();
        }

        public void WaitForPageToLoad()
        {
            Wait.Until((d) => (Driver as IJavaScriptExecutor).ExecuteScript("return document.readyState").Equals("complete"));
        }

        public void WaitUntilElementIsVisible(By locator)
        {
            Wait.Until(ExpectedConditions.ElementIsVisible(locator));
        }

        public IWebElement FindElement(By locator)
        {
            WaitUntilElementIsVisible(locator);
            return Driver.FindElement(locator);
        }

        public void WtriteText(IWebElement element, string text)
        {
            if (text == null || text == string.Empty)
                throw new ArgumentException("text should be not null and empty");
            element.SendKeys(text);
        }

        public void TakeScreanShot()
        {
            Screenshot ss = ((ITakesScreenshot)Driver).GetScreenshot();
            var path = $"{AppDomain.CurrentDomain.BaseDirectory}{Guid.NewGuid()}.png";
            ss.SaveAsFile(path, ScreenshotImageFormat.Png);
        }

        public string GetTitle() => Driver.Title;

        public bool AreAllCheckboxesSelected(IList<IWebElement> elementList, int expected)
        {
            if (expected < 0)
                throw new ArgumentException("expected should be greater or than 0");
            var actual = 0;
            for (int i = 0; i < elementList.Count; i++)
            {
                actual += (elementList[i].Selected == true ? 1 : 0);
            }
            return actual == expected;
        }

        public void SelectElementByText(SelectElement selectedElement, string text)
        {
            if (text == null || text == string.Empty)
                throw new ArgumentException("text should be not null and empty");
            selectedElement.SelectByText(text);
        }

        public void SelectElementByValue(SelectElement selectedElement, string value)
        {
            selectedElement.SelectByValue(value);
        }

        public void SelectElementByText(IWebElement element, string text)
        {
            SelectElementByText(new SelectElement(element), text);
        }

        public void SelectElementByValue(IWebElement element, string value)
        {
            SelectElementByValue(new SelectElement(element), value);
        }

        public void SelectElementByIndex(SelectElement selectedElement, int index)
        {
            if (index < 0)
                throw new ArgumentException("index should be greater than 0");
            selectedElement.SelectByIndex(index);
        }

        public void SelectElementByIndex(IWebElement element, int index)
        {
            SelectElementByIndex(new SelectElement(element), index);
        }

        public void SelectElementsByText(IWebElement element, IEnumerable<string> toSelect)
        {
            var selectList = new SelectElement(element);
            foreach (string text in toSelect)
            {
                SelectElementByText(selectList, text);
            }
        }

        public void SelectElementsByIndex(IWebElement element, IEnumerable<int> toSelect)
        {
            var selectList = new SelectElement(element);
            foreach (int index in toSelect)
            {
                SelectElementByIndex(selectList, index);
            }
        }

        public void DeselectAll(IWebElement element)
        {
            DeselectAll(new SelectElement(element));
        }

        public void DeselectAll(SelectElement selectedElement)
        {
            selectedElement.DeselectAll();
        }

        private void SwitchToWindow(string windowName)
        {
            Driver.SwitchTo().Window(windowName);
            WaitForPageToLoad();
        }

        private string ClickAndGetNewWindowName(IWebElement element)
        {
            Click(element);
            return Driver.WindowHandles.Last();
        }

        public void CloseCurrentWindowAndSwitchToMainWindow()
        {
            if (Driver.CurrentWindowHandle == MainWindowHander)
                throw new Exception("Main window hander is presented");
            Driver.Close();
            Driver.SwitchTo().Window(MainWindowHander);
        }

        public void ClickAndSwitchToNewWindow(IWebElement element)
        {
            SwitchToWindow(ClickAndGetNewWindowName(element));
        }

        public void Dispose()
        {
            Driver.Dispose();
        }
    }
}
