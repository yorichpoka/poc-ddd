using FluentMigrator;

namespace Edonys.JoyBiiz.Infrastructure.Persistence.Migrations
{
    [Migration(20230905151059)]
    public class Migration_20230905151059_CreateTableExam : AutoReversingMigration
    {
        public override void Up()
        {
            Create
                .Table("Course")
                .InSchema("dbo")
                    // Primary key
                    .WithColumn("Id")
                        .AsGuid()
                        .NotNullable()
                        .PrimaryKey()
                        .WithDefault(SystemMethods.NewSequentialId)
                    // Not nullable
                    .WithColumn("Name")
                        .AsString(100)
                        .NotNullable()
                    .WithColumn("Coefficient")
                        .AsFloat()
                        .NotNullable()
                    // Nullable
                    .WithColumn("Description")
                        .AsString(500)
                        .Nullable();
        }
    }
}
