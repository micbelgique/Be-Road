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
                { "Cops", new CopsService() },
                { "Mic", new MicService() },
                { "Zippo", new MockZippopotamService() },
                { "Geoip", new MockGeoipService() },
                { "Itunes", new MockItunesService() },
                { "Firstnames", new MockFirstNamesService() },
                { "Azure", new MockAzureService() }
            };
        }

        public async Task<PublicServiceData> GetDataOfAsync(PublicService ps, EidCard eid)
        {
            if (!Services.ContainsKey(ps.DalMethod))
                return null;
            var service = Services[ps.DalMethod];
            return await service.GetDataOfAsync(ps, eid);
        }
    }
}