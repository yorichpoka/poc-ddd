namespace POKA.POC.DDD.Extensions.Commands
{
    public record EnrollStudentToCourseCommand : ICommand
    {
        public StudentId StudentId { get; private set; } = null!;
        public CourseId CourseId { get; private set; } = null!;

        private EnrollStudentToCourseCommand()
        {
        }

        public EnrollStudentToCourseCommand(StudentId studentId, CourseId courseId)
        {
            StudentId = studentId;
            CourseId = courseId;
        }
    }
}
