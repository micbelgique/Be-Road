using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models.PublicServiceData
{
    public class PSDCar : PublicServiceData
    {
        public int Id { get; set; }
        public virtual PSDData Owner { get; set; }
        public virtual PSDData NumberPlate { get; set; }
        public virtual PSDData Brand { get; set; }
    }
}