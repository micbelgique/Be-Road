using Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Dal.Mock
{
    public class CentralServer
    {
        /// <summary>
        /// Finds the Mock (Temporary)
        /// </summary>
        /// <returns>The wanted mock class</returns>
        public static BeContractReturn FindMock(AdapterServer ads, BeContractCall call)
        {
            BeContractReturn res = null;

            if (AuthorisationCheck(ads))
            {
                switch (ads.ISName)
                {
                    case "CitizenDatabank":
                        res = AddressMock.GetAddressByOwnerId(call);
                        break;
                    case "MathLovers":
                        res = MathematicsMock.GetSumFunction(call);
                        break;
                    case "Doggies":
                        res = VeterinaryMock.GetOwnerId(call);
                        break;
                }
            }
            else
            {
                throw new BeContractException($"Unauthorized access for {ads.ISName}");
            }

            return res;
        }

        private static bool AuthorisationCheck(AdapterServer ads)
        {
            Console.WriteLine("Authorizing process !");
            if (ads.ISName.Equals("Fail"))
                return false;

            return true;
        }
    }
}
