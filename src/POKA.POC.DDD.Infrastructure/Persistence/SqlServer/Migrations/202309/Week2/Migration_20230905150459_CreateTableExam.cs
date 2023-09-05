using FluentMigrator;

namespace Edonys.JoyBiiz.Infrastructure.Persistence.Migrations
{
    [Migration(20230905150459)]
    public class Migration_20230905150459_CreateTableExam : AutoReversingMigration
    {
        public override void Up()
        {
            Create
                .Table("Exam")
                .InSchema("dbo")
                    // Primary key
                    .WithColumn("Id")
                        .AsGuid()
                        .NotNullable()
                        .PrimaryKey()
                        .WithDefault(SystemMethods.NewSequentialId)
                    // Not nullable
                    .WithColumn("Code")
                        .AsString(100)
                        .NotNullable()
                    .WithColumn("Name")
                        .AsString(100)
                        .NotNullable()
                    .WithColumn("On")
                        .AsDateTime2()
                        .NotNullable()
                    // Nullable
                    .WithColumn("Description")
                        .AsString(500)
                        .Nullable();
        }
    }
}
