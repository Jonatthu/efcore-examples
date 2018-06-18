using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;

namespace ConsoleApp4
{
    class Program
    {
        public static readonly LoggerFactory MyLoggerFactory
            = new LoggerFactory(new[]
            {
        new ConsoleLoggerProvider((category, level)
            => category == DbLoggerCategory.Database.Command.Name
               && level == LogLevel.Information, true)
            });

        static void Main(string[] args)
        {


			var request = new Entity
			{
				Id = 1,
				Blog = new Blog
				{
					Message = "Hello"
				},
				BlogIds = new[] { 1, 2, 3 },
				MyBlogs = new[]
				{
					new Blog
					{
						Title = "Hello"
					}
				}
			};
			var request1 = new Entity
			{
				Id = 1,
				Blog = new Blog
				{
					Message = "Hello"
				},
				BlogIds = new [] {1 , 2, 3},
				MyBlogs = new[]
				{
					new Blog
					{
						Title = "Hello0"
					}
				}
			};
			var request2 = new Entity
			{
				Id = 2,
				Blog = new Blog
				{
					Message = "Hello"
				},
				BlogIds = new[] { 1, 2, 3 },
				MyBlogs = new[]
				{
					new Blog
					{
						Title = "Hello"
					}
				}
			};

			int hash = request.GetHashCode();
			int hash1 = request1.GetHashCode();
			int hash2 = request2.GetHashCode();



			var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<TestDbContext>()
                 .UseLoggerFactory(MyLoggerFactory)
                 .UseSqlite(connection)
                 .Options;

            using (var dbContext = new TestDbContext(options))
            {
                dbContext.Database.EnsureCreated();
            }

            using (var dbContext = new TestDbContext(options))
            {

                dbContext.User.Add(new User
                {
                    FirstName = "Jonathan",
                    LastName = "Nungaray",
                    UserName = "Jonatthu",
                    Id = 2,
                });
                dbContext.User.Add(new User
                {
                    FirstName = "Lorena",
                    LastName = "Nungaray",
                    UserName = "Loretthu",
                    Id = 5
                });
                dbContext.SaveChanges();
            }


            using (var dbContext = new TestDbContext(options))
            {

                bool includeId = false;
                bool includeFirstName = false;
                bool includeLastName = true;

                var userParameter = Expression.Parameter(typeof(User), "x");
                var bindings = new List<MemberAssignment>();

                if (includeId)
                {
                    var idName = typeof(User).GetProperty(nameof(User.Id));
                    bindings.Add(Expression.Bind(idName, Expression.Property(userParameter, idName)));
                }

                if (includeFirstName)
                {
                    var firstNameProperty = typeof(User).GetProperty(nameof(User.FirstName));
                    bindings.Add(Expression.Bind(firstNameProperty, Expression.Property(userParameter, firstNameProperty)));
                }

                if (includeLastName)
                {
                    var lastNameProperty = typeof(User).GetProperty(nameof(User.LastName));
                    bindings.Add(Expression.Bind(lastNameProperty, Expression.Property(userParameter, lastNameProperty)));
                }

                var memberInit = Expression.MemberInit(Expression.New(typeof(User)), bindings);
                var lambda = Expression.Lambda<Func<User, User>>(memberInit, userParameter);

				var result = dbContext.User
					.Where(x =>
						new[] { 44, 5, 546, 99 }.Contains(x.Blogs.Count())
					)
					.Where(x => x.Blogs.Where(c => c.Flod == 1).Any())
					.Select(x => new User {
						Id = x.Id
					})
                    .ToList();


            }


			var service = $@"
				namespace Hello
				{{
					public class HelloThere
					{{
						#region Hello
							// Hello
									#endregion

						#region				HelloThere
						// HelloThere
		#endregion
					}}
				}}
			";

			var tree = CSharpSyntaxTree.ParseText(service);
			var root = tree.GetRoot()
				.NormalizeWhitespace(indentation: "	");

			var ret = root.ToFullString();

			connection.Close();

        }

		public class AuthValidationResult
        {

            public static AuthValidationResult IsValid => new AuthValidationResult();

            //public static AuthValidationResult IsValid()
            //{
            //	return new AuthValidationResult();
            //}

            public static AuthValidationResult NotValid()
            {
                throw new Exception();
            }
        }

        public AuthValidationResult IsValid()
        {
            return AuthValidationResult.IsValid;
        }
    }
}
