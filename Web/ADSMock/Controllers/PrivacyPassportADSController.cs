﻿using Contracts.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Routing;

namespace ADSMock.Controllers
{
    [RoutePrefix("api")]
    public class PrivacyPassportADSController : ApiController
    {
        private Dictionary<string, string> bankAccounts = new Dictionary<string, string>
        {
            { "93011150162", "BE08 1234 2315 9012"},
            { "97081817718", "BE08 3575 5678 5972"},
            { "95052316256", "BE08 4567 5275 9752"},
            { "97010215411", "BE67 5678 2937 1456"},
            { "96122400226", "BE24 5623 1028 1003"},
            { "93020623433", "BE08 1234 5475 4626"},
            { "95071956583", "BE08 9873 5678 9012"},
        };

        private Dictionary<string, dynamic> funny = new Dictionary<string, dynamic>
        {
            { "93011150162", new { ExtraInfo = "Programmer", Email = "michael@mic.be"} },
            { "97081817718", new { ExtraInfo = "Rien.", Email = "wilson@mic.be"}},
            { "95052316256", new { ExtraInfo = "Patate", Email = "raph@mic.be"}},
            { "97010215411", new { ExtraInfo = "Pepito", Email = "pierre@mic.be"}},
            { "96122400226", new { ExtraInfo = "Hypetrain", Email = "fred@mic.be"}},
            { "93020623433", new { ExtraInfo = "", Email = "mo@mic.be"}},
            { "95071956583", new { ExtraInfo = "Designer !", Email = "nico@mic.be"}},
            { "", new { ExtraInfo = "No info", Email = "No Info"} }
        };

        private Dictionary<string, dynamic> div = new Dictionary<string, dynamic>
        {
            { "93011150162", new { NumberPlate = "1-EBD-684", Brand = "Nissan"} },
            { "97081817718", new { NumberPlate = "1-OZA-014", Brand = "Porsche"}},
            { "95052316256", new { NumberPlate = "1-PAT-427", Brand = "Mercedes"}},
            { "97010215411", new { NumberPlate = "1-PEP-983", Brand = "Fiat"}},
            { "96122400226", new { NumberPlate = "1-HPT-918", Brand = "Audi"}},
            { "93020623433", new { NumberPlate = "1-VNK-646", Brand = "Citroën"}},
            { "95071956583", new { NumberPlate = "1-ZAR-755", Brand = "Mercedes"}},
            { "", new { NumberPlate = "No info", Brand = "No Info"} }
        };

        private Dictionary<string, dynamic> population = new Dictionary<string, dynamic>
        {
            { "93011150162", new { FirstName = "Mika", LastName = "VM", Birthday = "11 Jan 93", Locality = "Bxl", Nationality = "Belge"} },
            { "97081817718", new { FirstName = "Wilson", LastName = "Wiwi", Birthday = "20 Jan 93", Locality = "Charleroi", Nationality = "Belge"}},
            { "95052316256", new { FirstName = "Raph", LastName = "Disp", Birthday = "23 May 95", Locality = "Bxl", Nationality = "Belge"}},
            { "97010215411", new { FirstName = "Pierre", LastName = "Stal", Birthday = "02 Jan 97", Locality = "Bxl", Nationality = "Belge"}},
            { "96122400226", new { FirstName = "Fred", LastName = "Carb", Birthday = "24 Dec 96", Locality = "Bxl", Nationality = "Belge"}},
            { "93020623433", new { FirstName = "Mo", LastName = "Lou", Birthday = "10 April 95", Locality = "Charleroi", Nationality = "Maroc"}},
            { "95071956583", new { FirstName = "Nico", LastName = "Vervloe", Birthday = "30 Dec 91", Locality = "Louvain", Nationality = "Suisse"}},
            { "", new { FirstName = "No info", LastName = "No Info", Birthday = "No Info", Locality = "No Info", Nationality = "No Info"} }
        };

        [HttpPost]
        [Route("bank/GetBankContract")]
        public BeContractReturn GetBankContract(BeContractCall call)
        {
            return new BeContractReturn()
            {
                Id = "GetBankContract",
                Outputs = new Dictionary<string, dynamic>()
                {
                    { "Bankaccount", bankAccounts.FirstOrDefault(b => b.Key.Equals(call.Inputs["NRID"])).Value ?? "No account here"}
                }
            };
        }

        private Dictionary<String, dynamic> Dyn2Dict(dynamic dynObj)
        {
            var dictionary = new Dictionary<string, object>();
            foreach (PropertyDescriptor propertyDescriptor in TypeDescriptor.GetProperties(dynObj))
            {
                object obj = propertyDescriptor.GetValue(dynObj);
                dictionary.Add(propertyDescriptor.Name, obj);
            }
            return dictionary;
        }

        private Dictionary<string, dynamic> GetOutputsOf(Dictionary<string, dynamic> db, string nrid)
        {
            var keyVal = db.FirstOrDefault(b => b.Key.Equals(nrid)).Value;
            var dict = Dyn2Dict(keyVal);
            if(dict == null || dict.Count == 0)
                dict = Dyn2Dict(db.FirstOrDefault(b => b.Key.Equals("")).Value);
            return dict;
        }

        [HttpPost]
        [Route("funny/GetFunnyContract")]
        public BeContractReturn GetFunnyContract(BeContractCall call) => new BeContractReturn()
        {
            Id = "GetFunnyContract",
            Outputs = GetOutputsOf(funny, call.Inputs["NRID"])
        };

        [HttpPost]
        [Route("div/GetDivContract")]
        public BeContractReturn GetDivContract(BeContractCall call) => new BeContractReturn()
        {
            Id = "GetDivContract",
            Outputs = GetOutputsOf(div, call.Inputs["NRID"])
        };

        [HttpPost]
        [Route("population/GetPopulationContract")]
        public BeContractReturn GetPopulationContract(BeContractCall call) => new BeContractReturn()
        {
            Id = "GetPopulationContract",
            Outputs = GetOutputsOf(population, call.Inputs["NRID"])
        };
    }
}