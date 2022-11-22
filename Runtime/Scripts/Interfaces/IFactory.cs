namespace InventoryPackage
{
    public interface IFactory
    {
        public ItemAmount[] CookRecipe(Recipe recipe);
    }
}