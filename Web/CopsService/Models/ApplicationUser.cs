using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PublicService.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace PublicService.Models
{
    public class ApplicationUser : IdentityUser
    {
        public virtual string PhotoUrl { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authType)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, authType);
            return userIdentity;
        }
    }
}