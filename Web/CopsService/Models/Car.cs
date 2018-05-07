using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PublicService.Models
{
    public class Car
    {
        public int Id { get; set; }
        public virtual string Owner { get; set; }
        public virtual string NumberPlate { get; set; }
        public virtual string Brand { get; set; }
    }
}