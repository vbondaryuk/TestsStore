using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using TestsStore.Api.Infrastructure.EntityConfigurations;
using TestsStore.Api.Models;

namespace TestsStore.Api.Infrastructure
{
	public class TestsStoreContext : DbContext
	{
		public TestsStoreContext(DbContextOptions<TestsStoreContext> option)
			: base(option)
		{
		}

		public DbSet<Project> Projects { get; set; }
		public DbSet<Build> Builds { get; set; }
		public DbSet<Test> Tests { get; set; }
		public DbSet<TestResult> TestResults { get; set; }
		public DbSet<Status> Statuses { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.ApplyConfiguration(new BuildEntityTypeConfiguration());
			builder.ApplyConfiguration(new ProjectEntityTypeConfiguration());
			builder.ApplyConfiguration(new StatusEntityTypeConfiguration());
			builder.ApplyConfiguration(new StatusEntityTypeConfiguration());
			builder.ApplyConfiguration(new TestEntityTypeConfiguration());
			builder.ApplyConfiguration(new TestResultEntityTypeConfiguration());
		}
	}

	public class TestsStoreContextDesignFactory : IDesignTimeDbContextFactory<TestsStoreContext>
	{
		public TestsStoreContext CreateDbContext(string[] args)
		{
			var optionBuilder = new DbContextOptionsBuilder<TestsStoreContext>()
				.UseSqlServer("Server=.;Initial Catalog=TestsStoreDb;Integrated Security=true");

			return new TestsStoreContext(optionBuilder.Options);
		}
	}
}