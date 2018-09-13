using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestsStore.Api.Model;

namespace TestsStore.Api.Infrastructure.EntityConfigurations
{
	public class ProjectEntityTypeConfiguration
		: IEntityTypeConfiguration<Project>
	{
		public void Configure(EntityTypeBuilder<Project> builder)
		{
			builder.ToTable("Project");

			builder.HasKey(x => x.Id);

			builder.Property(x => x.Id)
				.ValueGeneratedNever()
				.IsRequired();

			builder.Property(x => x.Name)
				.IsRequired()
				.HasMaxLength(100);

			builder.HasIndex(x => x.Name)
				.IsUnique();

			builder.HasMany(x => x.Builds)
				.WithOne(x => x.Project)
				.OnDelete(DeleteBehavior.Cascade);

			builder.HasMany(x => x.Tests)
				.WithOne(x => x.Project)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}