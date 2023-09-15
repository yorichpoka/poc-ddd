using FluentMigrator;

namespace Edonys.JoyBiiz.Infrastructure.Persistence.Migrations
{
    [Migration(20230915102959)]
    public class Migration_20230915102959_CreateTableEventStore : AutoReversingMigration
    {
        public override void Up()
        {
            Create
                .Table("EventStore")
                .InSchema("dbo")
                    // Primary key
                    .WithColumn("Id")
                        .AsGuid()
                        .NotNullable()
                        .PrimaryKey()
                        .WithDefault(SystemMethods.NewSequentialId)
                    // Not nullable
                    .WithColumn("Type")
                        .AsString(250)
                        .NotNullable()
                    .WithColumn("AggregateId")
                        .AsString(250)
                        .NotNullable()
                    .WithColumn("AggregateType")
                        .AsString(250)
                        .NotNullable()
                    .WithColumn("Data")
                        .AsString(int.MaxValue)
                        .NotNullable()
                    .WithColumn("Version")
                        .AsInt32()
                        .NotNullable()
                    .WithColumn("CreatedOn")
                        .AsDateTime2()
                        .NotNullable()
                        .WithDefault(SystemMethods.CurrentUTCDateTime);
        }
    }
}
