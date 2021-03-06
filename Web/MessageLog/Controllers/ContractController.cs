﻿using Contracts.Models;
using LucidOcean.MultiChain;
using MessageLog.Dal;
using MessageLog.Helpers;
using MessageLog.Models;
using MessageLog.Models.Dto;
using MessageLog.Utils;
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
    [RoutePrefix("api/Contract")]
    public class ContractController : ApiController
    {
        private LogContext db;
        private AccessInfoService accessInfoService;

        public ContractController()
        {
            db = new LogContext(ConfigHelper.GetConnectionString("LogContext"));
            accessInfoService = new AccessInfoService(db);
        }

        [HttpGet]
        [Route("multichain")]
        public MultiChainConnection Get()
        {
            return MultichainUtils.Instance.GetConnection();
        }

        //GET: api/Contract/Get
        [HttpGet]
        [Route("Get/{page}")]
        public IHttpActionResult Get(int page = 1)
        {
            var logs = db.Logs.Where(l => l.Deter == Determiner.BeContract).ToList();
            var pagedLogs = new PagedList<Log>(logs, page, 50);

            return new ResponseMessageResult(Request.CreateResponse(pagedLogs));
        }

        // POST: api/Contract/Add
        [HttpPost]
        [Route("Add")]
        public async Task<IHttpActionResult> Add(ContractDtoGeneric logDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Log log = new Log()
            {
                Deter = Determiner.BeContract,
                ContractId = logDto.ContractId,
                UseType = "Create",
                Response = false,
                UserType = "DataProvider",
                UserName = logDto.UserName,
                Message = $"Creation of {logDto.ContractId}",
                CreationDate = DateTime.Now
            };

            var res = CheckLog(log);
            if (res != null)
            {
                return new ResponseMessageResult(
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
        [Route("Delete")]
        // POST: api/Contract/Delete
        public async Task<IHttpActionResult> Delete(ContractDtoGeneric logDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Log log = new Log()
            {
                Deter = Determiner.BeContract,
                ContractId = logDto.ContractId,
                UseType = "Delete",
                Response = false,
                UserType = "DataProvider",
                UserName = logDto.UserName,
                Message = $"Removal of {logDto.ContractId}",
                CreationDate = DateTime.Now
            };

            var res = CheckLog(log);
            if (res != null)
            {
                return new ResponseMessageResult(
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
        [Route("Call")]
        // POST: api/Contract/Call
        public async Task<IHttpActionResult> Call(ContractDtoCall logDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Log log = new Log()
            {
                Deter = Determiner.BeContract,
                ContractId = logDto.ContractId,
                UseType = "Call",
                Response = logDto.Response,
                UserType = logDto.UserType,
                UserName = logDto.UserName,
                Message = $"Call of {logDto.ContractId}",
                CreationDate = DateTime.Now
            };

            var res = CheckLog(log);
            if (res != null)
            {
                return new ResponseMessageResult(
                    Request.CreateErrorResponse(
                        (HttpStatusCode)422,
                        new HttpError($"Error in Log : {res} must contain a value")
                    )
                );
            }

            db.Logs.Add(log);
            
            //If the call has a justification, Store it 
            if((logDto.Inputs?.ContainsKey("Justification") ?? false)
                && (logDto.Inputs?.ContainsKey("NRID") ?? false))
            {
                await accessInfoService.LogAccessInfo(new AccessInfo()
                {
                    Date = log.CreationDate,
                    ContractId = log.ContractId,
                    NRID = logDto.Inputs["NRID"],
                    Name = log.UserName,
                    Justification = logDto.Inputs["Justification"]
                });
            }

            await db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.OK);
        }

        [HttpGet]
        [Route("justification")]
        public List<AccessInfoDto> GetJustificationAboutContract(string contractId, string nrid)
        {
            var lst = new List<AccessInfoDto>();
            var logs = db.AccessLogs
                .Where(log => log.NRID.Equals(nrid) && log.ContractId.Equals(contractId))
                .ToList();

            foreach(var log in logs)
            {
                lst.Add(new AccessInfoDto
                {
                    Date = log.Date,
                    Id = log.Id,
                    Name = log.Name,
                    Justification = log.Justification,
                    //Don't check the interity, it's to slow
                    IsReliable = true//await accessInfoService.IsReliableAsync(log)
                    }
                );
            }

            return lst;
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
