using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using Pages;

namespace SeleniumTests
{
    [TestFixture]
    public class SeleniumEasyTest
    {
        private SeleniumEasyPage _page;

        [OneTimeSetUp]
        public void Initialize()
        {
            _page = new SeleniumEasyPage(@"http://www.seleniumeasy.com/test/", driver: new ChromeDriver());
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
            var actualMessage = _page
                 .ClickInputFormsAndSimpleFormDemoMenuItem()
                 .EnterMessage(message)
                 .ClickShowMessage()
                 .GetYourMessage();
            Assert.AreEqual(message, actualMessage);
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

        [TestCase]
        public void OpenMainPageAndClickInputFormAndClickCheckboxDemoMenuAndSelectAllCheckBoxInMultipleCheckboxDemo()
        {
            _page.NavigateToMainPage();
            var areSelectedAllChcexbox = _page
                  .ClickInputFormsAndCheckboxDemoMenuItem()
                  .SelectAllCheckbox()
                  .AreAllCheckboxesSelected(4);

            Assert.AreEqual(true, areSelectedAllChcexbox);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _page?.Dispose();
        }
    }
}
