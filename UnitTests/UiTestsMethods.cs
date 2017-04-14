using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    public partial class UiTests
    {
        private void SizeIsCorrectAndAddaptive()
        {
            SizeIsCorrect();

            _brwsr.Nav.Maximize();
            
            //adaptivity check
            SizeIsCorrect();
        }

        private void SizeIsCorrect()
        {
            var lWidthStr = _mainPageControlls.LPanel.GetCssValue("width").Replace("px", "");
            var lWidth = Int32.Parse(lWidthStr);

            var rWidthStr = _mainPageControlls.RPanel.GetCssValue("width").Replace("px", "");
            var rWidth = Int32.Parse(rWidthStr);

            var percent = (lWidth + rWidth) / 100;

            Assert.AreEqual(lWidth / percent, 35, 1, "Incorrect size of left panel");
        }
        
        private void ChkBoxGreedMultiselectCheck()
        {
        }

        private void ChkBoxSelectAllCheck()
        {
        }

        private void FooterContainsAllNeededBtns()
        {

        }

        private void TreeContextMenuPresenceChk()
        {

        }
    }
}