using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace POKA.POC.DDD.Infrastructure.Persistence.SqlServer.EntityTypeConfiguration
{
    public class RequestEntityTypeConfiguration : IEntityTypeConfiguration<RequestEntity>
    {
        public void Configure(EntityTypeBuilder<RequestEntity> builder)
        {
            builder
                .ConfigureBaseEntity<RequestEntity, RequestId>("RequestStore", "dbo");

            builder
                .Property(l => l.ApplicationPerformer)
                .HasColumnName("ApplicationPerformer")
                .HasMaxLength(100)
                .IsRequired();

            builder
                .Property(l => l.Name)
                .HasColumnName("Name")
                .HasMaxLength(250)
                .IsRequired();

            builder
                .Property(l => l.Data)
                .HasColumnName("Data")
                .IsRequired();

            builder
                .Property(l => l.Status)
                .HasColumnName("Status")
                .HasMaxLength(100)
                .HasConversion(
                    value => value.Name,
                    dbValue => RequestStatusEnum.FromValue(dbValue)
                )
                .IsRequired();

            builder
                .Property(l => l.Type)
                .HasColumnName("Type")
                .HasMaxLength(100)
                .HasConversion(
                    value => value.Name,
                    dbValue => RequestTypeEnum.FromValue(dbValue)
                )
                .IsRequired();

            builder
                .Property(l => l.ScopeId)
                .HasColumnName("ScopeId")
                .HasMaxLength(100)
                .HasConversion(
                    value => value.Value,
                    dbValue => new RequestScopeId(dbValue)
                )
                .IsRequired();

            builder
                .Property(l => l.UserId)
                .HasColumnName("UserId")
                .HasMaxLength(100)
                .HasConversion(
                    value => value.Value,
                    dbValue => new UserId(dbValue)
                )
                .IsRequired(false);

            builder
                .Property(l => l.ParentId)
                .HasColumnName("ParentId")
                .HasMaxLength(100)
                .HasConversion(
                    value => value.Value,
                    dbValue => new RequestId(dbValue)
                )
                .IsRequired(false);

            builder
                .Property(l => l.Error)
                .HasColumnName("Error")
                .IsRequired(false);

            builder
                .Property(l => l.Duration)
                .HasColumnName("Duration")
                .IsRequired(false);

            builder
                .Property(l => l.CreatedOn)
                .HasColumnName("CreatedOn")
                .ValueGeneratedOnAdd()
                .IsRequired();
        }
    }
}
