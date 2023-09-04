using POKA.POC.DDD.Domain.ValueObjects;
using POKA.POC.DDD.Domain.Entities;

namespace POKA.POC.DDD.Domain.Aggregates
{
    public partial class StudentAggregate : AggregateRoot<StudentId>
    {
        private HashSet<StudentCourseEntity> _studentCourses = new();

        public string FirstName { get; protected set; } = null!;
        public string LastName { get; protected set; } = null!;
        public string? Email { get; protected set; } = null!;
        public Address? Address { get; protected set; } = null;
        public DateTime? BornOn { get; protected set; } = null;
    }
}
