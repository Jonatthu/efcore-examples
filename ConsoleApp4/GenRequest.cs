using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ConsoleApp4
{
	public abstract class GenFilterRequest<T>
	{

	}

	public abstract class GenResponse<T>
	{

		private readonly Dictionary<object, List<Parameter>> propertyParameters = new Dictionary<object, List<Parameter>>();

		protected void AddPropertyParameter(Func<T, object> property, Type parameterType, string parameterName)
		{
			if (this.propertyParameters.TryGetValue(property.Target, out List<Parameter> parameters))
			{
				parameters.Add(new Parameter(parameterType, parameterName));
			}
			else
			{
				this.propertyParameters.Add(property.Target, new List<Parameter> {
					new Parameter(parameterType, parameterName)
				});
			}
		}

		protected void AddPropertyParameter(Func<T, object> property, params Parameter[] parameters)
		{
			foreach (Parameter parameter in parameters)
			{
				AddPropertyParameter(property, parameter);
			}
		}

		protected class Parameter
		{
			public readonly Type type;
			public readonly string name;

			public Parameter(Type Type, string Name)
			{
				type = Type;
				name = Name;
			}
		}

	}

	public class UserSettings
	{
		public int Id { get; set; }
		public string Name { get; set; }
	}

	public class UserSettingsRequest : GenFilterRequest<UserSettings>
	{
		public bool ComputedSuggestedName { get; set; }
	}

	public abstract class UserSettingsResponse : GenResponse<UserSettings>
	{

		public UserSettingsResponse()
		{
			AddPropertyParameter(x => x.Name, typeof(int), "name");
			AddPropertyParameter(x => x.Name, typeof(int), "name");
			AddPropertyParameter(x => x.Name, typeof(int), "name");
			AddPropertyParameter(
				x => x.Name,
				new Parameter(typeof(int), "hello"),
				new Parameter(typeof(int), "hello"),
				new Parameter(typeof(int), "hello"),
				new Parameter(typeof(int), "hello")
			);
		}

		[MyCustomMetadata]
		public abstract string SuggestedName(int name);
	}






	[System.AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
	sealed class MyCustomMetadataAttribute : Attribute
	{
	}


}
