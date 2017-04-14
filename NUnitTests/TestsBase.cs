using System;
using BotAgent.Ifrit.Core;
using NUnit.Framework;

namespace NUnitTests
{
    public class TestsBase
    {
        protected IfrBrowser _brwsr;
        protected string _siteUrl;
        protected AppControlls _mainPageControlls;

        [SetUp]
        public void Init()
        {
            _siteUrl = $"file:///{AppDomain.CurrentDomain.BaseDirectory}/App/index.html";
            _brwsr = new IfrBrowser(Brwsr.Firefox);
            _brwsr.Nav.GoToAndWaitForPageLoad(_siteUrl);

            _mainPageControlls = new AppControlls(_brwsr);
        }

        [TearDown]
        public void Cleanup()
        {
            _brwsr.Close();
        }
    }
}
