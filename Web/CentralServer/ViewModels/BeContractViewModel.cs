using Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentralServer.ViewModels
{
    public class BeContractViewModel
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string Version { get; set; }
        public virtual List<Input> Inputs { get; set; }
        public virtual List<QueryViewModel> Queries { get; set; }
        public virtual List<Output> Outputs { get; set; }
    }
}