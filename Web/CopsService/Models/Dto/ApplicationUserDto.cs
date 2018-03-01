using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PublicService.Models.Dto
{
    public class ApplicationUserDto
    {
        public DataDto FirstName { get; set; }
        public DataDto LastName { get; set; }
        public DataDto BirthDate { get; set; }
        public DataDto Locality { get; set; }
        public DataDto Nationality { get; set; }
        public DataDto PhotoUrl { get; set; }
        public DataDto ExtraInfo { get; set; }
        public DataDto EmailAddress { get; set; }
    }
}