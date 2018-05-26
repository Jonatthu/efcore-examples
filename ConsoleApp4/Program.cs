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

                var watch = new Stopwatch();

                // clean up
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();

                bool includeId = true;
                bool includeFirstName = false;
                bool includeLastName = false;

                Stopwatch stopwatch = new Stopwatch();

                stopwatch.Start();
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

                //var results = dbContext.User.Select(lambda).ToList();
                stopwatch.Stop();
                Console.WriteLine($"First Time: {stopwatch.ElapsedTicks}");

                stopwatch.Start();
                //results = dbContext.User.Select(x => new User { FirstName = x.FirstName, LastName = x.LastName, Id = x.Id }).ToList();
                stopwatch.Stop();


				int[] blogsCountUserIds = dbContext.Blog
					.Select(x => new Blog { UserId = x.UserId})
					.Where(x => new[] { 44, 5, 546, 99 }.Contains(x.Id))
					.ToList()
					.Select(x => x.UserId)
					.ToArray();

				if (blogsCountUserIds.Length == 0)
					blogsCountUserIds = new [] {1, 3};

                var county = dbContext.User.AsNoTracking()
                    .Select(lambda)
					.AsNoTracking()
                    //.Where(x => blogsCountUserIds.Contains(x.Id))
					.Where(x => 
						x.Blogs.Count() > 0
					)
                    .ToList();


                Console.WriteLine($"First Time: {stopwatch.ElapsedTicks}");
            }

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
