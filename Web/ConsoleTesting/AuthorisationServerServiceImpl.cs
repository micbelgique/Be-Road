using Contracts.Dal;
using Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTesting
{
    class AuthorisationServerServiceImpl: IAuthorisationServerService
    {
        public async Task<bool> CanInformationSystemUseContract(string isName, BeContract contract)
        {
            bool canUse = true;
            await Task.Run(() =>
            {
                canUse = true;
            });
            return canUse;
        }
    }
}
