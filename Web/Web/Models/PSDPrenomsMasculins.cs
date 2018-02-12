﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class PSDPrenomsMasculins : PublicServiceData
    {
        public string SearchedName { get; set; }
        public int Count { get; set; }
        public string TimeZone { get; set; }
        public string SortingMethod { get; set; }
    }
}