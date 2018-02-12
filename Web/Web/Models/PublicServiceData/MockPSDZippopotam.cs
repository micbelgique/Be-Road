using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models.PublicServiceData
{
    public class MockPSDZippopotam : PublicServiceData
    {
        [JsonProperty("post code")]
        public string PostCode { get; set; }
        public string Country { get; set; }
        [JsonProperty("country abbreviation")]
        public string CountryAbbr { get; set; }
        public MockPSDPlace[] Places { get; set; }
    }
}