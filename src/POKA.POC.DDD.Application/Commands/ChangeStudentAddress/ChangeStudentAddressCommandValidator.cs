namespace POKA.POC.DDD.Extensions.Commands
{
    public class ChangeStudentAddressCommandValidator : AbstractValidator<ChangeStudentAddressCommand>
    {
        public ChangeStudentAddressCommandValidator()
        {
            RuleFor(l => l.StudentId)
                .NotNull();

            RuleFor(l => l.Address)
                .NotNull();

            RuleFor(l => l.Address.Country)
                .NotNull();

            RuleFor(l => l.Address.City)
                .NotNull()
                .NotEmpty();

            RuleFor(l => l.Address.Line1)
                .NotNull()
                .NotEmpty();

            RuleFor(l => l.Address.PostalCode)
                .NotNull()
                .NotEmpty();
        }
    }
}
