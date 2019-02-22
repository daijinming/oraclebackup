using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.IO;
using System.Text;
using System.Net;

namespace EcobpMQ.TaskSet
{
    class HttpUtil
    {

        public static string Post(string serviceUrl,string requestBody)
        {   

            return UseHttpWebApproach(serviceUrl, string.Empty, "POST", requestBody);
        }

        public static string PostJson(string serviceUrl, string requestBody)
        {

            return UseHttpWebApproach_Json(serviceUrl, string.Empty, "POST", requestBody, "utf-8");
        }

        public static string UseHttpWebApproach(string serviceUrl, string resourceUrl, string method, string requestBody)
        {
            return UseHttpWebApproach(serviceUrl, resourceUrl, method, requestBody, "utf-8");
        }

        public static string UseHttpWebApproach(string serviceUrl, string resourceUrl, string method, string requestBody, string sendEncodingName)
        {
            string responseMessage = null;
            HttpWebRequest request = WebRequest.Create(string.Concat(serviceUrl, resourceUrl)) as HttpWebRequest;
            if (request != null)
            {
                //request.ContentType = "application/json";
                request.ContentType = "application/x-www-form-urlencoded";
                request.Method = method;
                if (ConfigInit.GetValue("Sandbox").ToLower() == "true")
                {   
                    request.Headers.Add("sandbox", "1");
                }
            }

            if (method == "POST" && requestBody != null)
            {
                byte[] requestBodyBytes = ToByteArray(requestBody, sendEncodingName);

                request.ContentLength = requestBodyBytes.Length;

                using (Stream postStream = request.GetRequestStream())
                    postStream.Write(requestBodyBytes, 0, requestBodyBytes.Length);
            }

            if (request != null)
            {
                //request.Accept = "application/json";
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream responseStream = response.GetResponseStream();
                    if (responseStream != null)
                    {
                        StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                        
                        responseMessage = reader.ReadToEnd();
                    }
                }
                else
                {
                    responseMessage = response.StatusDescription;
                }
            }
            return responseMessage;
        }

        public static string UseHttpWebApproach_Json(string serviceUrl, string resourceUrl, string method, string requestBody, string sendEncodingName)
        {
            string responseMessage = null;
            HttpWebRequest request = WebRequest.Create(string.Concat(serviceUrl, resourceUrl)) as HttpWebRequest;
            if (request != null)
            {
                request.ContentType = "application/json";
                request.Method = method;
                if (ConfigInit.GetValue("Sandbox") == "true")
                {   
                    request.Headers.Add("sandbox", "1");
                }
            }
            
            if (method == "POST" && requestBody != null)
            {
                byte[] requestBodyBytes = ToByteArray(requestBody, sendEncodingName);

                request.ContentLength = requestBodyBytes.Length;

                using (Stream postStream = request.GetRequestStream())
                    postStream.Write(requestBodyBytes, 0, requestBodyBytes.Length);
            }

            if (request != null)
            {
                //request.Accept = "application/json";
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream responseStream = response.GetResponseStream();
                    if (responseStream != null)
                    {
                        StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));

                        responseMessage = reader.ReadToEnd();
                    }
                }
                else
                {	
                    responseMessage = response.StatusDescription;
                }
            }
            return responseMessage;
        }
        private static byte[] ToByteArray(string requestBody)
        {
            return ToByteArray(requestBody, "utf-8");
        }

        private static byte[] ToByteArray(string requestBody, string EncodingName)
        {
            byte[] bytes = null;
            
            bytes = Encoding.GetEncoding(EncodingName).GetBytes(requestBody);
            return bytes;
        }

        

    }
}
