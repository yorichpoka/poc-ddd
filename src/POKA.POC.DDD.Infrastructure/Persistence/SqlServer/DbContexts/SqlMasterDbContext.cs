using POKA.POC.DDD.Infrastructure.Persistence.SqlServer.EntityTypeConfiguration;

namespace POKA.POC.DDD.Infrastructure.Persistence.SqlServer.DbContexts
{
    public class SqlMasterDbContext : DbContext
    {
        public SqlMasterDbContext(DbContextOptions<SqlMasterDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
            modelBuilder
                .ApplyConfiguration(new StudentAggregateEntityTypeConfiguration())
                .ApplyConfiguration(new CourseEntityTypeConfiguration())
                .ApplyConfiguration(new ExamEntityTypeConfiguration())
            ;
    }
}
