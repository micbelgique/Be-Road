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
    public class ContractController : ApiController
    {
        private LogContext db = new LogContext();

        // POST: api/Contract/Add
        [HttpPost]
        public async Task<IHttpActionResult> Add([FromBody]string contractId, [FromBody]string userName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Log log = new Log()
            {
                Deter = Determiner.BeContract,
                ContractId = contractId,
                UseType = "Create",
                Response = false,
                UserType = "DataProvider",
                UserName = userName,
                Message = $"Creation of {contractId}",
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

        [HttpPost]
        // POST: api/Contract/Delete
        public async Task<IHttpActionResult> Delete([FromBody]string contractId, [FromBody]string userName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Log log = new Log()
            {
                Deter = Determiner.BeContract,
                ContractId = contractId,
                UseType = "Delete",
                Response = false,
                UserType = "DataProvider",
                UserName = userName,
                Message = $"Removal of {contractId}",
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

        [HttpPost]
        // POST: api/Contract/Cal
        public async Task<IHttpActionResult> Call([FromBody]string contractId, [FromBody]string userName, [FromBody]string userType, [FromBody]bool response)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Log log = new Log()
            {
                Deter = Determiner.BeContract,
                ContractId = contractId,
                UseType = "Call",
                Response = response,
                UserType = userType,
                UserName = userName,
                Message = $"Call of {contractId}",
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
