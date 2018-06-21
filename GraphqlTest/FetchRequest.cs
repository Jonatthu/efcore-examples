using System;
using System.Collections.Generic;
using System.Text;

namespace GraphqlTest
{
    public class FetchRequest
    {
		public int First { get; set; }
		public int Skip { get; set; }
		public FilterRequest Filter { get; set; }
		public NamePropertyArguments NamePropertyArguments { get; set; }
	}
}
