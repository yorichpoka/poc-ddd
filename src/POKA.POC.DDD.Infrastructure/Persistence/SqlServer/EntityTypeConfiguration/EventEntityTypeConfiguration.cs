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
                .HasMaxLength(250)
                .IsRequired();

            builder
                .Property(l => l.AggregateType)
                .HasColumnName("AggregateType")
                .HasMaxLength(250)
                .IsRequired();

            builder
                .Property(l => l.Type)
                .HasColumnName("Type")
                .HasMaxLength(250)
                .IsRequired();

            builder
                .Property(l => l.Version)
                .HasColumnName("Version")
                .IsRequired();

            builder
                .Property(l => l.Data)
                .HasColumnName("Data")
                .HasMaxLength(2000)
                .IsRequired();

            builder
                .Property(l => l.CreatedOn)
                .HasColumnName("CreatedOn")
                .ValueGeneratedOnAdd()
                .IsRequired();
        }
    }
}