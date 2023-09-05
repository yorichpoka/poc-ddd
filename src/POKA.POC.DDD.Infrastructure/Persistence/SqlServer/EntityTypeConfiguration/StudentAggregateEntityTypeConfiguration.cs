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
                .OwnsMany(
                    l => l.StudentCourses,
                    navigationBuilder =>
                    {
                        navigationBuilder
                            .Metadata.PrincipalToDependent.SetField("_studentCourses");

                        navigationBuilder
                            .WithOwner()
                            .HasForeignKey(l => l.StudentId);

                        navigationBuilder
                            .Property(l => l.CourseId)
                            .HasColumnName("CourseId")
                            .IsRequired();
                    }
                );

        }
    }
}
