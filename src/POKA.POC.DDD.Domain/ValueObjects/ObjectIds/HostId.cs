namespace POKA.POC.DDD.Domain.ValueObjects
{
    public record HostId : BaseObjectId
    {
        protected override string _type => "host";

        public HostId(Guid value)
            : base(value)
        {
        }

        public override string ToString() => base.ToString();
    }
}
