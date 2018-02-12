using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Web.Dal.Services;
using Web.Models;
using Web.Models.PublicServiceData;

namespace Web.Dal
{
    public class PSDataService
    {
        private Dictionary<string, IService> Services { get; set; }
        public PSDataService()
        {
            Services = new Dictionary<string, IService>
            {
                { "zippo", new MockZippopotamService() },
                { "geoip", new MockGeoipService() },
                { "itunes", new MockItunesService() },
                { "communal", new MockCommunalService() },
                { "firstnames", new MockFirstNamesService() }
            };
        }

        public async Task<PublicServiceData> GetDataOfAsync(PublicService ps, EidCard eid)
        {
            var service = Services[ps.DalMethod];
            if(service == null) return null;
            return await service.GetDataOfAsync(ps, eid);
        }
    }
}