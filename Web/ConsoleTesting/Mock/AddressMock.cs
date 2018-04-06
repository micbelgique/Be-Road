using Contracts.Models;
using System.Collections.Generic;

namespace ConsoleTesting.Mock
{
    class AddressMock
    {
        public static BeContractReturn ReturnAnswer(string ownerId, int number, string country)
        {
            return new BeContractReturn()
            {
                Id = "GetAddressByOwnerId",
                Outputs = new Dictionary<string, dynamic>()
                {
                    { "Street", ownerId},
                    { "StreetNumber", number},
                    { "Country", country}
                }
            };
        }

        public static BeContractReturn GetAddressByOwnerId(BeContractCall call)
        {
            switch (call.Inputs["OwnerID"])
            {
                case "Wilson !": return ReturnAnswer("Charleroi nord", 9999, "Belgique");
                case "Mika !": return ReturnAnswer("Bxl", 1080, "Belgique");
                case "Flo": return ReturnAnswer("Charleroi Centre", 1000, "Belgique");
                case "Pierre": return ReturnAnswer("Charleroi Central", 5000, "Belgique");
                default : return ReturnAnswer("SDF", 0, "SDF");
            }
        }
    }
}
