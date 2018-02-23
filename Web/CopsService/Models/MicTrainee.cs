using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PublicService.Models
{
    public class MicTrainee
    {
        public int Id { get; set; }
        public virtual Data FirstName { get; set; }
        public virtual Data LastName { get; set; }
        public virtual Data Age { get; set; }
        public virtual Data Locality { get; set; }
        public virtual Data Nationality { get; set; }
    }
}