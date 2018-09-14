using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestsStore.Api.Model;

namespace TestsStore.Api.Infrastructure.EntityConfigurations
{
	public class TestEntityTypeConfiguration
		: IEntityTypeConfiguration<Test>
	{
		public void Configure(EntityTypeBuilder<Test> builder)
		{
			builder.ToTable("Test");

			builder.HasKey(x => x.Id);

			builder.Property(x => x.Id)
				.ValueGeneratedNever()
				.IsRequired();

			builder.Property(x => x.Name)
				.IsRequired()
				.HasMaxLength(300);

			builder.Property(x => x.ClassName)
				.IsRequired()
				.HasMaxLength(1000);

			builder.HasIndex(x => new {x.ClassName, x.Name});

			builder.HasOne(x => x.Project)
				.WithMany(x => x.Tests)
				.HasForeignKey(x => x.ProjectId)
				.IsRequired();

			builder.HasMany(x => x.TestResults)
				.WithOne(x => x.Test)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}