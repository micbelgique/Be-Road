using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PublicService.Models.Dto
{
    public class ApplicationUserDto
    {
        public Data FirstName { get; set; }
        public Data LastName { get; set; }
        public Data BirthDate { get; set; }
        public Data Locality { get; set; }
        public Data Nationality { get; set; }
        public Data PhotoUrl { get; set; }
        public Data ExtraInfo { get; set; }
        public Data EmailAddress { get; set; }
    }
}