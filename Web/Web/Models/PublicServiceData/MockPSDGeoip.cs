using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models.PublicServiceData
{
    public class MockPSDGeoip : PublicServiceData
    {
        public string Ip { get; set; }
        public string CounrtyCode { get; set; }
        public string CountryName { get; set; }
        public string RegionCode { get; set; }
        public string RegionName { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string TmeZone { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string MetroCode { get; set; }
    }
}