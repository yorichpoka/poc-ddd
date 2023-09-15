namespace POKA.POC.DDD.Application.Interfaces
{
    public interface IMasterDbRepository : IUnitOfWork
    {
        IDbSetRepository<StudentAggregate> Students { get; }
        IDbSetRepository<CourseEntity> Courses { get; }
        IDbSetRepository<ExamEntity> Exams { get; }
    }
}
