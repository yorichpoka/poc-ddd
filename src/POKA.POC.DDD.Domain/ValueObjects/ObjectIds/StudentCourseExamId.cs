namespace POKA.POC.DDD.Domain.ValueObjects
{
    public record StudentCourseExamId : BaseObjectId
    {
        protected override string _type => "studentCourseExam";

        public StudentCourseExamId(Guid value)
            : base(value)
        {
        }

        public override string ToString() => base.ToString();
    }
}
