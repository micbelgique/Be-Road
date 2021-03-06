﻿using MessageLog.Dal;
using MessageLog.Helpers;
using MessageLog.Models;
using MessageLog.Models.Dto;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace MessageLog.Controllers
{
    [RoutePrefix("api/AdapterServer")]
    public class AdapterServerController : ApiController
    {
        private LogContext db = new LogContext(ConfigHelper.GetConnectionString("LogContext"));

        //GET: api/AdapterServer/Get
        [HttpGet]
        [Route("Get/{page}")]
        public IHttpActionResult Get(int page = 1)
        {
            var logs = db.Logs.Where(l => l.Deter == Determiner.AdapterServer).ToList();
            var pagedLogs = new PagedList<Log>(logs, page, 50);

            return new ResponseMessageResult(Request.CreateResponse(pagedLogs));
        }

        // POST: api/AdapterServer/Add
        [HttpPost]
        [Route("Add")]
        public async Task<IHttpActionResult> Add(AdsDtoGeneric logDto)
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
                UserType = logDto.UserType,
                UserName = logDto.UserName,
                Message = $"Creation of {logDto.UserName}'s ADS",
                CreationDate = DateTime.Now
            };

            var res = CheckLog(log);
            if (res != null)
            {
                return new System.Web.Http.Results.ResponseMessageResult(
                    Request.CreateErrorResponse(
                        (HttpStatusCode)422,
                        new HttpError($"Error in Log : {res} must contain a value")
                    )
                );
            }

            db.Logs.Add(log);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.OK);
        }

        [HttpPost]
        [Route("Update")]
        // PUT: api/AdapterServer/Update
        public async Task<IHttpActionResult> Update(AdsDtoGeneric logDto)
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
                UserType = logDto.UserType,
                UserName = logDto.UserName,
                Message = $"Update of {logDto.UserName}'s ADS",
                CreationDate = DateTime.Now
            };

            var res = CheckLog(log);
            if (res != null)
            {
                return new System.Web.Http.Results.ResponseMessageResult(
                    Request.CreateErrorResponse(
                        (HttpStatusCode)422,
                        new HttpError($"Error in Log : {res} must contain a value")
                    )
                );
            }

            db.Logs.Add(log);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.OK);
        }

        // DELETE: api/AdapterServer/Delete
        [HttpPost]
        [Route("Delete")]
        public async Task<IHttpActionResult> Delete(AdsDtoDelete logDto)
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
                UserName = logDto.UserName,
                Message = $"Removal of {logDto.UserName}'s ADS",
                CreationDate = DateTime.Now
            };

            var res = CheckLog(log);
            if (res != null)
            {
                return new System.Web.Http.Results.ResponseMessageResult(
                    Request.CreateErrorResponse(
                        (HttpStatusCode)422,
                        new HttpError($"Error in Log : {res} must contain a value")
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
