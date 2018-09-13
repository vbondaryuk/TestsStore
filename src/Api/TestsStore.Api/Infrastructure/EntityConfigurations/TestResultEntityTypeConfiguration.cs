using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestsStore.Api.Model;

namespace TestsStore.Api.Infrastructure.EntityConfigurations
{
	public class TestResultEntityTypeConfiguration
		: IEntityTypeConfiguration<TestResult>
	{
		public void Configure(EntityTypeBuilder<TestResult> builder)
		{
			builder.ToTable("TestResult");

			builder.HasKey(x => x.Id);

			builder.Property(x => x.Id)
				.ValueGeneratedNever()
				.IsRequired();

			builder.Property(x => x.Duration)
				.IsRequired();

			builder.Property(x => x.Messages)
				.HasMaxLength(5000);

			builder.Property(x => x.ErrorMessage)
				.HasMaxLength(5000);

			builder.Property(x => x.StackTrace)
				.HasMaxLength(5000);

			builder.HasOne(x => x.Build)
				.WithMany(x => x.TestResults)
				.HasForeignKey(x => x.BuildId)
				.IsRequired();

			builder.HasOne(x => x.Test)
				.WithMany(x => x.TestResults)
				.HasForeignKey(x => x.TestId)
				.IsRequired();

			builder.HasOne(x => x.Status)
				.WithMany()
				.HasForeignKey(x => x.StatusId)
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired();
		}
	}
}