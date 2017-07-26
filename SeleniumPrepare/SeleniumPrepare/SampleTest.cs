using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using Pages;

namespace SeleniumTests
{
    public class SampleTest
    {
        private PurePCPage _page;

        //[SetUp]
        //public void Initialize()
        //{
        //    _page = new PurePCPage(@"https://www.purepc.pl/", new ChromeDriver());
        //}

        //[Test]
        //public void OpenMainPage()
        //{
        //    _page.NavigateToMainPage();
        //}

        //[TestCase("user", "Konto użytkownika | PurePC.pl", Description = "Title Shoud be: Konto użytkownika | PurePC.pl")]
        //[TestCase("", "PurePC - wiemy, co się kręci!", Description = "Title Shoud be: PurePC - wiemy, co się kręci!")]
        //public void OpenSubPage(string url, string expected)
        //{
        //    string actual = _page.NawigateToSubPageAndGetTitle(url);
        //    Assert.AreEqual(expected, actual);
        //}

        //[TearDown]
        //public void TearDown()
        //{
        //    if (_page != null)
        //    {
        //        _page.Dispose();
        //    }
        //}
    }
}
