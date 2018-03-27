using Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Dal.Mock
{
    class AddressMock
    {
        public static void GetAddressByOwnerId(BeContractCall call)
        {
            switch (call.Inputs["OwnerID"])
            {
                case "Wilson !": Console.WriteLine("Charleroi Nord"); break;
                case "Mika !": Console.WriteLine("Bxl"); break;
                case "Flo": Console.WriteLine("Charleroi Sud"); break;
                case "Pierre": Console.WriteLine("Charleroi Centre"); break;
                default : Console.WriteLine("SDF"); break;
            }
        }
    }
}
