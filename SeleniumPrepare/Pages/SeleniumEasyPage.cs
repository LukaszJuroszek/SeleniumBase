using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Pages
{
    public class SeleniumEasyPage : BasePage
    {
        public SeleniumEasyPage(string baseUrl, IWebDriver driver, int timeToWait = 4) : base(baseUrl, driver, timeToWait)
        {
            PageFactory.InitElements(driver, this);
        }
        //Menu
        [FindsBy(How = How.Id, Using = "treemenu")]
        private IWebElement _sideMenu;

        //Menu InputForms sub categoryWait
        [FindsBy(How = How.CssSelector, Using = "#treemenu > li > ul > li:nth-child(1) > a")]
        private IWebElement _inputForms;
        //Input Form sub category-simple From demo 
        [FindsBy(How = How.CssSelector, Using = "#treemenu > li > ul > li:nth-child(1) > ul > li:nth-child(1) > a")]
        private IWebElement _inputFormsSimpleFormDemo;

        //Input Form page/ Single Input Field
        [FindsBy(How = How.Id, Using = "user-message")]
        private IWebElement _userMessageInput;

        [FindsBy(How = How.CssSelector, Using = "#get-input > button")]
        private IWebElement _showMessageButton;

        [FindsBy(How = How.Id, Using = "display")]
        private IWebElement _showedMessage;

        //Input Form page/ Two Input Fields
        [FindsBy(How = How.Id, Using = "sum1")]
        private IWebElement _valueAInput;
        [FindsBy(How = How.Id, Using = "sum2")]
        private IWebElement _valueBInput;

        [FindsBy(How = How.CssSelector, Using = "#gettotal > button")]
        private IWebElement _getTotalButton;

        [FindsBy(How = How.Id, Using = "displayvalue")]
        private IWebElement _displayedValueResult;

        //Input Form sub category-Checkbox Demo //TODO:
        [FindsBy(How = How.CssSelector, Using = "#treemenu > li > ul > li:nth-child(1) > ul > li:nth-child(2) > a")]
        private IWebElement _inputFormsCheckboxDemo;

        //Single Checkbox Demo
        [FindsBy(How = How.Id, Using = "isAgeSelected")]
        private IWebElement _singeCheckBox;
        [FindsBy(How = How.Id, Using = "txtAge")]
        private IWebElement _messageElementAfterCheckBoxSelected;


        //Multiple Checkbox Demo
        [FindsBy(How = How.CssSelector, Using = ".cb1-element")]
        private IWebElement _opinion1CheckBox;

        [FindsBy(How = How.CssSelector, Using = "#easycont > div > div.col-md-6.text-left > div:nth-child(5) > div.panel-body > div:nth-child(4) > label > input")]
        private IWebElement _opinion2CheckBox;

        [FindsBy(How = How.CssSelector, Using = "#easycont > div > div.col-md-6.text-left > div:nth-child(5) > div.panel-body > div:nth-child(5) > label > input")]
        private IWebElement _opinion3CheckBox;

        [FindsBy(How = How.CssSelector, Using = "#easycont > div > div.col-md-6.text-left > div:nth-child(5) > div.panel-body > div:nth-child(6) > label > input")]
        private IWebElement _opinion4CheckBox;


        [FindsBy(How = How.CssSelector, Using = ".cb1-element")]
        private IList<IWebElement> _allOpinions;

        [FindsBy(How = How.Id, Using = "check1")]
        private IWebElement _checkUnCheckAll;

        public SeleniumEasyPage ClickInputFormsMenuItem()
        {
            Click(_inputForms);
            return this;
        }

        public SeleniumEasyPage ClickInputFormsAndSimpleFormDemoMenuItem()
        {
            ClickInputFormsMenuItem();
            Click(_inputFormsSimpleFormDemo);
            return this;
        }

        public SeleniumEasyPage ClickInputFormsAndCheckboxDemoMenuItem()
        {
            ClickInputFormsMenuItem();
            Click(_inputFormsCheckboxDemo);
            return this;
        }

        public SeleniumEasyPage EnterMessage(string message)
        {
            WtriteText(_userMessageInput, message);
            return this;
        }

        public SeleniumEasyPage ClickShowMessage()
        {
            Click(_showMessageButton);
            return this;
        }

        public string GetYourMessage()
        {
            return _showedMessage.Text;
        }

        public SeleniumEasyPage EnterValueA(string value)
        {
            WtriteText(_valueAInput, value);
            return this;
        }

        public SeleniumEasyPage EnterValueB(string value)
        {
            WtriteText(_valueBInput, value);
            return this;
        }

        public SeleniumEasyPage ClickGetTotal()
        {
            Click(_getTotalButton);
            return this;
        }

        public string GetTotalSum()
        {
            return _displayedValueResult.Text;
        }

        public SeleniumEasyPage CickSingeCheckBox()
        {
            Click(_singeCheckBox);
            return this;
        }

        public bool IsSuccessMessageVisible()
        {
            return _messageElementAfterCheckBoxSelected.Displayed;
        }

        private SeleniumEasyPage SelectOpinionCheckBox(int opinionNumber)
        {
            if (opinionNumber < 0 || opinionNumber > 3)
                throw new NoSuchElementException($"Cannot find Opinion checkBox : {opinionNumber}");
            _allOpinions[opinionNumber].Click();
            return this;
        }

        public SeleniumEasyPage SelectAllCheckbox()
        {
            for (int i = 0; i < _allOpinions.Count; i++)
            {
                SelectOpinionCheckBox(i);
            }
            return this;
        }

        public bool AreAllCheckboxesSelected(int expected)
        {
              return AreAllCheckboxesSelected(_allOpinions,expected);
        }

        public SeleniumEasyPage ClickCheckUnCheckAll()
        {
            Click(_checkUnCheckAll);
            return this;
        }

    }
}
