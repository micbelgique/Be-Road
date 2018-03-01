using PublicService.Dal;
using PublicService.Models;
using PublicService.Models.Dto;
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
    public class ApplicationUserController : ApiController
    {
        private PSContext db = new PSContext();
        private AzureUpload azureUpload = new AzureUpload();

        // GET: api/ApplicationUser
        public IQueryable<ApplicationUserDto> GetUsers()
        {
            var users = db.Users;
            var dtos = users.Select(user => new ApplicationUserDto()
            {
                FirstName = new DataDto() { Value = user.FirstName.Value, AccessInfos = user.FirstName.AccessInfos },
                LastName = new DataDto() { Value = user.LastName.Value, AccessInfos = user.LastName.AccessInfos },
                BirthDate = new DataDto() { Value = user.BirthDate.Value, AccessInfos = user.BirthDate.AccessInfos },
                Locality = new DataDto() { Value = user.Locality.Value, AccessInfos = user.Locality.AccessInfos },
                Nationality = new DataDto() { Value = user.Nationality.Value, AccessInfos = user.Nationality.AccessInfos },
                PhotoUrl = new DataDto() { Value = user.PhotoUrl.Value, AccessInfos = user.PhotoUrl.AccessInfos },
                ExtraInfo = new DataDto() { Value = user.ExtraInfo.Value, AccessInfos = user.ExtraInfo.AccessInfos },
                EmailAddress = new DataDto() { Value = user.EmailAddress.Value, AccessInfos = user.EmailAddress.AccessInfos }
            });
            return dtos;
        }

        // PUT: api/ApplicationUser
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUploadToAzure()
        {
            await azureUpload.UploadToAzureAsync(db);
            return StatusCode(HttpStatusCode.OK);
        }
    }
}