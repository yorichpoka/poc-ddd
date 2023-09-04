namespace POKA.POC.DDD.Domain.Interfaces
{
    public interface IBaseCommand
    {

    }

    public interface ICommand : IBaseCommand, IRequest<Unit>
    {

    }

    public interface ICommand<TResponse> : IBaseCommand, IRequest<TResponse>
    {

    }
}
