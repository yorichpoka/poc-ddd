using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace POKA.POC.DDD.Infrastructure.Persistence.SqlServer.EntityTypeConfiguration
{
    public class ExamEntityTypeConfiguration : IEntityTypeConfiguration<ExamEntity>
    {
        public void Configure(EntityTypeBuilder<ExamEntity> builder)
        {
            builder
                .ConfigureBaseEntity<ExamEntity, ExamId>("Exam", "dbo");

            builder
                .Property(l => l.Code)
                .HasColumnName("Code")
                .IsRequired();

            builder
                .Property(l => l.Name)
                .HasColumnName("Name")
                .IsRequired();

            builder
                .Property(l => l.On)
                .HasColumnName("On")
                .IsRequired();

            builder
                .Property(l => l.Description)
                .HasColumnName("Description")
                .IsRequired(false);
        }
    }
}
