//namespace BotAgent.Ifrit.Core
//{
//    using System;

//    public partial class Brwsr
//    {
//        public partial class CurrPage
//        {
//            public static class SendRequest
//            {
//                private const string requestJsCode =
//                    "function post(path, params, method) {" +
//                        "var form = document.createElement(\"form\");" +
//                        "form.setAttribute(\"method\", method);" +
//                        "form.setAttribute(\"action\", path);" +

//                        "for(var key in params) {" +
//                            "if(params.hasOwnProperty(key)) {" +
//                                "var hiddenField = document.createElement(\"input\");" +
//                                "hiddenField.setAttribute(\"type\", \"hidden\");" +
//                                "hiddenField.setAttribute(\"name\", key);" +
//                                "hiddenField.setAttribute(\"value\", params[key]);" +

//                                "form.appendChild(hiddenField);" +
//                             "}" +
//                        "}" +

//                        "document.body.appendChild(form);" +
//                        "form.submit();" +
//                    "}";

//                private const string request = "post('{0}', {{{1}}}, {2});";
                
//                public static void Post(string path, params string[] parametersAndValues)
//                {
//                    string parametersStr = GenerateParamsStr(parametersAndValues);

//                    Post(path, parametersStr);
//                }

//                public static void Post(string path, string parameters)
//                {
//                    string getRequest = string.Format(request, path, parameters, "post");

//                    string fullRequest = requestJsCode + getRequest;

//                    Brwsr.CurrPage.ExecuteJs(fullRequest);
//                }

//                private static string GenerateParamsStr(params string[] parameters)
//                {
//                    if (parameters.Length % 2 != 0)
//                    {
//                        throw new Exception("Every parameter of GET/POST request must to have value!");
//                    }

//                    string rezult = string.Empty;

//                    for (int i = 0; i < parameters.Length; i += 2)
//                    {
//                        rezult += string.Format("{0}:'{1}',", parameters[i], parameters[i + 1]);
//                    }

//                    rezult = rezult.TrimEnd(',');

//                    return rezult;
//                }
//            }
//        }
//    }
//}
