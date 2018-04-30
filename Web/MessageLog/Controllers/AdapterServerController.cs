using MessageLog.Dal;
using MessageLog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MessageLog.Controllers
{
    public class AdapterServerController : ApiController
    {
        private LogContext db = new LogContext();

        // POST: api/AdapterServer/Add
        [HttpPost]
        public async Task<IHttpActionResult> Add([FromBody]string userType, [FromBody]string userName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Log log = new Log()
            {
                Deter = Determiner.AdapterServer,
                ContractId = "None",
                UseType = "Create",
                Response = false,
                UserType = userType,
                UserName = userName,
                Message = $"Creation of {userName}'s ADS",
                CreationDate = DateTime.Now
            };

            var res = CheckLog(log);
            if (res != null)
            {
                return new System.Web.Http.Results.ResponseMessageResult(
                    Request.CreateErrorResponse(
                        (HttpStatusCode)422,
                        new HttpError($"Error in Log : {res} cannot must contain a value")
                    )
                );
            }

            db.Logs.Add(log);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.OK);
        }

        // PUT: api/AdapterServer/Update
        public async Task<IHttpActionResult> Update([FromBody]string userType, [FromBody]string userName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Log log = new Log()
            {
                Deter = Determiner.AdapterServer,
                ContractId = "None",
                UseType = "Update",
                Response = false,
                UserType = userType,
                UserName = userName,
                Message = $"Update of {userName}'s ADS",
                CreationDate = DateTime.Now
            };

            var res = CheckLog(log);
            if (res != null)
            {
                return new System.Web.Http.Results.ResponseMessageResult(
                    Request.CreateErrorResponse(
                        (HttpStatusCode)422,
                        new HttpError($"Error in Log : {res} cannot must contain a value")
                    )
                );
            }

            db.Logs.Add(log);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.OK);
        }

        // DELETE: api/AdapterServer/Delete
        [HttpPost]
        public async Task<IHttpActionResult> Delete([FromBody]string userName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Log log = new Log()
            {
                Deter = Determiner.AdapterServer,
                ContractId = "None",
                UseType = "Delete",
                Response = false,
                UserType = "Admin",
                UserName = userName,
                Message = $"Removal of {userName}'s ADS",
                CreationDate = DateTime.Now
            };

            var res = CheckLog(log);
            if (res != null)
            {
                return new System.Web.Http.Results.ResponseMessageResult(
                    Request.CreateErrorResponse(
                        (HttpStatusCode)422,
                        new HttpError($"Error in Log : {res} cannot must contain a value")
                    )
                );
            }

            db.Logs.Add(log);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.OK);
        }

        private string CheckLog(Log log)
        {
            if (log.Deter == Determiner.Undefined)
                return "Determiner";

            if (log.ContractId == null || log.ContractId == "")
                return "ContractId";

            if (log.UseType == null || log.UseType == "")
                return "UseType";

            if (log.UserType == null || log.UserType == "")
                return "UserType";

            if (log.UserName == null || log.UserName == "")
                return "UserName";

            if (log.CreationDate == null)
                return "CreationDate";

            return null;
        }
    }
}
