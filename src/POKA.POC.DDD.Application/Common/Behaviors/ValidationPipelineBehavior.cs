namespace POKA.POC.DDD.Application.Behaviors
{
    public class ValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IValidator<TRequest>? _validator;

        public ValidationPipelineBehavior(IValidator<TRequest>? validator = null)
        {
            _validator = validator;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (this._validator is not null)
            {
                await this._validator.ValidateAndThrowAsync(request, cancellationToken);
            }

            var result = await next();

            return result;
        }
    }
}
