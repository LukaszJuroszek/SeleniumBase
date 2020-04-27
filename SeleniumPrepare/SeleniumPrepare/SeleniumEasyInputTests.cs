using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using Pages;
using System;

namespace SeleniumTests
{
    [TestFixture]
    [Parallelizable]
    public class SeleniumEasyInputTests
    {
        private SeleniumEasyPage _page;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var gridUri = new Uri("http://localhost:4444/wd/hub");
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

        [TestCase("1", "2", "3", TestName = "1 plus 2 should return three")]
        [TestCase("121", "asd", "NaN", TestName = "121 plus asd should return NaN")]
        [TestCase("1asd", "2", "3", TestName = "1asd plus 2 should return 3")]
        [TestCase("asd", "2", "NaN", TestName = "asd plus 2 should return NaN")]
        [TestCase("321", "123", "444", TestName = "123 plus 321 should return 444")]
        [TestCase("1", "2asd2", "3", TestName = "1 plus 2asd2 should return 3")]
        public void OpenSimpleFormDemo(string firstValue, string secondValue, string expected)
        {
            _page.NavigateToMainPage();
            var sum = _page
                 .ClickInputFormsAndSimpleFormDemoMenuItem()
                 .EnterValueA(firstValue)
                 .EnterValueB(secondValue)
                 .ClickGetTotal()
                 .GetTotalSum();
            _page.TakeScreanShot();
            Assert.AreEqual(expected, sum);
        }

        [TestCase]
        public void OpenMainPageAndClickInputFormAfterThatClickCheckboxDemoMenu()
        {
            _page.NavigateToMainPage();
            var isVisible = _page
                  .ClickInputFormsAndCheckboxDemoMenuItem()
                  .CickSingeCheckBox()
                  .IsSuccessMessageVisible();

            Assert.AreEqual(true, isVisible);
        }
    }
}
