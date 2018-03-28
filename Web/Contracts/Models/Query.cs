using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Models
{
    public class Query
    {
        public BeContract Contract { get; set; }
        public List<Mapping> Mappings { get; set; }
    }
}
