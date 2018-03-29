﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Models
{
    /// <summary>
    /// Class representing a contract
    /// </summary>
    public class BeContract
    {
        /// <summary>
        /// Used to recognize the contract (Required)
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string Id { get; set; }
        /// <summary>
        /// Describes the contract goal
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Specifies the version of the contract
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// Values used by the contract
        /// </summary>
        public List<Input> Inputs { get; set; }
        /// <summary>
        /// Other contracts to call to get the needed data
        /// </summary>
        public List<Query> Queries { get; set; }
        /// <summary>
        /// Values returned by the contract
        /// </summary>
        public List<Output> Outputs { get; set; }
    }
}
