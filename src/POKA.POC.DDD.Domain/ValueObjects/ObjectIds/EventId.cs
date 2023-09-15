namespace POKA.POC.DDD.Domain.ValueObjects
{
    public record EventId : BaseObjectId
    {
        protected override string _type => "event";

        public EventId(Guid value)
            : base(value)
        {
        }

        public override string ToString() => base.ToString();
    }
}
