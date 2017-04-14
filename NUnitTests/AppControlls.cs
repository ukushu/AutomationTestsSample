using System.Collections.Generic;
using BotAgent.Ifrit.Core;
using BotAgent.Ifrit.Core.BaseClasses;
using BotAgent.Ifrit.Core.ElemActions.Elem;
using BotAgent.Ifrit.Core.ElemTypes;
using NUnit.Framework;
using NUnitTests.Extensions;

namespace NUnitTests
{
    public class AppControlls
    {
        IfrBrowser _brwsr;
        public AppControlls(IfrBrowser brwsr)
        {
            _brwsr = brwsr;
        }

        public Other LPanel
        {
            get { return _brwsr.Page.Elem(By.Xpath(".//div[contains(@id, 'dd-tree')]")).AsOther(); }
        }

        public Other RPanel
        {
            get { return _brwsr.Page.Elem(By.Xpath(".//div[contains(@id, 'dd-grid')]")).AsOther(); }
        }

        public Other EngAlphabet
        {
            get { return _brwsr.Page.Elem(By.Xpath(".//div[contains(@id, 'dd-tree')]//span[text()='English alphabet']")).AsOther(); }
        }

        public List<ElemType> TreeChildren
        {
            get { return _brwsr.Page.Elems(By.Xpath($".//div[contains(@id, 'dd-tree')]//tr[contains(@id, 'record-ext-record')]//div[contains(@class,'x-grid-cell-inner')]/span")).AsList();}
        }

        public List<ElemType> GridChildren
        {
            get { return _brwsr.Page.Elems(By.Xpath($".//div[contains(@id, 'dd-grid')]//tr[contains(@id, 'record-ext-record')]//td[2][contains(@id,'ext-gen')]")).AsList(); }
        }

        //TODO
        public Button GridAddBtn
        {
            get { return _brwsr.Page.Elem(By.Xpath(".//div[contains(@id, 'dd-grid')]//span[contains(text(),'Add')]/../../..")).AsButton(); }
        }

        //TODO
        public Button GridDeleteBtn
        {
            get { return _brwsr.Page.Elem(By.Xpath(".//div[contains(@id, 'dd-grid')]//span[contains(text(),'Delete')]/../../..")).AsButton(); }
        }

        public void TreeContainsLettersElems(string childLetters = "ABCDEFGHIJ")
        {
            char[] charsChilds = childLetters.ToCharArray();

            var children = TreeChildren;
            
            Assert.AreEqual(charsChilds.Length, children.Count,"Incorrect Tree child count!");

            for (int i = 0; i < charsChilds.Length; i++)
            {
                var child = children[i].AsOther();

                if (child.Text != charsChilds[i].ToString())
                {
                    Assert.Fail("There is wrong child iside the Tree or incorrect sorting");
                }
            }
        }

        public void GridContainsLettersElems(string childLetters = "KLMNOPQRSTUVWXYZ", bool isDesc = true)
        {
            if (isDesc)
            {
                childLetters = childLetters.Reverse();
            }

            char[] charsChilds = childLetters.ToCharArray();

            var children = GridChildren;

            Assert.AreEqual(charsChilds.Length, children.Count,"Incorrect Grid child count!");

            for (int i = 0; i < charsChilds.Length; i++)
            {
                var child = children[i].AsOther();

                if (child.Text != charsChilds[i].ToString())
                {
                    Assert.Fail("There is wrong child iside the Grid or incorrect sorting");
                }
            }
        }

    }
}
