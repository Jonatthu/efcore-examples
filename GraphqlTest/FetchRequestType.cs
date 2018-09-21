using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphqlTest
{
    public class FetchRequestType : InputObjectGraphType<FetchRequest>
	{
		public FetchRequestType()
		{
			Field(x => x.First);

			Field(x => x.Skip);

			Field(x => x.TestArray, nullable: true);

			Field<FilterRequestType>()
				.Name("Filter")
				.Resolve(x => x.Source.Filter);
		}
    }
}
