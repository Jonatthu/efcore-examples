using GraphQL;
using GraphQL.Http;
using GraphQL.Language.AST;
using GraphQL.Types;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GraphqlTest
{
	class Program
	{
		public static void Main(string[] args)
		{
			Run().Wait();
		}

		private static async Task Run()
		{
			Console.WriteLine("Hello GraphQL!");

			var schema = new Schema { Query = new StarWarsQuery() };

			var result = await new DocumentExecuter().ExecuteAsync(_ =>
			{
				_.Schema = schema;
				_.Query = @"
                query {
                  droid {
                    id,
					name(isFullName: ""OfCourseIwantFullName""),
					lastName
                  }
                }
              ";
			}).ConfigureAwait(false);

			var json = new DocumentWriter(indent: true).Write(result);

			Console.WriteLine(json);
		}
	}

	public class Droid
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string UserName { get; set; }
	}

	public class DroidType : ObjectGraphType<Droid>
	{
		public DroidType()
		{
			Field(x => x.Id)
				.Description("The Id of the Droid.");

			Field(x => x.Name, nullable: true)
				.Description("The name of the Droid.")
				.Argument<StringGraphType>(name: "isFullName", description: "Determines if you want a full name.");

			Field(x => x.FirstName, nullable: true)
				.Description("The name of the Droid.");

			Field(x => x.LastName, nullable: true)
				.Description("The name of the Droid.");

			Field(x => x.UserName, nullable: true)
				.Description("The name of the Droid.");

		}
	}

	public class StarWarsQuery : ObjectGraphType
	{
		public StarWarsQuery()
		{

			Field<DroidType>()
				.Name("droid")
				.Argument<StringGraphType>("id", "The key")
				.Resolve(context =>
				{
					var selectedFields = context.SubFields.Keys;
					context.SubFields.TryGetValue("name", out Field field);
					var arguments = field.Arguments.ToList();
					var isFullNameValue = arguments.First().Value.Value;
					return new Droid { Id = "1", Name = "R2-D2" };
				});
		}
	}
}

