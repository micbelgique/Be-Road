using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class MockPSDPlace
    {
        [JsonProperty("place name")]
        public string PlaceName { get; set; }
        public double Longitude { get; set; }
        public string State { get; set; }
        [JsonProperty("state abbreviation")]
        public string StateAbbr { get; set; }
        public double Latitude { get; set; }
    }
}