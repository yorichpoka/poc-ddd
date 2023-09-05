using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace POKA.POC.DDD.Infrastructure.Persistence.SqlServer.EntityTypeConfiguration
{
    public class StudentCourseEntityTypeConfiguration : IEntityTypeConfiguration<StudentCourseEntity>
    {
        public void Configure(EntityTypeBuilder<StudentCourseEntity> builder)
        {
            builder
                .ConfigureBaseEntity<StudentCourseEntity, StudentCourseId>("StudentCourse", "dbo");

            builder
                .Property(l => l.StudentId)
                .HasColumnName("StudentId")
                .HasConversion(
                    value => value.Value,
                    dbValue => dbValue.ToObjectId<StudentId>()
                )
                .IsRequired();

            builder
                .Property(l => l.CourseId)
                .HasColumnName("CourseId")
                .HasConversion(
                    value => value.Value,
                    dbValue => dbValue.ToObjectId<CourseId>()
                )
                .IsRequired();
        }
    }
}
