using POKA.POC.DDD.Domain.ValueObjects;
using POKA.POC.DDD.Domain.Exceptions;
using POKA.POC.DDD.Domain.Enums;

namespace POKA.POC.DDD.Domain.Entities
{
    public class CourseEntity : BaseEntity<CourseId>
    {
        public string Name { get; private set; } = null!;
        public string Description { get; private set; } = null!;
        public short Coefficient { get; private set; }

        public CourseEntity()
        {
        }

        public CourseEntity(string name, string description, short coefficient)
        {
            if (name.HasValue() == false)
            {
                throw new AppException(AppErrorEnum.ArgumentNullPassed, nameof(name));
            }

            if (description.HasValue() == false)
            {
                throw new AppException(AppErrorEnum.ArgumentNullPassed, nameof(description));
            }

            if (coefficient <= 0)
            {
                throw new AppException(AppErrorEnum.ArgumentNullPassed, nameof(coefficient));
            }

            Coefficient = coefficient;
            Description = description;
            Name = name;
        }
    }
}
