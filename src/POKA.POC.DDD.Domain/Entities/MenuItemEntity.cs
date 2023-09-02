﻿using POKA.POC.DDD.Domain.ValueObjects;
using POKA.POC.DDD.Domain.Exceptions;
using POKA.POC.DDD.Domain.Enums;

namespace POKA.POC.DDD.Domain.Entities
{
    public class MenuItemEntity : BaseEntity<MenuItemId>
    {
        public MenuSectionId MenuSectionId { get; private set; } = null!;
        public string Name { get; private set; } = null!;
        public string Description { get; private set; } = null!;

        public MenuItemEntity()
        {
        }

        public MenuItemEntity(MenuSectionId menuSectionId, string name, string description)
        {
            if (menuSectionId.HasValue() == false)
            {
                throw new AppException(AppErrorEnum.ArgumentNullPassed, nameof(menuSectionId));
            }

            if (name.HasValue() == false)
            {
                throw new AppException(AppErrorEnum.ArgumentNullPassed, nameof(name));
            }

            if (description.HasValue() == false)
            {
                throw new AppException(AppErrorEnum.ArgumentNullPassed, nameof(description));
            }

            MenuSectionId = menuSectionId;
            Description = description;
            Name = name;
        }
    }
}
