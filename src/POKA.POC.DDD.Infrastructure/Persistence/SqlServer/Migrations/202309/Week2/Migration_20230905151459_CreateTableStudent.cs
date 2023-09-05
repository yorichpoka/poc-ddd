using FluentMigrator;

namespace Edonys.JoyBiiz.Infrastructure.Persistence.Migrations
{
    [Migration(20230905151459)]
    public class Migration_20230905151459_CreateTableStudent : AutoReversingMigration
    {
        public override void Up()
        {
            Create
                .Table("Student")
                .InSchema("dbo")
                    // Primary key
                    .WithColumn("Id")
                        .AsGuid()
                        .NotNullable()
                        .PrimaryKey()
                        .WithDefault(SystemMethods.NewSequentialId)
                    // Not nullable
                    .WithColumn("Version")
                        .AsInt32()
                        .NotNullable()
                    .WithColumn("CreatedOn")
                        .AsDateTime2()
                        .NotNullable()
                        .WithDefault(SystemMethods.CurrentUTCDateTime)
                    .WithColumn("FirstName")
                        .AsString(100)
                        .NotNullable()
                    .WithColumn("LastName")
                        .AsString(100)
                        .NotNullable()
                    // Nullable
                    .WithColumn("CreatedByUserId")
                        .AsGuid()
                        .Nullable()
                    .WithColumn("LastUpdatedOn")
                        .AsDateTime2()
                        .Nullable()
                    .WithColumn("Email")
                        .AsString(100)
                        .Nullable()
                    .WithColumn("Address")
                        .AsString(int.MaxValue)
                        .Nullable()
                    .WithColumn("BornOn")
                        .AsDateTime2()
                        .Nullable();
        }
    }
}
