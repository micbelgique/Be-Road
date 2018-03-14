using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using MessageLog.Dal;
using MessageLog.Models;
using MessageLog.Models.Dto;
using MessageLog.Utils;

namespace MessageLog.Controllers
{
    public class AccessLogController : ApiController
    {
        private LogContext db = new LogContext();

        // POST: api/AccessLog
        [ResponseType(typeof(void))]
        public IHttpActionResult PostAccessInfo(List<ApplicationUserDto> appUserList)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(HttpStatusCode.BadRequest);
            }

            var accessInfoList = appUserList
                .SelectMany(user =>
                {
                    return user.LastName.AccessInfos
                        .Union(user.FirstName.AccessInfos)
                        .Union(user.LastName.AccessInfos)
                        .Union(user.BirthDate.AccessInfos)
                        .Union(user.Locality.AccessInfos)
                        .Union(user.Nationality.AccessInfos)
                        .Union(user.PhotoUrl.AccessInfos)
                        .Union(user.ExtraInfo.AccessInfos)
                        .Union(user.EmailAddress.AccessInfos);
                }
                ).Select(dto =>
                {
                    return new AccessInfo()
                    {
                        Id = dto.Id,
                        Hash = HashUtils.Instance.HashAccessLogDto(dto),
                        TransactionAddress = "DoPlzBlockchain"
                    };
                }).ToList();

            var existingLogs = db.AccessLogs
                .AsEnumerable()
                .Where(existingLog =>
                    accessInfoList.Select(newLog => newLog.Id)
                    .Contains(existingLog.Id))
                .ToList();

            existingLogs.ForEach(remainingLog =>
                {
                    var logToRemove = accessInfoList.SingleOrDefault(log => log.Id == remainingLog.Id);
                    if (!logToRemove.Hash.Equals(remainingLog.Hash))
                    {
                        // Intergity problem
                        System.Diagnostics.Debug.WriteLine("Integrity problem on log " + logToRemove.Id);
                    }

                    accessInfoList.Remove(logToRemove);
                });

            // Log in blockchain

            db.AccessLogs.AddRange(accessInfoList);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.OK);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AccessInfoExists(int id)
        {
            return db.AccessLogs.Count(e => e.Id == id) > 0;
        }
    }
}