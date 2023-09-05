using POKA.POC.DDD.Application.Interfaces;

namespace POKA.POC.DDD.Extensions.Commands
{
    public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, StudentId>
    {
        private readonly ICurrentUserProvider _currentUserProvider;
        private readonly IMasterDbRepository _masterDbRepository;
        private readonly IMediator _mediator;

        public CreateStudentCommandHandler(ICurrentUserProvider currentUserProvider, IMasterDbRepository masterDbRepository, IMediator mediator)
        {
            _currentUserProvider = currentUserProvider;
            _masterDbRepository = masterDbRepository;
            _mediator = mediator;
        }

        public async Task<StudentId> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            if (request.Email.HasValue())
            {
                var doesStudentEmailTaken = await this._masterDbRepository.Students.AnyAsync(l => l.Email == request.Email, cancellationToken);

                if (doesStudentEmailTaken)
                {
                    throw new AppException(AppErrorEnum.ConflictEmailTaken, request.Email);
                }
            }

            var studentAggregate =  StudentAggregate
                                        .Create(
                                            authorId: this._currentUserProvider.Id,
                                            firstName: request.FirstName,
                                            lastName: request.LastName,
                                            bornOn: request.BornOn,
                                            email: request.Email
                                        );

            await this._masterDbRepository.BeginTransactionAsync(cancellationToken);

            await this._masterDbRepository.Students.CreateAsync(studentAggregate, cancellationToken);

            await this._mediator.PublishAndCommitDomainEventAsync(studentAggregate, cancellationToken);

            await this._masterDbRepository.CommitTransactionAsync(cancellationToken);

            return studentAggregate.Id;
        }
    }
}
