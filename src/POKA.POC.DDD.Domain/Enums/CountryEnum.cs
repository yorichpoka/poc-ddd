using POKA.POC.DDD.Domain.Exceptions;

namespace POKA.POC.DDD.Domain.Enums
{
    public sealed class CountryEnum : BaseEnum<CountryEnum>
    {
        private readonly string _codeISO2;
        private readonly string _codeISO3;

        public static readonly CountryEnum Luxembourg = new(0, "lu", "lux", "Luxembourg");

        public CountryEnum(int value, string name)
            : base(value, name)
        {
            var @enum = FromValue(value);

            _codeISO2 = @enum._codeISO2;
            _codeISO3 = @enum._codeISO3;
        }

        private CountryEnum(int index, string codeISO2, string codeISO3, string name)
            : base(index, name)
        {
            _codeISO2 = codeISO2;
            _codeISO3 = codeISO3;
        }

        public string GetCodeISO2() => this._codeISO2;

        public string GetCodeISO3() => this._codeISO3;

        public static CountryEnum FromValueCodeISO2(string codeISO2)
        {
            var result = CreateEnumerations()
                            .Select(l => l.Value)
                            .FirstOrDefault(l => l._codeISO2.ToLower() == codeISO2.ToLower().Trim());

            if (result == null)
            {
                throw new AppException(AppErrorEnum.EnumValueNotFound, nameof(result));
            }

            return result;
        }
    }
}
