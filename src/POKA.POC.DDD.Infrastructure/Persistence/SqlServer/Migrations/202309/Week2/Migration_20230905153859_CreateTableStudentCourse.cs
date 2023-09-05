using FluentMigrator;

namespace Edonys.JoyBiiz.Infrastructure.Persistence.Migrations
{
    [Migration(20230905153859)]
    public class Migration_20230905153859_CreateTableStudentCourse : AutoReversingMigration
    {
        public override void Up()
        {
            Create
                .Table("StudentCourse")
                .InSchema("dbo")
                    // Primary key
                    .WithColumn("Id")
                        .AsGuid()
                        .NotNullable()
                        .PrimaryKey()
                        .WithDefault(SystemMethods.NewSequentialId)
                    // Not nullable
                    .WithColumn("StudentId")
                        .AsGuid()
                        .NotNullable()
                        .ForeignKey(
                            foreignKeyName: "FK_Student_StudentCourse",
                            primaryTableSchema: "dbo",
                            primaryTableName: "Student",
                            primaryColumnName: "Id"
                        )
                    .WithColumn("CourseId")
                        .AsGuid()
                        .NotNullable()
                        .ForeignKey(
                            foreignKeyName: "FK_Student_StudentCourse",
                            primaryTableSchema: "dbo",
                            primaryTableName: "Course",
                            primaryColumnName: "Id"
                        );
        }
    }
}
