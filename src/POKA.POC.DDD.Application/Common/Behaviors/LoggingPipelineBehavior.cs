namespace POKA.POC.DDD.Application.Behaviors
{
    public class LoggingPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<LoggingPipelineBehavior<TRequest, TResponse>> _logger;

        public LoggingPipelineBehavior(ILogger<LoggingPipelineBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var requestName = request.GetType().Name;
            TResponse? result;

            this._logger.LogInformation($"'{requestName}' is running.");

            try
            {
                result = await next();

                this._logger.LogInformation($"'{requestName}' is done with success.");
            }
            catch (Exception ex)
            {
                this._logger.LogInformation($"'{requestName}' is done with error, {ex.StackTrace}.");

                throw;
            }

            return result;
        }
    }
}
