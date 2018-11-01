using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestsStore.Api.Models;

namespace TestsStore.Api.Infrastructure.EntityConfigurations
{
	public class StatusEntityTypeConfiguration
		: IEntityTypeConfiguration<Status>
	{
		public void Configure(EntityTypeBuilder<Status> builder)
		{
			builder.ToTable("Status");

			builder.HasKey(x => x.Id);

			builder.Property(x => x.Id)
				.HasDefaultValue(Status.None.Id)
				.ValueGeneratedNever()
				.IsRequired();

			builder.Property(x => x.Name)
				.HasMaxLength(50)
				.IsRequired();
		}
	}
}