using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphqlTest
{
    public class FilterRequestType : InputObjectGraphType<FilterRequest>
	{
		public FilterRequestType()
		{
			Name = "filter";

			Field(x => x.Search, nullable: true);

			Field<ListGraphType<StringGraphType>>()
				.Name("ids")
				.Resolve(x =>
				{
					return x.Source.Ids;
				});
		}
    }
}
