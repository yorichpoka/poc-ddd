using POKA.POC.DDD.Domain.ValueObjects;
using POKA.POC.DDD.Domain.Interfaces;

namespace POKA.POC.DDD.Domain.Entities
{
    public class StudentEntity : BasePersonEntity<StudentId>, IHasCreatedOn
    {
        public DateTime CreatedOn { get; private set; }

        public StudentEntity()
            : base()
        {
        }

        public StudentEntity(StudentId id, string firstName, string lastName) 
            : base(id, firstName, lastName)
        {
            CreatedOn = DateTime.UtcNow;
        }
    }
}
