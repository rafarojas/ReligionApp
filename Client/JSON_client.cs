using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class JSON_client
    {
        string endpoint;

        public JSON_client(string endpointUrl)
        {
            endpoint = endpointUrl;
        }

        public void POST(string method, string JSONparameters)
        {
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(endpoint + method);
                httpWebRequest.Method = "POST";
                httpWebRequest.ContentType = "text/json";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(JSONparameters);
                    streamWriter.Flush();
                    streamWriter.Close();

                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        Console.WriteLine(method + " result: " + result.ToString());
                    }
                }
            }
            catch (WebException ex)
            {
                WebResponse errorResponse = ex.Response;
                using (Stream responseStream = errorResponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                    String errorText = reader.ReadToEnd();
                    Console.WriteLine("Exception caught: " + errorText);
                }
            }
        }

        public void GET(string method)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(endpoint + method);
                request.Method = "GET";

                WebResponse response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    var result = reader.ReadToEnd();
                    Console.WriteLine(method + " result: " + result.ToString());
                }
            }
            catch (WebException ex)
            {
                WebResponse errorResponse = ex.Response;
                using (Stream responseStream = errorResponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                    String errorText = reader.ReadToEnd();
                    Console.WriteLine("Exception caught: " + errorText);
                }
            }
        }
    }
}
