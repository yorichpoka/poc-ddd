﻿using POKA.POC.DDD.Application.Interfaces;

namespace POKA.POC.DDD.Extensions.Commands
{
    public class ChangeStudentBirthdateCommandHandler : IRequestHandler<ChangeStudentBirthdateCommand, Unit>
    {
        private readonly IStudentValidationService _studentValidationService;
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly ICurrentUserProvider _currentUserProvider;
        private readonly IMasterDbRepository _masterDbRepository;
        private readonly IMediator _mediator;

        public ChangeStudentBirthdateCommandHandler(
            IStudentValidationService studentValidationService, 
            IEventStoreRepository eventStoreRepository, 
            ICurrentUserProvider currentUserProvider, 
            IMasterDbRepository masterDbRepository, 
            IMediator mediator
        )
        {
            _studentValidationService = studentValidationService;
            _eventStoreRepository = eventStoreRepository;
            _currentUserProvider = currentUserProvider;
            _masterDbRepository = masterDbRepository;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(ChangeStudentBirthdateCommand request, CancellationToken cancellationToken)
        {
            var studentAggregate = await this._masterDbRepository.Students.FirstOrDefaultAsync(l => l.Id == request.StudentId, cancellationToken);

            if (studentAggregate is null)
            {
                throw new AppException(AppErrorEnum.StudentNotFound, request.StudentId.ToString());
            }

            studentAggregate.ChangeBirthdate(request.Birthdate, this._currentUserProvider.Id);

            this._studentValidationService.ValidateAge(studentAggregate);

            await this._masterDbRepository.BeginTransactionAsync(cancellationToken);
            {
                await this._masterDbRepository.Students.UpdateAsync(studentAggregate.Id, studentAggregate, cancellationToken);
                await this._eventStoreRepository.SaveFromAggregateAsync(studentAggregate, cancellationToken);
                await this._mediator.PublishAndCommitDomainEventAsync(studentAggregate, cancellationToken);
            }
            await this._masterDbRepository.CommitTransactionAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
