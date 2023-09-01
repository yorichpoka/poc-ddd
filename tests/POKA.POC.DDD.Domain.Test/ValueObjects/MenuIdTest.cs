namespace POKA.POC.DDD.Domain.Test.ValueObjects
{
    public class MenuIdTest
    {
        [Fact]
        public void CanHaveToString()
        {
            // A
            var menuId = BaseObjectId.Create<MenuId>();

            // A
            var stringValue = menuId.ToString();
            var typeName = menuId.GetTypeName();

            // A
            Assert.NotNull(stringValue);
            Assert.NotNull(typeName);
            Assert.True(stringValue.Contains(menuId.Value.ToString()));
            Assert.True(stringValue.Contains(typeName));
        }
    }
}
