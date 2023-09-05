using POKA.POC.DDD.Application.Interfaces;

namespace POKA.POC.DDD.Extensions.Commands
{
    public class ChangeStudentAddressCommandHandler : IRequestHandler<ChangeStudentAddressCommand, Unit>
    {
        private readonly ICurrentUserProvider _currentUserProvider;
        private readonly IMasterDbRepository _masterDbRepository;
        private readonly IMediator _mediator;

        public ChangeStudentAddressCommandHandler(ICurrentUserProvider currentUserProvider, IMasterDbRepository masterDbRepository, IMediator mediator)
        {
            _currentUserProvider = currentUserProvider;
            _masterDbRepository = masterDbRepository;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(ChangeStudentAddressCommand request, CancellationToken cancellationToken)
        {
            var studentAggregate = await this._masterDbRepository.Students.FirstOrDefaultAsync(l => l.Id == request.StudentId, cancellationToken);

            if (studentAggregate is null)
            {
                throw new AppException(AppErrorEnum.StudentNotFound, request.StudentId.ToString());
            }

            studentAggregate.ChangeAddress(request.Address, this._currentUserProvider.Id);

            await this._masterDbRepository.BeginTransactionAsync(cancellationToken);

            await this._masterDbRepository.Students.UpdateAsync(studentAggregate.Id, studentAggregate, cancellationToken);

            await this._mediator.PublishAndCommitDomainEventAsync(studentAggregate, cancellationToken);

            await this._masterDbRepository.CommitTransactionAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
