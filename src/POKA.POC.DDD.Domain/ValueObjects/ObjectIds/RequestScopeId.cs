namespace POKA.POC.DDD.Domain.ValueObjects
{
    public record RequestScopeId : BaseObjectId
    {
        protected override string _type => "requestScope";

        public RequestScopeId(Guid value)
            : base(value)
        {
        }

        public override string ToString() => base.ToString();
    }
}
