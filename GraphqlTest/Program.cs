using GraphQL;
using GraphQL.Conversion;
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

		public class PascalCaseFieldNameConverter : IFieldNameConverter
		{
			public string NameFor(string field, Type parentType)
			{
				return field.ToPascalCase();
			}
		}

		private static async Task Run()
		{
			Console.WriteLine("Hello GraphQL!");

			var schema = new Schema { Query = new StarWarsQuery() };

			var result = await new DocumentExecuter().ExecuteAsync(_ =>
			{
				_.Schema = schema;
				_.FieldNameConverter = new PascalCaseFieldNameConverter();
				_.Query = @"
				query {
					Droid(
						Fetch: { 
							First: 10, 
							Skip: 12, 
							Filter: { 
							}
						}
					) 
					{
						Id,
						Name(IsFullName: true, UseMotherName: true),
						LastName
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

			Field(x => x.Name)
				.Description("The name of the Droid.")
				.Argument<BooleanGraphType>(name: "IsFullName", description: "Determines if you want a full name.")
				.Argument<BooleanGraphType>(name: "UseMotherName", description: "Determines if you want a full name.");

			Field(x => x.FirstName)
				.Description("The name of the Droid.");

			Field(x => x.LastName)
				.Description("The name of the Droid.");

			Field(x => x.UserName)
				.Description("The name of the Droid.");

		}
	}

	public class StarWarsQuery : ObjectGraphType
	{
		public StarWarsQuery()
		{

			Field<DroidType, Droid>()
				.Name("Droid")
				.Argument<FetchRequestType>("Fetch", "The fetch request object")
				.Resolve(context =>
				{
					FetchRequest fetchRequest = context.GetArgument<FetchRequest>("Fetch");

					fetchRequest.NamePropertyArguments = new NamePropertyArguments
					{
						IsFullName = (bool?)context.GetSubArgument("Name", "IsFullName"),
						UseMotherName = (bool)context.GetSubArgument("Name", "UseMotherName")
					};

					return new Droid { Id = "1", Name = "R2-D2" };
				});
		}
	}

	public static class GraphqlResoverContextExtensions
	{
		public static object GetSubArgument(this ResolveFieldContext<object> context, string subFieldName, string subFieldArgumentName)
		{
			if (context.SubFields.TryGetValue(subFieldName, out Field field))
			{
				var value = field.Arguments.FirstOrDefault(x => x.Name == subFieldArgumentName);

				if (value == null)
				{
					throw new Exception($@"Field ""{subFieldName}"" does not have an argument called ""{subFieldArgumentName}""");
				}

				return value.Value.Value;
			}
			else
			{
				throw new Exception($@"Field ""{subFieldName}"" does not exists on the schema.");
			}
		}
	}
}

