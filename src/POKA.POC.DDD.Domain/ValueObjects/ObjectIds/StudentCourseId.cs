namespace POKA.POC.DDD.Domain.ValueObjects
{
    public record StudentCourseId : BaseObjectId
    {
        protected override string _type => "studentCourse";

        public StudentCourseId(Guid value)
            : base(value)
        {
        }

        public override string ToString() => base.ToString();
    }
}
