namespace POKA.POC.DDD.Domain.ValueObjects
{
    public record ExamId : BaseObjectId
    {
        protected override string _type => "exam";

        public ExamId(Guid value)
            : base(value)
        {
        }

        public override string ToString() => base.ToString();
    }
}
