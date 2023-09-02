namespace POKA.POC.DDD.Domain.ValueObjects
{
    public record StudentId : BaseObjectId
    {
        protected override string _type => "student";

        public StudentId(Guid value)
            : base(value)
        {
        }

        public override string ToString() => base.ToString();
    }
}
