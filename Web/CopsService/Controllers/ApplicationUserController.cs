using PublicService.Dal;
using PublicService.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace PublicService.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ApplicationUserController : ApiController
    {
        private PSContext db = new PSContext();

        
    }
}