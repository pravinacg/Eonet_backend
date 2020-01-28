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

            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("https://eonet.sci.gsfc.nasa.gov/api/v2.1/events");
            httpWebRequest.ContentType = "application/json; charset=utf-8";
            httpWebRequest.Method = "GET";
            httpWebRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                response = streamReader.ReadToEnd();
            }
            httpResponse.Close();

            return response;

        }
    }
}