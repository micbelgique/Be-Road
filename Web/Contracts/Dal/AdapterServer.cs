using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Models
{
    /// <summary>
    /// Class representing an adapter server
    /// </summary>
    public class AdapterServer
    {
        /// <summary>
        /// Names of the contracts handled by the Information System
        /// </summary>
        public List<string> ContractNames { get; set; }
        /// <summary>
        /// Name of the Information System
        /// </summary>
        public string ISName { get; set; }
        /// <summary>
        /// URL of the adapter server API
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Calls the api of the Information System
        /// </summary>
        public void Call(string id)
        {
            Console.WriteLine($"Calling {0}", ISName);
        }
    }
}
