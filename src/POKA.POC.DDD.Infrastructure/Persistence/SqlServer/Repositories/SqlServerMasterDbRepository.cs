using POKA.POC.DDD.Infrastructure.Persistence.SqlServer.DbContexts;
using Microsoft.EntityFrameworkCore.Storage;

namespace POKA.POC.DDD.Infrastructure.Persistence.SqlServer.Repositories
{
    public class SqlServerMasterDbRepository : IMasterDbRepository
    {
        private readonly IDbSetRepository<StudentAggregate> _students;
        private readonly IDbSetRepository<CourseEntity> _courses;
        private readonly IDbSetRepository<ExamEntity> _exams;

        private readonly SqlMasterDbContext _dbContext;

        private IDbContextTransaction? _dbContextTransaction;
        private int _dbContextTransactionCount = 0;

        public IDbSetRepository<StudentAggregate> Students => _students;
        public IDbSetRepository<CourseEntity> Courses => _courses;
        public IDbSetRepository<ExamEntity> Exams => _exams;

        public SqlServerMasterDbRepository(
            IDbSetRepository<StudentAggregate> students,
            IDbSetRepository<CourseEntity> course,
            IDbSetRepository<ExamEntity> exams,
            SqlMasterDbContext dbContext
        )
        {
            _students = students;
            _courses = course;
            _exams = exams;
        }

        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            this._dbContextTransactionCount++;

            if (_dbContextTransaction is not null)
            {
                return;
            }

            _dbContextTransaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_dbContextTransaction is null)
            {
                return;
            }

            this._dbContextTransactionCount--;

            if (this._dbContextTransactionCount == 0)
            {
                await _dbContextTransaction.CommitAsync(cancellationToken);

                _dbContextTransaction = null;
            }
        }

        public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_dbContextTransaction is null)
            {
                return;
            }

            await _dbContextTransaction.RollbackAsync(cancellationToken);

            _dbContextTransaction = null;
            this._dbContextTransactionCount = 0;
        }
    }
}
