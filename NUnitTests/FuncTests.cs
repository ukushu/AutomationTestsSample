using BotAgent.Ifrit.Core.BaseClasses;
using NUnit.Framework;
using Selenium = OpenQA.Selenium;

namespace NUnitTests
{
    [TestFixture]
    public class FuncTests: TestsBase
    {
        [Test]
        public void DeleteAndAddLettersToTreeViaContextMenu()//TODO: Is'nt work on non-standard context menu. Need to rewrite.
        {
            var wElem = _mainPageControlls.TreeChildren[0].GetIWebElement();

            _brwsr.Page.WebActions.ContextClick(wElem).Perform();
            _brwsr.Page.Elem(By.Xpath(".//span[text()='Delete']/..")).AsOther().Click();

            //Remove first element
            

            _brwsr.Page.WebActions.ContextClick(wElem)
                .SendKeys(Selenium.Keys.ArrowDown).SendKeys(Selenium.Keys.ArrowDown)
                .SendKeys(Selenium.Keys.Enter)
                .Build().Perform();
            
            //Add element
            wElem = _mainPageControlls.TreeChildren[0].GetIWebElement();
            _brwsr.Page.WebActions.ContextClick(wElem)
                .SendKeys(Selenium.Keys.ArrowDown).SendKeys(Selenium.Keys.ArrowDown)
                .SendKeys(Selenium.Keys.Enter)
                .Build().Perform();
            AddLetterPopupIsOpenedPerformAddFirstElement();
        }

        [Test]
        public void DeleteAndAddLettersToGreed()
        {
            _mainPageControlls.GridChildren[0].AsButton().Click();

            _mainPageControlls.GridDeleteBtn.Click();

            _mainPageControlls.GridContainsLettersElems("LMNOPQRSTUVWXYZ", false);//remove false!

            _mainPageControlls.GridAddBtn.Click();

            AddLetterPopupIsOpenedPerformAddFirstElement();

            _mainPageControlls.GridContainsLettersElems("KLMNOPQRSTUVWXYZ", false);//remove false!
        }

        //doesnt work
        public void AddLetterPopupIsOpenedPerformAddFirstElement()
        {
            var ddBtn = _brwsr.Page.Elem(By.Xpath(".//div[contains(@id,'add-letter')]//div[@id,'ext-gen']")).GetIWebElement();

            _brwsr.Page.WebActions.Click(ddBtn).SendKeys(Selenium.Keys.ArrowDown).SendKeys(Selenium.Keys.Enter).Build().Perform();
        }
    }
}
