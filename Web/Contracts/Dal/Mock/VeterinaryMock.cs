﻿using Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Dal.Mock
{
    class VeterinaryMock
    {
        public static BeContractReturn ReturnAnswer(string ownerId)
        {
            return new BeContractReturn()
            {
                Id = "GetOwnerIdByDogId",
                Outputs = new Dictionary<string, dynamic>()
                {
                    { "OwnerIDOfTheDog", ownerId}
                }
            };
        }

        public static BeContractReturn GetOwnerId(BeContractCall call)
        {
            switch(call.Inputs["DogID"])
            {
                case "D-123": return ReturnAnswer("Wilson !");
                case "D-122": return ReturnAnswer("Mika !");
                case "D-124": return ReturnAnswer("Flo !");
                case "D-126": return ReturnAnswer("Pierre !");
                default : return ReturnAnswer("Incognito !");
            }
        }
    }
}