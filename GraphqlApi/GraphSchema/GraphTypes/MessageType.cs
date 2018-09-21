using GraphQL.Types;
using GraphqlApi.GraphSchema.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphqlApi.GraphSchema.GraphTypes
{
	public class MessageType : ObjectGraphType<Message>
	{
		public MessageType()
		{
			Field(x => x.Content).Description("");
			Field(o => o.Content);
			Field(o => o.SentAt);
			Field(o => o.Sub, nullable: true);
			Field(o => o.From, false, typeof(MessageFromType)).Resolve(ResolveFrom);
		}

		private MessageFrom ResolveFrom(ResolveFieldContext<Message> context)
		{
			var message = context.Source;
			return message.From;
		}
	}
}
