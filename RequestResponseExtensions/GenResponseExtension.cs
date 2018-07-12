using System;
using System.Collections.Generic;

namespace RequestResponseExtensions
{
	public abstract class GenResponseExtension<T>
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

}