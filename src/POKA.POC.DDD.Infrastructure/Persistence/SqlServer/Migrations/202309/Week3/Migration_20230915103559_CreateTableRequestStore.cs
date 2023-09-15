using FluentMigrator;

namespace Edonys.JoyBiiz.Infrastructure.Persistence.Migrations
{
    [Migration(20230915103559)]
    public class Migration_20230915103559_CreateTableRequestStore : AutoReversingMigration
    {
        public override void Up()
        {
            Create
                .Table("RequestStore")
                .InSchema("dbo")
                    // Primary key
                    .WithColumn("Id")
                        .AsGuid()
                        .NotNullable()
                        .PrimaryKey()
                        .WithDefault(SystemMethods.NewSequentialId)
                    // Not nullable
                    .WithColumn("ApplicationPerformer")
                        .AsString(100)
                        .NotNullable()
                    .WithColumn("Name")
                        .AsString(250)
                        .NotNullable()
                    .WithColumn("Data")
                        .AsString(int.MaxValue)
                        .NotNullable()
                    .WithColumn("Status")
                        .AsString(100)
                        .NotNullable()
                    .WithColumn("Type")
                        .AsString(100)
                        .NotNullable()
                    .WithColumn("CreatedOn")
                        .AsDateTime2()
                        .NotNullable()
                        .WithDefault(SystemMethods.CurrentUTCDateTime)
                    // Nullable
                    .WithColumn("Duration")
                        .AsTime()
                        .Nullable()
                    .WithColumn("Error")
                        .AsString(int.MaxValue)
                        .Nullable()
                    .WithColumn("UserId")
                        .AsGuid()
                        .Nullable()
                    .WithColumn("ParentId")
                        .AsGuid()
                        .Nullable()
                        .ForeignKey(
                            foreignKeyName: "FK_RequestStore_RequestStore_Parent",
                            primaryTableName: "RequestStore",
                            primaryTableSchema: "dbo",
                            primaryColumnName: "Id"
                        );
        }
    }
}
