﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PublicService.Models.Dto
{
    public class DataDto
    {
        public string Value { get; set; }
        public List<AccessInfo> AccessInfos { get; set; }
    }
}