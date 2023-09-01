using POKA.POC.DDD.Domain.ValueObjects;
using POKA.POC.DDD.Domain.Exceptions;

namespace POKA.POC.DDD.Domain.Entities
{
    public class MenuSectionEntity : BaseEntity<MenuSectionId>
    {
        public MenuId MenuId { get; private set; } = null!;
        public string Name { get; private set; } = null!;
        public string Description { get; private set; } = null!;

        public MenuSectionEntity()
        {
        }

        public MenuSectionEntity(MenuId menuId, string name, string description)
        {
            if (menuId.HasValue() == false)
            {
                throw new AppException(Enums.AppErrorEnum.ArgumentNullPassed, nameof(menuId));
            }

            if (name.HasValue() == false)
            {
                throw new AppException(Enums.AppErrorEnum.ArgumentNullPassed, nameof(name));
            }

            if (description.HasValue() == false)
            {
                throw new AppException(Enums.AppErrorEnum.ArgumentNullPassed, nameof(description));
            }

            Description = description;
            MenuId = menuId;
            Name = name;
        }
    }
}
