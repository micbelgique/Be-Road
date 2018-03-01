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

namespace PublicService.Controllers
{
    public class ApplicationUserController : ApiController
    {
        private PSContext db = new PSContext();

        // GET: api/Users
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
       
        /*// GET: api/Users/5
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> GetUser(int id)
        {
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.Id)
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Users
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Users.Add(user);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> DeleteUser(int id)
        {
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            await db.SaveChangesAsync();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.Id == id) > 0;
        }*/
    }
}