using System;
using BotAgent.Ifrit.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;


//I know that is lame to use microsoft teset system, but I lost 40 mins and still didn't 
// install NUnit because of some bug  =((( That's why I'm using standard Microsoft Test features
// Time is going, so I have no ability to check what is the reason of problem.
namespace UnitTests
{
    [TestClass]
    public partial class UiTests
    {
        IfrBrowser _brwsr;
        string _siteUrl;
        AppControlls _mainPageControlls;

        [TestInitialize]
        public void Init()
        {
            _siteUrl = $"file:///{AppDomain.CurrentDomain.BaseDirectory}/App/index.html";
            _brwsr = new IfrBrowser(Brwsr.Firefox);
            _brwsr.Nav.GoToAndWaitForPageLoad(_siteUrl);

            _mainPageControlls = new AppControlls(_brwsr);
        }
        
        [TestCleanup]
        public void Cleanup()
        {
            _brwsr.Close();
        }


        [TestMethod]
        public void BasicUiTesting()
        {
            Assert.IsTrue(_mainPageControlls.LPanel.IsExist(),"There are no Left Tree side i shown");

            Assert.IsTrue(_mainPageControlls.RPanel.IsExist(),"There are no Right Greed side i shown");
            
            SizeIsCorrectAndAddaptive();

            Assert.IsTrue(_mainPageControlls.LPanel.IsVisible(),"EngAlphabet elem Not exist or not visible");

            ChkBoxGreedMultiselectCheck();

            ChkBoxSelectAllCheck();

            FooterContainsAllNeededBtns();

            TreeContextMenuPresenceChk();
        }

        [TestMethod]
        public void ContentCheck()
        {
            _mainPageControlls.TreeContainsLettersElems();

            _mainPageControlls.GridContainsLettersElems();
        }

        [TestMethod]
        public void DragNdropCheck()//TODO: Doesnt work
        {
            var elemToMove = _mainPageControlls.GridChildren[0].GetIWebElement();
            var elemEndPoint = _mainPageControlls.TreeChildren[0].GetIWebElement();
            
            _brwsr.Page.WebActions.ClickAndHold(elemToMove).MoveToElement(elemEndPoint).Release().Build().Perform();

            _mainPageControlls.TreeContainsLettersElems("ABCDEFGHIJK");
            _mainPageControlls.TreeContainsLettersElems("LMNOPQRSTUVWXYZ");
        }



        
    }
}
