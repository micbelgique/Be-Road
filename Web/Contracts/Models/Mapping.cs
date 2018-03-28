using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Models
{
    public class Mapping
    {
        public string InputKey { get; set; }
        public BeContract Contract { get; set; }
        public string ContractKey { get; set; }
    }
}
