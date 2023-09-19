namespace POKA.POC.DDD.Application.Test.Infrastructure.Repositories
{
    public class IMasterDbRepositoryTest : BaseTest
    {
        [Fact]
        public void CanGetStudents()
        {
            // A
            var masterDbRepository = ServiceProvider.GetRequiredService<IMasterDbRepository>();

            // A
            var query = masterDbRepository
                            .Students
                            .AsQueryable()
                            .Where(l => l.StudentCourses.Count != 0);
            var queryResult = masterDbRepository.Students.ExecuteQuery(query);

            // A
            Assert.NotNull(queryResult);
            Assert.NotEmpty(queryResult);
        }
    }
}
