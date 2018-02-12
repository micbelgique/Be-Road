using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Models;
using Web.Models.PublicServiceData;

namespace Web.Dal
{
    interface IService
    {
        Task<PublicServiceData> GetDataOfAsync(PublicService ps, EidCard eid);
    }
}
