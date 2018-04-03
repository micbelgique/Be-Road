using Proxy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proxy
{
    public class BeContractException : Exception
    {
        public BeContract BeContract { get; set; }
        public BeContractCall BeContractCall { get; set; }
        public BeContractReturn BeContractReturn { get; set; }

        public BeContractException(string msg) : base(msg) { }
    }
}
