using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using Pages;
using System;

namespace SeleniumTests
{
    [TestFixture]
    [Parallelizable]
    public class SeleniumEasyTests
    {
        private SeleniumEasyPage _page;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var gridUri = new Uri("http://192.168.102.214:4444/wd/hub");
            var options = new ChromeOptions
            {
                PlatformName = "Windows",
            };
            var webDriver = new RemoteWebDriver(gridUri, options);
            _page = new SeleniumEasyPage(webDriver);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _page?.Dispose();
        }

        [TestCase("Sample Message", TestName = "Message in ShowMessage Element shoud be Sample Message")]
        [TestCase("Message", TestName = "Message in ShowMessage Element shoud be Message")]
        [TestCase("!@#$$$$Sample Message", TestName = "Message in ShowMessage Element shoud be !@#$$$$Sample Message")]
        [TestCase("___Message", TestName = "Message in ShowMessage Element shoud be___Message")]
        [TestCase("asdasdSample Message", TestName = "Message in ShowMessage Element shoud be asdasdSample Message")]
        [TestCase("-0ioakkka Message", TestName = "Message in ShowMessage Element shoud be -0ioakkka Message")]
        public void OpenMainPageAndClickInputFormAfterThatClickSimpleFormDemo(string message)
        {
            _page.NavigateToMainPage();
            var actualMessage = _page.ClickInputFormsAndSimpleFormDemoMenuItem()
                                     .EnterMessage(message)
                                     .ClickShowMessage()
                                     .GetYourMessage();
            Assert.AreEqual(message, actualMessage);
        }

        [TestCase]
        public void OpenMainPageAndClickInputFormAndClickCheckboxDemoMenuAndSelectAllCheckBoxInMultipleCheckboxDemo()
        {
            _page.NavigateToMainPage();
            var areSelectedAllChcexbox = _page.ClickInputFormsAndCheckboxDemoMenuItem()
                                              .SelectAllCheckbox()
                                              .AreAllCheckboxesSelected(4);

            Assert.AreEqual(true, areSelectedAllChcexbox);
        }
    }
}
