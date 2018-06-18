using System;
using System.Collections.Generic;
using System.Text;

namespace GraphqlTest
{
    public class FilterRequest
    {
		public IEnumerable<string> Ids { get; set; }
		public string Search { get; set; }
	}
}
