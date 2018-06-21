using GraphQL.Types;
using GraphqlApi.GraphSchema.GraphTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphqlApi.GraphSchema
{
	public class ChatQuery : ObjectGraphType
	{
		public ChatQuery(IChat chat)
		{
			Field<ListGraphType<MessageType>>("messages", resolve: context => chat.AllMessages.Take(100));
		}
	}
}
