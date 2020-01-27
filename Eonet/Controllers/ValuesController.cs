using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Script.Serialization;
using static EONET.Models.ResponseClass;

namespace Eonet.Controllers
{
    [RoutePrefix("api/Events")]
    public class EventsController : ApiController
    {
        // GET api/values
        public IEnumerable<Event> GetEvent()
        {
            String url = "https://eonet.sci.gsfc.nasa.gov/api/v2.1/events";
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

            JObject o = JObject.Parse(response);

            JArray a = (JArray)o["events"];

            List<Event> a2 = new List<Event>();

           
            List<Event> events = a.ToObject<List<Event>>();

            return events;

           // return new string[] { "value1", "value2" };


        }

        [Route("FilterEvents")]
        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        public IEnumerable<Event> FilterEvents(string status , string category)
        {
            List<Event> serachedEvent = GetEvent().ToList();
            
            var searched= serachedEvent.Where(f => f.categories[0].title == category ||  f.closed == null).ToList();
            return serachedEvent;
        }

        [Route("SortEvents")]
        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        public IEnumerable<Event> SortEvents(string orderby, string col)
        {
            List<Event> serachedEvent = GetEvent().ToList();
            if(orderby == "desc")
            {
                return  serachedEvent.OrderByDescending(x => x.categories[0].title).ToList();
            }
            else
            {
                return serachedEvent.OrderByDescending(x => x.categories[0].title).ToList();
            }
           
          
        }
        private static T Deserialize<T>(string response)
        {
            JsonSerializer json = new JsonSerializer();
            return json.Deserialize<T>(new JsonTextReader(new StringReader(response)));
          
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
