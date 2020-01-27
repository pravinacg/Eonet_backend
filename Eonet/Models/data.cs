using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EONET.Models
    {
        public class ResponseClass
        {

            public class Category
            {
                public int id { get; set; }
                public string title { get; set; }
            }

            public class Source
            {
                public string id { get; set; }
                public string url { get; set; }
            }

            public class Geometry
            {
                public DateTime date { get; set; }
                public string type { get; set; }
                public List<dynamic> coordinates { get; set; }
            }

            public class Event
            {
                public string id { get; set; }
                public string title { get; set; }
                public string description { get; set; }
                public string link { get; set; }

               public List<Category> categories { get; set; }
               public List<Source> sources { get; set; }
                public List<Geometry> geometries { get; set; }
                
                public DateTime? closed { get; set; }
            }

            public class RootObject
            {
                public string title { get; set; }
                public string description { get; set; }
                public string link { get; set; }
                public List<Event> events { get; set; }
            }
        }
    }

