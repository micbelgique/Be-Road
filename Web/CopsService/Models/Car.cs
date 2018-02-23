using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PublicService.Models
{
    public class Car
    {
        public int Id { get; set; }
        public virtual Data Owner { get; set; }
        public virtual Data NumberPlate { get; set; }
        public virtual Data Brand { get; set; }
    }
}