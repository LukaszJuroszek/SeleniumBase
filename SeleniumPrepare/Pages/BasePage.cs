using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pages
{
    public abstract class BasePage : IDisposable, IBasePage
    {
        public IWebDriver Driver { get; set; }
        public string BaseUrl { get; }
        public WebDriverWait Wait { get; set; }
        public string MainWindowHander { get; set; }

        protected BasePage(string baseUrl, IWebDriver driver, int timeToWait = 5)
        {
            BaseUrl = baseUrl;
            Driver = driver;
            InitWebDriverWait(timeToWait);
            InitWindow();
            MainWindowHander = Driver.CurrentWindowHandle;
        }

        private void InitWindow()
        {
            //that dont work in chrome driver
            if (Driver is ChromeDriver)
            {

            }
            else
            {
                Driver.Manage().Window.Maximize();
            }
        }

        private void InitWebDriverWait(int timeToWait)
        {
            if (timeToWait < 4)
                throw new ArgumentException($"invalid timeToWait... was {timeToWait}");
            Wait = new WebDriverWait(Driver, timeout: TimeSpan.FromSeconds(timeToWait))
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
            if (string.IsNullOrEmpty(text))
                throw new ArgumentException("text should be not null or empty");
            element.SendKeys(text);
        }

        public void TakeScreanShot()
        {
            var ss = ((ITakesScreenshot)Driver).GetScreenshot();
            var path = $"{AppDomain.CurrentDomain.BaseDirectory}{Guid.NewGuid()}.png";
            ss.SaveAsFile(path, ScreenshotImageFormat.Png);
        }

        public string GetTitle() => Driver.Title;

        public bool AreAllCheckboxesSelected(IList<IWebElement> elementList, int expected)
        {
            if (expected < 0)
                throw new ArgumentException("expected should be greater or than 0");
            var result = elementList.Sum(x => x.Selected ? 1 : 0);
            return result == expected;
        }

        public void SelectElementByText(SelectElement selectedElement, string text)
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentException("text should be not null or empty");
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
            foreach (var text in toSelect)
            {
                SelectElementByText(selectList, text);
            }
        }

        public void SelectElementsByIndex(IWebElement element, IEnumerable<int> toSelect)
        {
            var selectList = new SelectElement(element);
            foreach (var index in toSelect)
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
