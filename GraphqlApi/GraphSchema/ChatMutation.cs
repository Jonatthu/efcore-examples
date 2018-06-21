using GraphQL.Types;
using GraphqlApi.GraphSchema.GraphTypes;
using GraphqlApi.GraphSchema.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphqlApi.GraphSchema
{
	public class ChatMutation : ObjectGraphType<object>
	{
		public ChatMutation(IChat chat)
		{
			Field<MessageType>("addMessage",
				arguments: new QueryArguments(
					new QueryArgument<MessageInputType> { Name = "message" }
				),
				resolve: context =>
				{
					var receivedMessage = context.GetArgument<ReceivedMessage>("message");
					receivedMessage.SentAt = DateTime.Now;
					var message = chat.AddMessage(receivedMessage);
					return message;
				});
		}
	}

	public class MessageInputType : InputObjectGraphType
	{
		public MessageInputType()
		{
			Field<StringGraphType>("fromId");
			Field<StringGraphType>("content");
		}
	}
}
