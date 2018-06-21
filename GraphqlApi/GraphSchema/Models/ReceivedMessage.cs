using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphqlApi.GraphSchema.Models
{
	public class ReceivedMessage
	{
		public string FromId { get; set; }

		public string Content { get; set; }

		public DateTime SentAt { get; set; }
	}
}
