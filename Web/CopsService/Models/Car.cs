using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PublicService.Models
{
    public class Car
    {
        public int Id { get; set; }
        public virtual ApplicationUser Owner { get; set; }
    }
}