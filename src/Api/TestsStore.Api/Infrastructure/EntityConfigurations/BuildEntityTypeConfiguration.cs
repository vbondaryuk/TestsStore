using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestsStore.Api.Models;

namespace TestsStore.Api.Infrastructure.EntityConfigurations
{
	public class BuildEntityTypeConfiguration
		: IEntityTypeConfiguration<Build>
	{
		public void Configure(EntityTypeBuilder<Build> builder)
		{
			builder.ToTable("Build");

			builder.HasKey(x => x.Id);

			builder.Property(x => x.Id)
				.ValueGeneratedNever()
				.IsRequired();

			builder.Property(x => x.Name)
				.IsRequired()
				.HasMaxLength(100);

			builder.Property(x => x.StartTime)
				.IsRequired();

			builder.Property(x => x.EndTime);

			builder.HasOne(x => x.Project)
				.WithMany(x => x.Builds)
				.HasForeignKey(x => x.ProjectId)
				.IsRequired();

			builder.HasOne(x => x.Status)
				.WithMany()
				.HasForeignKey(x => x.StatusId)
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired();

			builder.HasMany(x => x.TestResults)
				.WithOne(x => x.Build)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}