using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace POKA.POC.DDD.Infrastructure.Persistence.SqlServer.EntityTypeConfiguration
{
    public class CourseEntityTypeConfiguration : IEntityTypeConfiguration<CourseEntity>
    {
        public void Configure(EntityTypeBuilder<CourseEntity> builder)
        {
            builder
                .ConfigureBaseEntity<CourseEntity, CourseId>("Course", "dbo");

            builder
                .Property(l => l.Name)
                .HasColumnName("Name")
                .IsRequired();

            builder
                .Property(l => l.Coefficient)
                .HasColumnName("Coefficient")
                .IsRequired();

            builder
                .Property(l => l.Description)
                .HasColumnName("Description")
                .IsRequired(false);
        }
    }
}
