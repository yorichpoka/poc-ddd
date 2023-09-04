namespace POKA.POC.DDD.Extensions.Commands
{
    public class CreateStudentCommandValidator : AbstractValidator<CreateStudentCommand>
    {
        public CreateStudentCommandValidator()
        {
            RuleFor(l => l.FirstName)
                .NotNull()
                .NotEmpty();

            RuleFor(l => l.LastName)
                .NotNull()
                .NotEmpty();

            When(
                l => l.Email.HasValue(),
                () =>
                {
                    RuleFor(l => l.Email)
                        .EmailAddress();
                }
            );

            When(
                l => l.BornOn.HasValue,
                () =>
                {
                    RuleFor(l => l.BornOn.Value)
                        .Must(l => l < DateTime.UtcNow);
                }
            );
        }
    }
}
