using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Web.Models;
using Web.Models.PublicServiceData;

namespace Web.Dal.Services
{
    public class MockGeoipService : IService
    {
        public Task<PublicServiceData> GetDataOfAsync(PublicService ps, EidCard eid)
        {
            throw new NotImplementedException();
        }
    }
}