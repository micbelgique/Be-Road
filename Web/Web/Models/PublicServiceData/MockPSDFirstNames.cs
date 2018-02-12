using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models.PublicServiceData
{
    public class MockPSDFirstNames : PublicServiceData
    {
        public List<MockPSDRecord> Records { get; set; }
    }

    public class MockPSDRecord
    {
        public string SearchedName { get; set; }
        public int Count { get; set; }
    }
}