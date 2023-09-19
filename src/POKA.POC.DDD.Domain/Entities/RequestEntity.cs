using POKA.POC.DDD.Domain.ValueObjects;
using POKA.POC.DDD.Domain.Enums;

namespace POKA.POC.DDD.Domain.Entities
{
    public class RequestEntity : BaseEntity<RequestId>
    {
        public RequestId? ParentId { get; private set; }
        public RequestScopeId ScopeId { get; private set; }
        public UserId? UserId { get; private set; }
        public string ApplicationPerformer { get; private set; } = null!;
        public RequestStatusEnum Status { get; private set; } = null!;
        public RequestTypeEnum? Type { get; private set; }
        public string Name { get; private set; } = null!;
        public string Data { get; private set; } = null!;
        public string? Error { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public TimeSpan? Duration { get; private set; }

        private RequestEntity()
        {
        }

        public RequestEntity(RequestId id)
        {
            Id = id;
        }

        public RequestEntity(RequestScopeId scopeId, UserId? userId, string applicationPerformer, RequestStatusEnum status, string name, string data, DateTime createdOn, RequestId? parentId = null)
        {
            ApplicationPerformer = applicationPerformer;
            Id = BaseObjectId.Create<RequestId>();
            CreatedOn = createdOn;
            ParentId = parentId;
            ScopeId = scopeId;
            UserId = userId;
            Duration = null;
            Status = status;
            Error = null;
            Type = null;
            Name = name;
            Data = data;
        }

        public void AsCommand() => this.Type = RequestTypeEnum.Command;

        public void AsQuery() => this.Type = RequestTypeEnum.Query;

        public void Success()
        {
            this.Status = RequestStatusEnum.Success;
            this.Complete();
        }

        public void Fail(string? error)
        {
            this.Status = RequestStatusEnum.Fail;
            this.Error = error;
            this.Complete();
        }

        private void Complete() => this.Duration = DateTime.UtcNow - this.CreatedOn;
    }
}
