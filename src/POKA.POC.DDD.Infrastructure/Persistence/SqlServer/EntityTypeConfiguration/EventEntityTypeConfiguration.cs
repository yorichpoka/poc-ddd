using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace POKA.POC.DDD.Infrastructure.Persistence.SqlServer.EntityTypeConfiguration
{
    public class EventEntityTypeConfiguration : IEntityTypeConfiguration<EventEntity>
    {
        public void Configure(EntityTypeBuilder<EventEntity> builder)
        {
            builder
                .ConfigureBaseEntity<EventEntity, EventId>("EventStore", "dbo");

            builder
                .Property(l => l.AggregateId)
                .HasColumnName("AggregateId")
                .IsRequired();

            builder
                .Property(l => l.AggregateType)
                .HasColumnName("AggregateType")
                .IsRequired();

            builder
                .Property(l => l.Type)
                .HasColumnName("Type")
                .IsRequired();

            builder
                .Property(l => l.Version)
                .HasColumnName("Version")
                .IsRequired();

            builder
                .Property(l => l.Data)
                .HasColumnName("Data")
                .IsRequired();

            builder
                .Property(l => l.CreatedOn)
                .HasColumnName("CreatedOn")
                .ValueGeneratedOnAdd()
                .IsRequired();
        }
    }
}