using Contracts.Models;
using System;

namespace Contracts
{
    public class BeContractException : Exception
    {
        public BeContract BeContract { get; set; }
        public BeContractCall BeContractCall { get; set; }
        public BeContractReturn BeContractReturn { get; set; }

        public BeContractException(string msg) : base(msg) { }
    }
}
