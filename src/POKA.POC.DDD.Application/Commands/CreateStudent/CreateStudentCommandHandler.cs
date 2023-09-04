using POKA.POC.DDD.Application.Interfaces;
using POKA.POC.DDD.Domain.Enums;

namespace POKA.POC.DDD.Extensions.Commands
{
    public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, StudentId>
    {
        private readonly IMasterDbRepository _masterDbRepository;
        private readonly IMediator _mediator;

        public CreateStudentCommandHandler(IMasterDbRepository masterDbRepository, IMediator mediator)
        {
            _masterDbRepository = masterDbRepository;
            _mediator = mediator;
        }

        public async Task<StudentId> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            if (request.Email.HasValue() == false)
            {
                var doesStudentEmailTaken = await this._masterDbRepository.Students.AnyAsync(l => l.Email == request.Email, cancellationToken);

                if (doesStudentEmailTaken)
                {
                    throw new AppException(AppErrorEnum.ConflictEmailTaken, request.Email);
                }
            }

            var studentAggregate =  StudentAggregate
                                        .Create(
                                            firstName: request.FirstName,
                                            lastName: request.LastName,
                                            bornOn: request.BornOn,
                                            email: request.Email
                                        );

            await this._masterDbRepository.Students.CreateAsync(studentAggregate, cancellationToken);

            await this._mediator.PublishAndCommitDomainEventAsync(studentAggregate, cancellationToken);

            return studentAggregate.Id;
        }
    }
}
