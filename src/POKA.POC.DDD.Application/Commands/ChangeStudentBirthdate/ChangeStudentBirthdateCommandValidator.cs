namespace POKA.POC.DDD.Extensions.Commands
{
    public class ChangeStudentBirthdateCommandValidator : AbstractValidator<ChangeStudentBirthdateCommand>
    {
        public ChangeStudentBirthdateCommandValidator()
        {
            RuleFor(l => l.StudentId)
                .NotNull();
        }
    }
}
