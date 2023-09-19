namespace POKA.POC.DDD.Extensions.Commands
{
    public record ChangeStudentBirthdateCommand : ICommand
    {
        public StudentId StudentId { get; private set; } = null!;
        public DateTime Birthdate { get; private set; }

        private ChangeStudentBirthdateCommand()
        {
        }

        public ChangeStudentBirthdateCommand(StudentId studentId, DateTime birthdate)
        {
            StudentId = studentId;
            Birthdate = birthdate;
        }
    }
}
