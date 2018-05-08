using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PublicService.Models
{
    public class ManageViewModel
    {
        public string UserName { get; set; }

        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Display(Name = "Day")]
        [StringLength(2, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string BirthDateD { get; set; }

        [Display(Name = "Month")]
        public string BirthDateM { get; set; }

        [Display(Name = "Year")]
        public string BirthDateY { get; set; }

        [Display(Name = "Email address")]
        public string EmailAddress { get; set; }

        [Display(Name = "Locality")]
        public string Locality { get; set; }

        [Display(Name = "Nationality")]
        public string Nationality { get; set; }

        [Display(Name = "Photo URL")]
        public string PhotoUrl { get; set; }

        [Display(Name = "Extra")]
        public string ExtraInfo { get; set; }
    }
}