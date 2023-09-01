namespace POKA.POC.DDD.Domain.ValueObjects
{
    public record MenuSectionId : BaseObjectId
    {
        protected override string _type => "menuSection";

        public MenuSectionId(Guid value)
            : base(value)
        {
        }

        public override string ToString() => base.ToString();
    }
}
