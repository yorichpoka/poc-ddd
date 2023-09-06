namespace POKA.POC.DDD.Application.Interfaces
{
    public interface IStudentValidationService
    {
        void ValidateAge(StudentAggregate studentAggregate);
    }
}
