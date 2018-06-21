using GraphQL.Types;
using GraphqlApi.GraphSchema.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphqlApi.GraphSchema.GraphTypes
{
	public class MessageFromType : ObjectGraphType<MessageFrom>
	{
		public MessageFromType()
		{
			Field(o => o.Id);
			Field(o => o.DisplayName);
		}
	}
}
