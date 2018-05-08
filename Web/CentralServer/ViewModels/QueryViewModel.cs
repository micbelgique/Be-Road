using Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentralServer.ViewModels
{
    public class QueryViewModel
    {
        public string Contract { get; set; }
        public List<Mapping> Mappings { get; set; }
    }
}