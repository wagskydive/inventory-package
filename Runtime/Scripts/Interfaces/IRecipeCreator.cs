namespace InventoryPackage
{
    public interface IRecipeCreator
    {
        public Recipe CreateRecipe(ItemAmount[] result, ItemAmount[] ingredients, float craftingTime, int craftingTier);
    }
}
