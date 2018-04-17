﻿using Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Dal
{
    public interface IAdapterServerService
    {
        AdapterServer FindAS(string name);
        Task<BeContractReturn> CallAsync(BeContractCall call);
        Task<BeContractReturn> FindAsync(AdapterServer ads, BeContractCall call);
    }
}