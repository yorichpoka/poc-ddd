namespace POKA.POC.DDD.Domain.ValueObjects
{
    public record RequestId : BaseObjectId
    {
        protected override string _type => "request";

        public RequestId(Guid value)
            : base(value)
        {
        }

        public override string ToString() => base.ToString();
    }
}
