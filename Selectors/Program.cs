using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Selectors
{
	class Program
	{
		static void Main(string[] args)
		{

			Stopwatch stopwatch = new Stopwatch();

			stopwatch.Reset();
			stopwatch.Start();
			IEnumerable<UserSelectorEnum> selectors = GetSelectors<UserSelectorEnum>("UserName", "LastName");
			IEnumerable<UserComputedSelectorEnum> computedSelectors = GetSelectors<UserComputedSelectorEnum>("FullName", "FullNameInitials");
			stopwatch.Stop();
			Console.WriteLine("With Array: " + stopwatch.ElapsedTicks);

			stopwatch.Reset();
			stopwatch.Start();
			IEnumerable<UserSelectorEnum> selectorsList = GetSelectorsList<UserSelectorEnum>("UserName", "FirstName");
			IEnumerable<UserComputedSelectorEnum> computedSelectorsList = GetSelectorsList<UserComputedSelectorEnum>("FullName", "FullNameSignature");
			stopwatch.Stop();
			Console.WriteLine("With List: " + stopwatch.ElapsedTicks);

			if (selectors.Contains(UserSelectorEnum.UserName))
			{
				Console.WriteLine("It contains Username!");
			}

			Console.ReadKey();
		}

		public static IEnumerable<T> GetSelectors<T>(params string[] fields)
		{
			T[] selectors = new T[fields.Length];

			for (int i = 0; i < fields.Length; i++)
			{
				if (Enum.TryParse(typeof(T), fields[i], out object result))
				{
					selectors[i] = (T)result;
				}
				else
				{
					throw new Exception($@"The field:""{fields[i]}"" does not exist.");
				}
			}

			return selectors;
		}

		public static IEnumerable<T> GetSelectorsList<T>(params string[] fields)
		{
			List<T> selectors = new List<T>(fields.Length);

			foreach (string field in fields)
			{
				if (Enum.TryParse(typeof(T), field, out object result))
				{
					selectors.Add((T)result);
				}
				else
				{
					throw new Exception($@"The field:""{field}"" does not exist.");
				}
			}

			return selectors;
		}

		public enum UserSelectorEnum
		{
			UserName,
			FirstName,
			LastName
		}

		public enum UserComputedSelectorEnum
		{
			FullName,
			FullNameInitials,
			FullNameSignature
		}
	}
}
