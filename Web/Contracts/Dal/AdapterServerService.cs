using Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Dal.Mock
{
    /// <summary>
    /// Class used to find an adapter server
    /// </summary>
    public class AdapterServerService
    {
        /// <summary>
        /// List of the adapter servers
        /// </summary>
        public List<AdapterServer> ADSList { get; set; }

        /// <summary>
        /// Find an adapter server with the name of the contract used
        /// </summary>
        /// <param name="name">The name of the contract used</param>
        /// <returns>The found adapter server</returns>
        public AdapterServer FindAS(string name)
        {
            return ADSList.FirstOrDefault(s => s.ContractNames.Any(cn => cn.Equals(name)));
        }
    }
}
