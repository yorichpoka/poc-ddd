﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace POKA.POC.DDD.Extensions
{
    public static class EntityTypeBuilderExtensions
    {
        public static EntityTypeBuilder<TEntity> ConfigureBaseEntity<TEntity, TObjectId>(this EntityTypeBuilder<TEntity> builder, string tableName, string schemaName) 
            where TEntity : BaseEntity<TObjectId>
            where TObjectId : BaseObjectId
        {
            builder
                .ToTable(tableName, schemaName);

            builder
                .HasKey(e => e.Id);

            builder
                .Property(l => l.Id)
                .HasColumnName("Id")
                .HasConversion(
                    value => value.Value,
                    dbValue => dbValue.ToObjectId<TObjectId>()
                );

            return builder;
        }

        public static EntityTypeBuilder<TEntity> ConfigureHasCreatedByUserId<TEntity>(this EntityTypeBuilder<TEntity> builder)
            where TEntity : class, IHasCreatedByUserId
        {
            builder
                .Property(l => l.CreatedByUserId)
                .HasColumnName("CreatedByUserId")
                .HasConversion(
                    value => value.Value,
                    dbValue => new UserId(dbValue)
                );

            return builder;
        }

        public static EntityTypeBuilder<TEntity> ConfigureHasCreatedOn<TEntity>(this EntityTypeBuilder<TEntity> builder)
            where TEntity : class, IHasCreatedOn
        {
            builder
                .Property(l => l.CreatedOn)
                .HasColumnName("CreatedOn")
                .ValueGeneratedOnAdd()
                .IsRequired();

            return builder;
        }

        public static EntityTypeBuilder<TEntity> ConfigureHasVersion<TEntity>(this EntityTypeBuilder<TEntity> builder) 
            where TEntity : class, IHasVersion
        {
            builder
                .Property(l => l.Version)
                .HasColumnName("Version")
                .IsRequired();

            return builder;
        }

        public static EntityTypeBuilder<TEntity> ConfigureHasAddress<TEntity>(this EntityTypeBuilder<TEntity> builder)
            where TEntity : class, IHasAddress
        {

            builder
                .OwnsOne(
                    l => l.Address,
                    navigationBuilder =>
                    {
                        var addressNavigationBuilder = navigationBuilder.ToJson("Address");

                        addressNavigationBuilder
                            .Property(l => l.Country)
                            .HasColumnName("Country")
                            .HasConversion(
                                value => value.GetCodeISO2(),
                                dbValue => CountryEnum.FromValueCodeISO2(dbValue)
                            );
                    }
                );

            return builder;
        }
    }
}
