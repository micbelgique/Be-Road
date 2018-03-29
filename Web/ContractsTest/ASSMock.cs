using Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Dal.Mock
{
    /// <summary>
    /// Class used to fill the Adapter Server list for the tests
    /// </summary>
    class ASSMock
    {
        /// <summary>
        /// Fills the the Adapter Server list
        /// </summary>
        /// <returns>List of Adapter Servers</returns>
        public static List<AdapterServer> Fill()
        {
            List<AdapterServer> asList = new List<AdapterServer>();

            asList.Add(new AdapterServer() { ISName = "Doggies", Url = "www.doggies.com/api/getownerbydoggy" });
            asList.Add(new AdapterServer() { ISName = "MathLovers", Url = "www.mathlovers.com/api/getmathfunc" });
            asList.Add(new AdapterServer() { ISName = "CitizenDatabank", Url = "www.doggies.com/api/getaddrbycitizen" });
            asList.Add(new AdapterServer() { ISName = null, Url = null });

            return asList;
        }
    }
}
