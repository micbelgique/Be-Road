﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Models.Dto;

namespace Web.Models
{
    public class PublicServiceData
    {
        public string NRID { get; set; }
        public Dictionary<string, dynamic> Datas { get; set; }
    }
}