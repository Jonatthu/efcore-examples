using Microsoft.EntityFrameworkCore;

namespace ConsoleApp4
{
	public class TestDbContext : DbContext
	{
		public DbSet<User> User { get; set; }
		public DbSet<Blogg> Blog { get; set; }

		public TestDbContext()
		{

		}

		public TestDbContext(DbContextOptions<TestDbContext> options) : base(options)
		{

		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EFProviders.InMemory;Trusted_Connection=True;ConnectRetryCount=0");
			}
		}

	}
}
