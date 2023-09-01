using POKA.POC.DDD.Domain.Enums;

namespace POKA.POC.DDD.Domain
{
    public class AppException : ValidationException
    {
        public AppErrorEnum ErrorCode { get; private set; } = AppErrorEnum.Unknow;

        public AppException(string? message = null)
            : base(message)
        {
        }

        public AppException(AppErrorEnum codeError)
            : base(codeError.GetDescription())
        {
            this.ErrorCode = codeError;
        }

        public AppException(AppErrorEnum codeError, params object[] data)
            : base(codeError.GetDescription())
        {
            this.ErrorCode = codeError;

            for (int index = 0; index < data.Length; index++)
            {
                this.Data.Add(index.ToString(), data[index]);
            }
        }

        public AppException(string message, AppErrorEnum codeError)
            : base(message)
        {
            this.ErrorCode = codeError;
        }

        public AppException(string message, AppErrorEnum codeError, params object[] data)
            : base(message)
        {
            this.ErrorCode = codeError;

            for (int index = 0; index < data.Length; index++)
            {
                this.Data.Add(index.ToString(), data[index]);
            }
        }
    }
}
