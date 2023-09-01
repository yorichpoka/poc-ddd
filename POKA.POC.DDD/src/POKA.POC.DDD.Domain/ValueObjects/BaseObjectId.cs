using POKA.POC.DDD.Domain.Interfaces;
using POKA.POC.DDD.Domain.Enums;

namespace POKA.POC.DDD.Domain.ValueObjects
{
    public record BaseObjectId : IObjectId
    {
        protected virtual string _type { get; } = null!;

        public Guid Value { get; private set; }

        protected BaseObjectId()
        {
            Value = Guid.Empty;
        }

        public BaseObjectId(Guid value)
        {
            Value = value;
        }

        public string GetTypeName() => this._type;

        public static TObjectId Parse<TObjectId>(string value) where TObjectId : BaseObjectId, IObjectId
        {
            try
            {
                var valueParts = value.Split('_');
                var type = valueParts[0];
                var guidValue = Guid.Parse(valueParts[1]);

                var result = (TObjectId)Activator.CreateInstance(typeof(TObjectId), guidValue);

                if (type.ToLower().Trim() != result._type.ToLower())
                {
                    throw new AppException(AppErrorEnum.InvalidParseObjectId);
                }

                return result;
            }
            catch
            {
                throw new AppException(AppErrorEnum.InvalidParseObjectId);
            }
        }

        public static bool Is<TObjectId>(string value) where TObjectId : BaseObjectId, IObjectId
        {
            try
            {
                var result = Parse<TObjectId>(value);

                return true;
            }
            catch { }

            return false;
        }

        public static TObjectId Create<TObjectId>(Guid? fromValue = null) where TObjectId : class, IObjectId =>
            Activator.CreateInstance(typeof(TObjectId), fromValue ?? Guid.NewGuid()) as TObjectId;

        public override string ToString() => $"{this._type}_{this.Value}";
    }
}
