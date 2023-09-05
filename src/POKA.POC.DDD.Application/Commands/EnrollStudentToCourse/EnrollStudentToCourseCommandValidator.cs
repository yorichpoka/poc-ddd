namespace POKA.POC.DDD.Extensions.Commands
{
    public class EnrollStudentToCourseCommandValidator : AbstractValidator<EnrollStudentToCourseCommand>
    {
        public EnrollStudentToCourseCommandValidator()
        {
            RuleFor(l => l.StudentId)
                .NotNull();

            RuleFor(l => l.CourseId)
                .NotNull();
        }
    }
}
