﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CopsService.Models
{
    public class Data
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<AccessInfo> AccessInfos { get; set; }
    }
}