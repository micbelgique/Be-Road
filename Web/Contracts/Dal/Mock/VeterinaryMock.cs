using Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Dal.Mock
{
    class VeterinaryMock
    {
        public static void GetOwnerId(BeContractCall call)
        {
            switch(call.Inputs["DogID"])
            {
                case "D-123": Console.WriteLine("Wilson !"); break;
                case "D-122": Console.WriteLine("Mika !"); break;
                case "D-124": Console.WriteLine("Flo"); break;
                case "D-126": Console.WriteLine("Pierre"); break;
                default : Console.WriteLine("Incognito"); break;
            }
        }
    }
}
