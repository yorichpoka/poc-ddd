namespace POKA.POC.DDD.Domain.ValueObjects
{
    public record UserId : BaseObjectId
    {
        protected override string _type => "user";

        public UserId(Guid value)
            : base(value)
        {
        }

        public override string ToString() => base.ToString();
    }
}
