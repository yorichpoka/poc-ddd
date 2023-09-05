using POKA.POC.DDD.Domain.ValueObjects;
using POKA.POC.DDD.Domain.Exceptions;
using POKA.POC.DDD.Domain.Enums;

namespace POKA.POC.DDD.Domain.Entities
{
    public class MenuSectionEntity : BaseEntity<MenuSectionId>
    {
        public MenuId MenuId { get; private set; } = null!;
        public string Name { get; private set; } = null!;
        public string Description { get; private set; } = null!;

        private MenuSectionEntity()
        {
        }

        public MenuSectionEntity(MenuId menuId, string name, string description)
        {
            if (menuId.HasValue() == false)
            {
                throw new AppException(AppErrorEnum.ArgumentNullPassed, nameof(menuId));
            }

            if (name.HasValue() == false)
            {
                throw new AppException(AppErrorEnum.ArgumentNullPassed, nameof(name));
            }

            if (description.HasValue() == false)
            {
                throw new AppException(AppErrorEnum.ArgumentNullPassed, nameof(description));
            }

            Description = description;
            MenuId = menuId;
            Name = name;
        }
    }
}
