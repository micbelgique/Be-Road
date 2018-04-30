using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessageLog.Models.Dto
{
    public class ExceptionDto
    {
        public string UserName { get; set; }
        public string UserType { get; set; }
        public string Message { get; set; }
    }
}