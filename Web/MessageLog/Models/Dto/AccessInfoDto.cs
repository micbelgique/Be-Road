﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessageLog.Models.Dto
{
    public class AccessInfoDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Reason { get; set; }
        public DateTime Date { get; set; }
    }
}