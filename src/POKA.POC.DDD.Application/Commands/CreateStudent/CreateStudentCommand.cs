namespace POKA.POC.DDD.Extensions.Commands
{
    public record CreateStudentCommand : ICommand<StudentId>
    {
        public string FirstName { get; private set; } = null!;
        public string LastName { get; private set; } = null!;
        public string? Email { get; private set; } = null;
        public DateTime? BornOn { get; private set; } = null;

        private CreateStudentCommand()
        {
        }

        public CreateStudentCommand(string firstName, string lastName, string? email = null, DateTime? bornOn = null)
        {
            FirstName = firstName;
            LastName = lastName;
            BornOn = bornOn;
            Email = email;
        }
    }
}
