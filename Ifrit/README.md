# README #

Quick overview:

1) initialization:
var Brwsr = new IfrBrowser("ff");
(Initialization of browser. At the moment in so lame way =) In future will be something like  var Brwsr = new IfrBrowser.ff(); )

Logging of messages:
Brwsr.Log.ErrorMsg("some error was throwned");

2) Logging
(also there is exis info messages, warnings, etc. Realized on NLog)
All Logs will be located at path written in ParamsLib;
Logs have the following structure:
year.month.day.hour.minute_sessionId/Screenshots folder (there also will be saved html source file of the screenshots)
year.month.day.hour.minute_sessionId/Log files

Also in ParamsLib can be enabled more detailed logging (core actions logging), but in most cases this is useless feature.


3) Browser Navigation panel
Brwsr.Nav.GoTo("https://www.vk.com");
Brwsr.Nav.Backwar();
Brwsr.Nav.Stop();
Brwsr.Nav.Maximize();
etc

4) Search and actions making for web-elements:
Search of some web-element  (by presence on the page) in 1 second period and click if button was found (and wait for new page load event)
Brwsr.Page.Elem( By.Id("someId"), Await.Present(1000) ).AsButton().ClickAndWaitForPageLoad();

and by default way it will search for element for being Visible in 1,5 seconds:
Brwsr.Page.Elem(By.Id("someId")).AsButton().Click();

All elements is structured by the following way:
.AsButton() -- this is example of elemet type and framework can tell you which actions you can do with exactly this element

All types:
button, image, checkBox, DropDownList, Image, Input,Link, TextArea, Other

Other -- this web-element type with methods not related to any specific type of element.

5) Methods related to web-element type:
Every type have some specific individual methods group.
My framework also fixes "holes" of original selenium: there is no ability to get image source. In my framework its possible by 2 ways:

Bitmap OriginalImage = Brwsr.Page.Elem(By.Id("someId")).AsImage().Image;
Getting absolutely correct image source

Takin screenshot of all page, and automatically cut-off part of page not related to this element:
Bitmap OriginalImage = Brwsr.Page.Elem(By.Id("someId")).AsOther().GetElementScreenshot();

Also original selenium doesnt work with XML pages. I had fixed this by class based on HTML Agrility pack. And now we can work with it directly by the following syntax:
Getting of "type" attribute from "a" xml element located at any part of page:

string someIdValue = Brwsr.Page.Xml.Elmnt(".//a").AttributeValue("type");

syntax is similar, but you can use for search only xpath, (but not css selectors, ids, classes, etc)

6) JS execution:
Brwsr.Page.ExecuteJs("");

var rez = Brwsr.Page.ExecuteJs("");

7) Work with several windows:
Brwsr.WindowNew();
or
Brwsr.WindowNew("https://www.vk.com");

Change of active window by index and by title:
Brwsr.WindowChange( int index);
Brwsr.WindowChange( string title );

Brwsr.WindowSetNext();
Brwsr.WindowSetPrev();

8) Taking screenshot to bitmap:
Bitmap scren = Brwsr.Page.GetScreenshotBitmap();

Screenshot logging (with html source):
Brwsr.Page.LogScreenshot("c:\screenshot.png");
Brwsr.Page.LogScreenshot("c:\screenshot.png",true);

(without html source):
Brwsr.Page.LogScreenshot("c:\screenshot.png",false);

also there is duplication:
Brwsr.Log.Page();

This will save screenshot directly to session logs folder.