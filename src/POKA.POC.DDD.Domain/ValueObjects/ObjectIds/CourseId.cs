namespace POKA.POC.DDD.Domain.ValueObjects
{
    public record CourseId : BaseObjectId
    {
        protected override string _type => "course";

        public CourseId(Guid value)
            : base(value)
        {
        }

        public override string ToString() => base.ToString();
    }
}
