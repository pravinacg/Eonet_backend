using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace Eonet
{
    public class EonetWebRequest
    {
        public string GetResponse(string Url)
        {
            string response = string.Empty;

            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(Url);

                httpWebRequest.ContentType = "application/json; charset=utf-8";

                httpWebRequest.Method = "GET";

                httpWebRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

                HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                response = ParseResponse(httpResponse);
                
                httpResponse.Close();

            }
            catch(Exception ex)
            {
                //error logging 
                response = ex.InnerException.ToString();
            }
            return response;

        }

        public string ParseResponse(HttpWebResponse eonetWebResponse)
        {
            string response = "";
             if (eonetWebResponse.StatusCode != HttpStatusCode.OK)
            {
                //can be addded different status check here 
                response= "problem in webservice";
            }
            else
            {
                using (StreamReader streamReader = new StreamReader(eonetWebResponse.GetResponseStream()))
                {
                    response = streamReader.ReadToEnd();
                }
            }

            return response;
        }


    }
}