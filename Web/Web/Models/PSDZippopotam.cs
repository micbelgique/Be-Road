using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class PSDZippopotam : PublicServiceData
    {
        public string PostCode { get; set; }
        public string Country { get; set; }
        public string CountryAbbr { get; set; }
        public Place[] Places { get; set; }
    }
}