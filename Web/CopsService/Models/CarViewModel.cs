using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PublicService.Models
{
    public class CarViewModel
    {
        public virtual ApplicationUser Owner { get; set; }
        public virtual string NumberPlate { get; set; }
        public virtual string Brand { get; set; }
    }
}