namespace BotAgent.Ifrit.Requester
{
    using System;
    using System.IO;
    using System.Net;
    using System.Text;

    public class Requester
    {
        private CookieCollection cookies;
        private string userAgent;
        private IWebProxy proxy;

        public enum methodType
        { 
            POST,
            GET
        }

        public static class ConType
        {
            public static class Audio
            {
                public static string Mp4 = "audio/mp4";

                public static string Mpeg = @"audio/mpeg";
            }

            public static class Text
            {
                public static string Plain(string encoding)
                {
                    return "text/plain; charset=" + encoding;
                }

                public static string Html(string encoding)
                {
                    return "text/html; charset=" + encoding;
                }
            }
            
            public static class Image
            {
                public static string Gif = "image/gif";

                public static string Jpg = "image/jpeg";

                public static string Png = "image/png";
            }

            public static class Video
            {
                public static string Flv = "video/x-flv";
            }


        }

        public Requester()
        {
            cookies = new CookieCollection();
            userAgent = ParamsLib.BrwsrOptions.UserAgent;
            
        }

        public object Post(string site, string postData)
        {
            return SendRequest(site, methodType.POST, ConType.Text.Html("WINDOWS-1251"), postData);
        }

        public string SendRequest(string site, methodType method, string contentType, string postData)
        {
            WebRequest request = WebRequest.Create(site);
            request.Method = method.ToString();
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            request.ContentType = contentType;
            request.ContentLength = byteArray.Length;

            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            WebResponse response = request.GetResponse();
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            Console.WriteLine(responseFromServer);

            reader.Close();
            dataStream.Close();
            response.Close();

            return responseFromServer;
        }



    }
}
