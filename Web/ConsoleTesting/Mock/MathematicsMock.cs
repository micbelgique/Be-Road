﻿using Contracts.Models;
using System;
using System.Collections.Generic;

namespace ConsoleTesting.Mock
{
    class MathematicsMock
    {
        public static BeContractReturn GetSumFunction(BeContractCall call)
        {
            var a = Convert.ToInt32(call.Inputs["A"]);
            var b = Convert.ToInt32(call.Inputs["B"]);
            return new BeContractReturn()
            {
                Id = "GetMathemathicFunction",
                Outputs = new Dictionary<string, dynamic>()
                {
                    { "Total", a + b },
                    { "Formula", "Total = A + B"}
                }
            };
        }
    }
}
