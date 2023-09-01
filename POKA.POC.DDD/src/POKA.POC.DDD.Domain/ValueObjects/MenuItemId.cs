namespace POKA.POC.DDD.Domain.ValueObjects
{
    public record MenuItemId : BaseObjectId
    {
        protected override string _type => "menuItem";

        public MenuItemId(Guid value)
            : base(value)
        {
        }

        public override string ToString() => base.ToString();
    }
}
