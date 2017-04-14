using System;
using BotAgent.Ifrit.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using By = BotAgent.Ifrit.Core.BaseClasses.By;

namespace UnitTests
{
    [TestClass]
    public class FuncTests
    {
        IfrBrowser _brwsr;
        string _siteUrl;
        AppControlls _appControlls;

        //I know that this is code repeating but looks like this is limitation of MSTest. 
        //Its will be not repeated in case of using NUnit
        [TestInitialize]
        public void Init()
        {
            _siteUrl = $"file:///{AppDomain.CurrentDomain.BaseDirectory}/App/index.html";
            _brwsr = new IfrBrowser(Brwsr.Firefox);
            _brwsr.Nav.GoToAndWaitForPageLoad(_siteUrl);

            _appControlls = new AppControlls(_brwsr);
        }
        
        [TestCleanup]
        public void Cleanup()
        {
            _brwsr.Close();
        }


        
        [TestMethod]
        public void DeleteAndAddLettersToTreeViaContextMenu()//TODO: Is'nt work on non-standard context menu. Need to rewrite.
        {
            //Remove first element
            var wElem = _appControlls.TreeChildren[0].GetIWebElement();
            _brwsr.Page.WebActions.ContextClick(wElem)
                .SendKeys(Keys.ArrowDown).SendKeys(Keys.ArrowDown)
                .SendKeys(Keys.Enter)
                .Build().Perform();

            //Add element
            wElem = _appControlls.TreeChildren[0].GetIWebElement();
            _brwsr.Page.WebActions.ContextClick(wElem)
                .SendKeys(Keys.ArrowDown).SendKeys(Keys.ArrowDown)
                .SendKeys(Keys.Enter)
                .Build().Perform();
            LetterAddPopupIsOpenedPerformAddFirstElement();
        }

        [TestMethod]
        public void DeleteAndAddLettersToGreed()
        {
            _appControlls.GridChildren[0].AsButton().Click();

            _appControlls.GridDeleteBtn.Click();

            _appControlls.GridContainsLettersElems("LMNOPQRSTUVWXYZ");

            _appControlls.GridAddBtn.Click();

            LetterAddPopupIsOpenedPerformAddFirstElement();

            _appControlls.GridContainsLettersElems("KLMNOPQRSTUVWXYZ");
        }


        public void LetterAddPopupIsOpenedPerformAddFirstElement()
        {
            var ddBtn = _brwsr.Page.Elem(By.Xpath(".//div[contains(@id,'add-letter')]//div[@id,'ext-gen']")).GetIWebElement();

            _brwsr.Page.WebActions.Click(ddBtn).SendKeys(Keys.ArrowDown).SendKeys(Keys.Enter).Build().Perform();
        }

    }
}
