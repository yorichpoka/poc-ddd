using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace POKA.POC.DDD.Infrastructure.Persistence.SqlServer.EntityTypeConfiguration
{
    public class StudentAggregateEntityTypeConfiguration : IEntityTypeConfiguration<StudentAggregate>
    {
        public void Configure(EntityTypeBuilder<StudentAggregate> builder)
        {
            builder
                .ConfigureAggregate<StudentAggregate, StudentId>("Student", "dbo")
                .ConfigureHasAddress();

            builder
                .Property(l => l.FirstName)
                .HasColumnName("FirstName")
                .IsRequired();

            builder
                .Property(l => l.LastName)
                .HasColumnName("LastName")
                .IsRequired();

            builder
                .Property(l => l.Email)
                .HasColumnName("Email")
                .IsRequired(false);

            builder
                .Property(l => l.BornOn)
                .HasColumnName("BornOn")
                .IsRequired(false);

            builder
                .OwnsMany<StudentCourseEntity>(
                    "_studentCourses",
                    navigationBuilder =>
                    {
                        navigationBuilder
                            .ToTable("StudentCourse", "dbo");

                        navigationBuilder
                            .Property(l => l.Id)
                            .HasColumnName("Id")
                            .HasConversion(
                                value => value.Value,
                                dbValue => dbValue.ToObjectId<StudentCourseId>()
                            )
                            .IsRequired();

                        navigationBuilder
                            .WithOwner()
                            .HasForeignKey(l => l.StudentId);

                        navigationBuilder
                            .Property(l => l.CourseId)
                            .HasColumnName("CourseId")
                            .HasConversion(
                                value => value.Value,
                                dbValue => dbValue.ToObjectId<CourseId>()
                            )
                            .IsRequired();
                    }
                );

        }
    }
}
