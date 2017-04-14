using NUnit.Framework;
using System.Linq;

namespace NUnitTests
{
    [TestFixture]
    public partial class UiTests: TestsBase
    {
        [Test]
        public void BasicUiTesting()
        {
            Assert.IsTrue(_mainPageControlls.LPanel.IsExist(),"There are no Left Tree side is shown");

            Assert.IsTrue(_mainPageControlls.RPanel.IsExist(),"There are no Right Greed side is shown");
            
            SizeIsCorrectAndAddaptive();

            Assert.IsTrue(_mainPageControlls.LPanel.IsVisible(),"EngAlphabet elem Not exist or not visible");

            ChkBoxGreedMultiselectCheck();

            ChkBoxSelectAllCheck();

            FooterContainsAllNeededBtns();

            TreeContextMenuPresenceChk();
        }
        
        [Test]
        public void DragNdropPlusDefaultContentAndSortingTest()
        {
            var elemToMove = _mainPageControlls.GridChildren[0].AsOther();
            var elemEndPoint = _mainPageControlls.TreeChildren[0].AsOther();

            elemToMove.DragElementTo(elemEndPoint);
            
            _mainPageControlls.TreeContainsLettersElems("ABCDEFGHIJK");
            _mainPageControlls.GridContainsLettersElems("LMNOPQRSTUVWXYZ");

            elemToMove = _mainPageControlls.TreeChildren.Last().AsOther(); 
            elemEndPoint = _mainPageControlls.GridChildren[0].AsOther();

            elemToMove.DragElementTo(elemEndPoint);

            _mainPageControlls.TreeContainsLettersElems("ABCDEFGHIJ");
            _mainPageControlls.GridContainsLettersElems("KLMNOPQRSTUVWXYZ");
        }   
    }
}
