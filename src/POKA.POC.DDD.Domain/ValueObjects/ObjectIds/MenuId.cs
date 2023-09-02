namespace POKA.POC.DDD.Domain.ValueObjects
{
    public record MenuId : BaseObjectId
    {
        protected override string _type => "menu";

        public MenuId(Guid value)
            : base(value)
        {
        }

        public override string ToString() => base.ToString();
    }
}
