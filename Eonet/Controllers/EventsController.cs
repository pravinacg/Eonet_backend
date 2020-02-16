using Microsoft.AspNetCore.Cors;
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
using System.Web.Http.Cors;
using System.Web.Script.Serialization;
using static EONET.Models.ResponseClass;

namespace Eonet.Controllers
{
    [RoutePrefix("api/Events")]
    [System.Web.Http.Cors.EnableCors("*", "*", "*")]
    public class EventsController : ApiController
    {
        // GET api/values
        [AcceptVerbs("GET")]
        [HttpGet]
      
        public IEnumerable<Event> Events()
        {
            String url = "https://eonet.sci.gsfc.nasa.gov/api/v2.1/events";

            EonetWebRequest eoweb = new EonetWebRequest();

            string response = eoweb.GetResponse(url);

            JObject rootEonet = JObject.Parse(response);

            JArray oEvents = (JArray)rootEonet["events"];

            List<Event> events = oEvents.ToObject<List<Event>>();

            return events;
        }

        [Route("GetEventDetails")]
        [AcceptVerbs("GET")]
        [HttpGet]
        public Event GetEventDetails(string eventID)
        {
            String url = "https://eonet.sci.gsfc.nasa.gov/api/v2.1/events/"+ eventID;

            EonetWebRequest eoweb = new EonetWebRequest();

            string response = eoweb.GetResponse(url);

            JObject rootEonet = JObject.Parse(response);

             Event events = rootEonet.ToObject<Event>();

            return events;
        }


        [Route("GeteventsByDate")]
        [AcceptVerbs("GET")]
        [HttpGet]
        public IEnumerable<Event> GeteventsByDate(string start, string end)
        {
            String url = "https://eonet.sci.gsfc.nasa.gov/api/v3-beta/events?start=" + start + "&end=" + end;

            EonetWebRequest eoweb = new EonetWebRequest();

            string response = eoweb.GetResponse(url);

            JObject rootEonet = JObject.Parse(response);

            JArray oEvents = (JArray)rootEonet["events"];

            List<Event> events = oEvents.ToObject<List<Event>>();

            return events;
        }

        [Route("EventCategories")]
        [AcceptVerbs("GET")]
        [HttpGet]

        public IEnumerable<Category> getEventCategories()
        {
            String url = "https://eonet.sci.gsfc.nasa.gov/api/v2.1/categories";

            EonetWebRequest eoweb = new EonetWebRequest();

            string response = eoweb.GetResponse(url);

            JObject oCategories = JObject.Parse(response);

            JArray categories = (JArray)oCategories["categories"];


            List<Category> categoriesResult = categories.ToObject<List<Category>>();


                    return categoriesResult.ToList();
        }


        [Route("FilterEvents")]
        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        public IEnumerable<Event> FilterEvents(string status, string category)
        {
            List<Event> serachedEvent = Events().ToList();

            return serachedEvent.Where(f => f.categories[0].title.ToLower() == category.ToLower() && f.closed == null).ToList();

        }


        [Route("SortEvents")]
        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        public IEnumerable<Event> SortEvents(string orderby, string col)
        {
            List<Event> serachedEvent = Events().ToList();

            if (orderby == "desc")
            {
                return serachedEvent.OrderByDescending(x => x.categories[0].title).ToList();
            }
            else
            {
                return serachedEvent.OrderBy(x => x.categories[0].title).ToList();
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