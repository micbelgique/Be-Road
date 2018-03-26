﻿using Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public class BeContractException : Exception
    {
        public BeContract BeContract { get; set; }
        public BeContractCall BeContractCall { get; set; }

        public BeContractException(string msg) : base(msg) { }
    }
}