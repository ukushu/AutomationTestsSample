using System.Diagnostics;

namespace BotAgent.Ifrit.Core
{
    using System;
    using System.Text;
    using System.Threading;
    using System.Linq;
    using System.Drawing;
    using System.IO;
    using BaseClasses;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Interactions;
    using SelBy = OpenQA.Selenium.By;
    using ElemTypes;
    using Xml;

    public class IfrPage
    {
        private readonly IWebDriver Instance ;
        private readonly IfrLog Log;

        public XmlActions Xml;

        public IfrBrowser HierarhyParent;

        /// <summary>
        /// Actions builder.
        /// FOR INTERNAL USE ONLY.
        /// </summary>
        public Actions WebActions;

        public IfrPage(IWebDriver instance, IfrBrowser parent)
        {
            HierarhyParent = parent;

            Instance = instance;

            Log = parent.Log;
            
            Xml = new XmlActions(instance);

            WebActions = new Actions(Instance);
        }

        public void ScrollToEndOfPage()
        {
            ExecuteJs("window.scrollTo(0, document.body.scrollHeight - 150)");
        }


        /// <summary>
        /// Find Web Element.
        /// If element is not visible or absent on the page, it will be seached in period of time configured in paramsLib
        /// </summary>
        /// <param name="by">Configure by what need to search web element</param>
        /// <returns></returns>
        public ElemType Elem(BaseClasses.By by)
        {
            return Elem(by, Await.Present());
        }

        /// <summary>
        /// Find Web Element.
        /// If element is not ready by Await parameter or absent on the page, it will be seached in period of time configured in paramsLib
        /// </summary>
        /// <param name="by">Configure by what need to search web element</param>
        /// <param name="awaitFor">Await.Visible() or Await.Visible(1000 -- like timeout)</param>
        /// <returns>element wrapper "ElemType"</returns>
        public ElemType Elem(BaseClasses.By by, Await awaitFor)
        {
            TimerTest timer = new TimerTest();
            ElemType elType = new ElemType(this);

            if (awaitFor.TimeOut < 0)
            {
                throw new Exception("Wait period cannot be less than 0");
            }

            if (awaitFor.Value == string.Empty)
            {
                elType = FindIfrElement(by);

                if (!elType.IsNull)
                {
                    return elType;
                }
            }

            for (; timer.GetElapsedTime() < awaitFor.TimeOut; )
            {
                ElemType webElement = FindIfrElement(by);

                if (IsElementReady(webElement, awaitFor))
                {
                    timer.Finish("Element changed status to ready in ");

                    return webElement;
                }
            }

            timer.Finish("Element wasnt changed it's status to requested in ");

            return elType;
        }

        public ElemsType Elems(BaseClasses.By by)
        {
            return FindIfrElements(by);
        }

        /// <summary>
        /// Find Element on the page
        /// </summary>
        /// <param name="by">ElemType of search</param>
        /// <returns>Element's type wrapper</returns>
        private ElemType FindIfrElement(BaseClasses.By by)
        {
            switch (by.CurrType)
            {
                case (int)BaseClasses.By.PossibleTypes.Class:
                    return FindElement(SelBy.ClassName(by.CurrValue));

                case (int)BaseClasses.By.PossibleTypes.CssSelector:
                    return FindElement(SelBy.CssSelector(by.CurrValue));

                case (int)BaseClasses.By.PossibleTypes.Id:
                    return FindElement(SelBy.Id(by.CurrValue));

                case (int)BaseClasses.By.PossibleTypes.Name:
                    return FindElement(SelBy.Name(by.CurrValue));

                case (int)BaseClasses.By.PossibleTypes.Xpath:
                    return FindElement(SelBy.XPath(by.CurrValue));
            }

            throw new Exception("Something going wrong, dude!");
        }

        private ElemType FindElement(SelBy seleniumBy)
        {
            ElemType element = new ElemType(this);
            try
            {
                var tmp = Instance.FindElement(seleniumBy);

                element.SetElement(tmp);

                return element;
            }
            catch (Exception ex)
            {
                Debug.WriteLine( ex.ToString() );
                //// No errors -- as designed
            }

            return element;
        }

        private ElemsType FindIfrElements(BaseClasses.By by)
        {
            switch (by.CurrType)
            {
                case (int)BaseClasses.By.PossibleTypes.Class:
                    return FindElements(SelBy.ClassName(by.CurrValue));

                case (int)BaseClasses.By.PossibleTypes.CssSelector:
                    return FindElements(SelBy.CssSelector(by.CurrValue));

                case (int)BaseClasses.By.PossibleTypes.Id:
                    return FindElements(SelBy.Id(by.CurrValue));

                case (int)BaseClasses.By.PossibleTypes.Name:
                    return FindElements(SelBy.Name(by.CurrValue));

                case (int)BaseClasses.By.PossibleTypes.Xpath:
                    return FindElements(SelBy.XPath(by.CurrValue));
            }

            throw new Exception("Something going wrong, dude!");
        }

        private ElemsType FindElements(SelBy seleniumBy)
        {
            ElemsType elements = new ElemsType(this);
            try
            {
                var tmp = Instance.FindElements(seleniumBy).ToList();

                elements.SetElements(tmp);

                return elements;
            }
            catch (Exception)
            {
                //// No errors -- as designed
            }

            return elements;
        }

        private bool IsElementReady(ElemType element, Await awaitFor)
        {
            switch (awaitFor.CurrStatus)
            {
                case (int)Await.Status.Visible:
                    return AwaitCheck.Visible(element);

                case (int)Await.Status.NotVisible:
                    return !(AwaitCheck.Visible(element));

                case (int)Await.Status.Present:
                    return AwaitCheck.Present(element);

                case (int)Await.Status.NotPresent:
                    return !(AwaitCheck.Present(element));

                case (int)Await.Status.ReadOnly:
                    throw new Exception("Not work at the moment");

                case (int)Await.Status.NotReadOnly:
                    throw new Exception("Not work at the moment");

                case (int)Await.Status.Empty:
                    return AwaitCheck.TextEmpty(element);

                case (int)Await.Status.NotEmpty:
                    return !(AwaitCheck.TextEmpty(element));

                case (int)Await.Status.Enabled:
                    return AwaitCheck.Enabled(element);

                case (int)Await.Status.NotEnabled:
                    return !(AwaitCheck.Enabled(element));

                case (int)Await.Status.TextMatch:
                    throw new Exception("Not work at the moment");

                case (int)Await.Status.TextEquals:
                    return AwaitCheck.TextEquals(element, awaitFor);

                case (int)Await.Status.TextNotEquals:
                    return !(AwaitCheck.TextEquals(element, awaitFor));

                case (int)Await.Status.TextEqualsCaseSensitive:
                    return AwaitCheck.TextEqualsCaseSensitive(element, awaitFor);

                case (int)Await.Status.TextNotEqualsCaseSensitive:
                    return !(AwaitCheck.TextEqualsCaseSensitive(element, awaitFor));

                case (int)Await.Status.TextPartExist:
                    return AwaitCheck.TextPartExist(element, awaitFor);

                case (int)Await.Status.TextPartNotExist:
                    return !(AwaitCheck.TextPartExist(element, awaitFor));

                case (int)Await.Status.TextPartExistCaseSensitive:
                    return AwaitCheck.TextPartExistCaseSensitive(element, awaitFor);

                case (int)Await.Status.TextPartNotExistCaseSensitive:
                    return !(AwaitCheck.TextPartExistCaseSensitive(element, awaitFor));
            }

            return false;
        }

        private static class AwaitCheck
        {
            public static bool Visible(ElemType element)
            {
                return element.AsOther().IsVisible();
            }

            public static bool Present(ElemType element)
            {
                return element.AsOther().IsExist();
            }

            public static bool TextEmpty(ElemType element)
            {
                if (element.AsOther().Text == String.Empty)
                {
                    return true;
                }
                return false;
            }

            public static bool Enabled(ElemType element)
            {
                return element.AsOther().IsEnabled();
            }

            public static bool TextEquals(ElemType element, Await awaitFor)
            {
                if (element.AsOther().Text.ToLower() == awaitFor.Value.ToLower())
                {
                    return true;
                }

                return false;
            }

            public static bool TextEqualsCaseSensitive(ElemType element, Await awaitFor)
            {
                if (element.AsOther().Text == awaitFor.Value)
                {
                    return true;
                }

                return false;
            }

            public static bool TextPartExist(ElemType element, Await awaitFor)
            {
                string subString = awaitFor.Value.ToLower();

                if (element.AsOther().Text.ToLower().Contains(subString))
                {
                    return true;
                }

                return false;
            }

            public static bool TextPartExistCaseSensitive(ElemType element, Await awaitFor)
            {
                if (element.AsOther().Text.Contains(awaitFor.Value))
                {
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Getting of Page HTML Sourse code
        /// </summary>
        public string PageSource
        {
            get
            {
                return Instance.PageSource;
            }
        }

        /// <summary>
        /// Getting of Page title form current active page
        /// </summary>
        public string PageTitle
        {
            get
            {
                return Instance.Title;
            }
        }

        /// <summary>
        /// Get or set current Browser's url form current active page
        /// Set method calls browser.Nav.GoTo("");
        /// </summary>
        public string Url
        {
            get
            {
                return Instance.Url;
            }

            set
            {
                HierarhyParent.Nav.GoTo(value);
            }
        }

        /// <summary>
        /// Execute javascript code on the current active page
        /// </summary>
        /// <param name="javascriptBody">javascript code</param>
        /// <returns>you can get response from browser if you will write "return SomeWariable;" in the end of JS. 
        /// This feature doesn't work for IE.</returns>
        public object ExecuteJs(string javascriptBody)
        {
            IJavaScriptExecutor js = Instance as IJavaScriptExecutor;

            return js.ExecuteScript(javascriptBody);
        }

        /// <summary>
        /// Execute javascript code on the current active page and checking for correct response
        /// </summary>
        /// <param name="javascriptBody">javascript код</param>
        /// <param name="checkResult">check resutl using Equeals</param>
        public bool ExecuteJs(string javascriptBody, string checkResult)
        {
            IJavaScriptExecutor js = Instance as IJavaScriptExecutor;
            return js.ExecuteScript(javascriptBody).Equals(checkResult);
        }

        /// <summary>
        /// Make Screenshot of the active Browser's page and save it
        /// </summary>
        public void LogScreenshot(string path, bool isNeedToSavePageSourse)
        {
            FileInfo lastFile;
            int newFileNumber = 1000000;
            string fileNameTemplate = ParamsLib.IfritOptions.ScreenShotName;

            MkDirIfMissing(path);

            var directory = new DirectoryInfo(path);

            try
            {
                lastFile = (from f in directory.GetFiles()
                            orderby f.LastWriteTime descending
                            select f).First();

                if (lastFile.Name.Contains(fileNameTemplate))
                {
                    newFileNumber =
                        int.Parse(lastFile.Name.Replace(fileNameTemplate, string.Empty)
                            .Replace(".png", string.Empty)) + 1;
                }
            }
            catch (Exception)
            {
                //// no actions needed
            }

            string fileAdress = string.Format(@"{0}{1}{2}", directory, fileNameTemplate, newFileNumber);
            string screenShotFileAdress = fileAdress + ".png";
            string pageSourseFileAdress = fileAdress + "_source.html";

            if (isNeedToSavePageSourse)
            {
                MkCommentFileIfMissing(pageSourseFileAdress, Instance.PageSource);
            }

            Screenshot myScreenShot = ((ITakesScreenshot)Instance).GetScreenshot();

            myScreenShot.SaveAsFile(screenShotFileAdress, System.Drawing.Imaging.ImageFormat.Png);
            Log.InfoMsg("Screenshot was saved to file '{0}'.", fileAdress);
        }

        private void MkCommentFileIfMissing(string path, string comment)
        {
            if (comment != string.Empty)
            {
                File.WriteAllText(path, comment,Encoding.UTF8);
            }
        }

        /// <summary>
        /// Make Screenshot of the active Browser's page and return Bitmap
        /// </summary>
        /// <returns>screenshot Bitmap</returns>
        public Bitmap GetScreenshotBitmap()
        {
            Screenshot myScreenShot = ((ITakesScreenshot)Instance).GetScreenshot();

            Bitmap screen = new Bitmap(new MemoryStream(myScreenShot.AsByteArray));

            return screen;
        }

        /// <summary>
        /// Returns Bitmap of the screenshot of the some area of the page
        /// </summary>
        /// <param name="point"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public Bitmap GetScreenshotBitmap(Point point, Size size)
        {
            Screenshot myScreenShot = ((ITakesScreenshot)Instance).GetScreenshot();

            Bitmap screen = new Bitmap(new MemoryStream(myScreenShot.AsByteArray));

            Bitmap eleScreenshot = screen.Clone(new Rectangle(point, size), screen.PixelFormat);

            return eleScreenshot;
        }

        /// <summary>
        /// Create folder if its not exist
        /// </summary>
        /// <param name="path">folder path</param>
        private void MkDirIfMissing(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public bool WaitForPageLoad()
        {
            return WaitForPageLoad(400, ParamsLib.BrwsrOptions.PageLoadTimeOut, false);
        }

        public bool WaitForPageLoad(bool needToLog)
        {
            return WaitForPageLoad(400, ParamsLib.BrwsrOptions.PageLoadTimeOut, needToLog);
        }

        public bool WaitForPageLoad(int firstWait, int timeoutMs, bool needToLog)
        {
            TimerTest PageLoadTimer = new TimerTest();

            Thread.Sleep(firstWait);

            if (timeoutMs < 0)
            {
                throw new Exception("wait time cannot be less than " + firstWait + " ms!");
            }

            for (; PageLoadTimer.GetElapsedTime() < timeoutMs; )
            {
                string readyStateJsStr = "return document.readyState;";

                var javaScriptExecutor = Instance as IJavaScriptExecutor;

                bool isReady = javaScriptExecutor.ExecuteScript(readyStateJsStr).Equals("complete");

                if (isReady)
                {
                    if (needToLog)
                    {
                        PageLoadTimer.Finish("Page Loaded in");
                    }

                    return true;
                }
            }

            if (needToLog)
            {
                PageLoadTimer.Finish("Page failed to load after");
            }

            return false;
        }

        public bool WaitForAjax()
        {
            return WaitForAjax(200, ParamsLib.BrwsrOptions.PageLoadTimeOut);
        }

        public bool WaitForAjax(int firstWait, int timeoutMs)
        {
            bool isJqueryComplete = false;
            bool isPrototypeComplete = false;
            bool isDojoComplete = false;

            TimerTest timer = new TimerTest();

            Thread.Sleep(firstWait);

            for (; timer.GetElapsedTime() < timeoutMs; )
            {
                try
                {
                    var javaScriptExecutor = Instance as IJavaScriptExecutor;
                    if (javaScriptExecutor != null)
                    {
                        isJqueryComplete = (bool)javaScriptExecutor.ExecuteScript(" return jQuery.active == 0");
                    }
                }
                catch (Exception)
                {
                    //// Normal behaviour
                }

                try
                {
                    var javaScriptExecutor = Instance as IJavaScriptExecutor;
                    if (javaScriptExecutor != null)
                    {
                        isDojoComplete =
                            (bool)
                                javaScriptExecutor.ExecuteScript("userWindow.dojo.io.XMLHTTPTransport.inFlight.length == 0");
                    }
                }
                catch (Exception)
                {
                    //// Normal behaviour
                }

                try
                {
                    var javaScriptExecutor = Instance as IJavaScriptExecutor;
                    if (javaScriptExecutor != null)
                    {
                        isPrototypeComplete =
                            (bool)javaScriptExecutor.ExecuteScript("userWindow.Ajax.activeRequestCount == 0");
                    }
                }
                catch (Exception)
                {
                    //// Normal behaviour
                }

                if (isJqueryComplete | isPrototypeComplete | isDojoComplete)
                {
                    timer.Finish("Wait for Ajax sucessfully finished in");

                    return true;
                }
            }

            timer.Finish("Wait for Ajax failed after");

            return false;
        }

        /// <summary>
        /// Close this browser page/window. 
        /// In this case active page/window changes to first window.
        /// </summary>
        public void Close()
        {
            Instance.Close();
            HierarhyParent.WindowChange(0);
        }

    }
}
