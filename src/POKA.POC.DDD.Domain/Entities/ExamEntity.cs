using POKA.POC.DDD.Domain.ValueObjects;
using POKA.POC.DDD.Domain.Exceptions;
using POKA.POC.DDD.Domain.Enums;

namespace POKA.POC.DDD.Domain.Entities
{
    public class ExamEntity : BaseEntity<ExamId>
    {
        public string Code { get; private set; } = null!;
        public string Name { get; private set; } = null!;
        public string? Description { get; private set; }
        public DateTime On { get; private set; }

        public ExamEntity()
        {
        }

        public ExamEntity(string code, string name, DateTime on, string? description = null)
        {
            if (name.HasValue() == false)
            {
                throw new AppException(AppErrorEnum.ArgumentNullPassed, nameof(name));
            }

            if (code.HasValue() == false)
            {
                throw new AppException(AppErrorEnum.ArgumentNullPassed, nameof(code));
            }

            Description = description;
            Code = code;
            Name = name;
            On = on;
        }
    }
}
