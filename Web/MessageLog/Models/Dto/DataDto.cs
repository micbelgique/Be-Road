using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessageLog.Models.Dto
{
    public class DataDto
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public List<AccessInfoDto> AccessInfos { get; set; }
    }
}