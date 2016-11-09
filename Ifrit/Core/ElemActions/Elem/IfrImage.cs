namespace BotAgent.Ifrit.Core.ElemActions.Elem
{
    using System.Drawing;

    public class IfrImage : ActionsBase
    {
        /// <summary>
        /// Works even if images are disabled in browser!
        /// </summary>
        public Bitmap Image
        {
            get
            {
                string webPath = Element.GetAttribute("src");

                if (webPath != string.Empty)
                {
                    try
                    {
                        System.Net.WebRequest request =
                            System.Net.WebRequest.Create(webPath);

                        System.Net.WebResponse response = request.GetResponse();

                        System.IO.Stream responseStream = response.GetResponseStream();

                        Bitmap bitmapImg = new Bitmap(responseStream);

                        return bitmapImg;
                    }
                    catch (System.Net.WebException)
                    {
                    }
                }

                return new Bitmap(1,1);
            }
        }

        /// <summary>
        /// Returns url of image [src attribute value]
        /// </summary>
        public string SrcValue
        {
            get
            {
                return Element.GetAttribute("src");
            }
        }

        public IfrImage(IfrPage parent)
        {
            HierarhicalParent = parent;
        }

        public Bitmap GetElementScreenshot()
        {
            Size elemSize = Element.Size;
            Point point = Element.Location;

            return HierarhicalParent.GetScreenshotBitmap(point, elemSize);
        }
    }
}
