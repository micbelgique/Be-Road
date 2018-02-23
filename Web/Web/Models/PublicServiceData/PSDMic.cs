using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models.PublicServiceData
{
    public class PSDMic : PublicServiceData
    {
        public int Id { get; set; }
        public PSDData FirstName { get; set; }
        public PSDData LastName { get; set; }
        public PSDData Age { get; set; }
        public PSDData Locality { get; set; }
        public PSDData Nationality { get; set; }
    }
}