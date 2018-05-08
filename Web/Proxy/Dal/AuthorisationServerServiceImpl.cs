using Contracts.Dal;
using Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Proxy.Dal
{
    public class AuthorisationServerServiceImpl : IAuthorisationServerService
    {
        //TODO: Call the Authorisation server here !
        public async Task<bool> CanInformationSystemUseContract(string isName, BeContract contract)
        {    
            bool canUse = true;
            await Task.Run(() => 
            {
                switch(contract.Id)
                {
                    case "GetServiceInfo": if (isName.Equals("Insomnia Client") || isName.Equals("Public Service")) canUse = false; break;
                    case "GetPopulationContract": if (isName.Equals("Postman")) canUse = false; break;
                    case "GetDivContract": if (isName.Equals("Insomnia Client")) canUse = false; break;
                    case "GetBankContract": if (isName.Equals("MIC")) canUse = false; break;
                }
            });
            return canUse;
        }
    }
}