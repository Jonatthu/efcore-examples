using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp4
{
    public class User
    {
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string UserName { get; set; }

		public List<Blogg> Blogs { get; set; }
	}
}
