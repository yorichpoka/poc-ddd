using POKA.POC.DDD.Application.Interfaces;

namespace POKA.POC.DDD.Extensions.Commands
{
    public class EnrollStudentToCourseCommandHandler : IRequestHandler<EnrollStudentToCourseCommand, Unit>
    {
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly ICurrentUserProvider _currentUserProvider;
        private readonly IMasterDbRepository _masterDbRepository;
        private readonly IMediator _mediator;

        public EnrollStudentToCourseCommandHandler(
            IEventStoreRepository eventStoreRepository, 
            ICurrentUserProvider currentUserProvider, 
            IMasterDbRepository masterDbRepository, 
            IMediator mediator
        )
        {
            _eventStoreRepository = eventStoreRepository;
            _currentUserProvider = currentUserProvider;
            _masterDbRepository = masterDbRepository;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(EnrollStudentToCourseCommand request, CancellationToken cancellationToken)
        {
            var studentAggregate = await this._masterDbRepository.Students.FirstOrDefaultAsync(l => l.Id == request.StudentId, cancellationToken);

            if (studentAggregate is null)
            {
                throw new AppException(AppErrorEnum.StudentNotFound, request.StudentId.ToString());
            }

            var doesCourseExist = await this._masterDbRepository.Courses.AnyAsync(l => l.Id == request.CourseId, cancellationToken);

            if (doesCourseExist == false)
            {
                throw new AppException(AppErrorEnum.CourseNotFound, request.CourseId.ToString());
            }

            var doesStudentAlreadyEnrolled = studentAggregate
                                                .GetStudentCourses()
                                                .Any(l => l.CourseId == request.CourseId);

            if (doesStudentAlreadyEnrolled)
            {
                throw new AppException(AppErrorEnum.StudentEnrolledToCourseYet, request.StudentId.ToString(), request.CourseId.ToString());
            }

            studentAggregate.EnrolTheCourse(request.CourseId, this._currentUserProvider.Id);

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
