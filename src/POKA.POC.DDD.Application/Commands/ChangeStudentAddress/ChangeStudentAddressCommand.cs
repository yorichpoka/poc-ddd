namespace POKA.POC.DDD.Extensions.Commands
{
    public class ChangeStudentAddressCommand : ICommand
    {
        public StudentId StudentId { get; private set; } = null!;
        public Address Address { get; private set; } = null!;

        private ChangeStudentAddressCommand()
        {
        }

        public ChangeStudentAddressCommand(StudentId studentId, Address address)
        {
            StudentId = studentId;
            Address = address;
        }
    }
}
