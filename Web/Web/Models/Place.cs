using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class Place
    {
        public string PlaceName { get; set; }
        public double Longitude { get; set; }
        public string State { get; set; }
        public string StateAbbr { get; set; }
        public double Latitude { get; set; }
    }
}