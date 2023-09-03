﻿using POKA.POC.DDD.Domain.DomainEvents;
using POKA.POC.DDD.Domain.ValueObjects;
using POKA.POC.DDD.Domain.Interfaces;
using POKA.POC.DDD.Domain.Exceptions;
using POKA.POC.DDD.Domain.Enums;

namespace POKA.POC.DDD.Domain.Aggregates
{
    public class MenuAggregate : AggregateRoot<MenuId>
    {
        private HashSet<MenuSectionId> _menuSections = new();
        private HashSet<Rating> _ratings = new();

        public HostId HostId { get; private set; } = null!;
        public string Name { get; private set; } = null!;
        public string Description { get; private set; } = null!;

        public float GetAverageRatings() => (
            this._ratings
                .Select(l => l.Score)
                .Average()
        );

        public ReadOnlyCollection<MenuSectionId> GetMenuSections() => (
            this._menuSections
                .ToList()
                .AsReadOnly()
        );

        public static MenuAggregate Create(HostId hostId, string name, string description, UserId? authorId = null)
        {
            if (hostId.HasValue() == false)
            {
                throw new AppException(AppErrorEnum.ArgumentNullPassed, nameof(hostId));
            }

            if (name.HasValue() == false)
            {
                throw new AppException(AppErrorEnum.ArgumentNullPassed, nameof(name));
            }

            if (description.HasValue() == false)
            {
                throw new AppException(AppErrorEnum.ArgumentNullPassed, nameof(description));
            }

            var menuId = BaseObjectId.Create<MenuId>();
            var menuAggregate = new MenuAggregate();

            var domainEvent = new MenuCreated(menuId, hostId, name, description, DateTime.UtcNow, authorId);

            menuAggregate.ApplyUncommittedDomainEvent(domainEvent);

            return menuAggregate;
        }

        public void Rate(UserId userId, float score, UserId? authorId = null)
        {
            if (userId.HasValue() == false)
            {
                throw new AppException(AppErrorEnum.ArgumentNullPassed, nameof(userId));
            }

            var domainEvent = new MenuRated(this.Id, this.Version, userId, score, DateTime.UtcNow, authorId);

            ApplyUncommittedDomainEvent(domainEvent);
        }

        public override void ApplyDomainEventImplementation(IDomainEvent<MenuId> domainEvent)
        {
            switch (domainEvent)
            {
                #region MenuCreated

                case MenuCreated e:
                    this.Description = e.Description;
                    this.HostId = e.HostId;
                    this.Name = e.Name;

                    this.CreatedByUserId = e.AuthorId;
                    this.CreatedOn = e.On;
                    this.Version = e.Version;
                    this.Id = e.Id;
                    break;

                #endregion

                #region MenuRated

                case MenuRated e:
                    var ratings = new Rating(e.UserId, e.Score, e.On);

                    this._ratings.Add(ratings);

                    this.LastUpdatedOn = e.On;
                    this.Version = e.Version;
                    break;

                #endregion

                default:
                    throw new AppException(AppErrorEnum.NotImplemented, nameof(ApplyDomainEventImplementation));
            }
        }

        public override void Snapshot()
        {
            throw new NotImplementedException();
        }
    }
}
