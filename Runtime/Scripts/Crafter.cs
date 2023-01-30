using System;

namespace InventoryPackage
{
    public static class Crafter
    {
        public static bool CraftNow(Recipe recipe, Inventory input, Inventory output, ItemInstance tool = null)
        {
            if (CanCraft(recipe, input, output, tool))
            {
                for (int i = 0; i < recipe.Ingredients.Slots.Length; i++)
                {
                    InventoryHandler.RemoveFromInventory(recipe.Ingredients.Slots[i], input);

                }
                InventoryHandler.AddToInventory(recipe.Result,output);

                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool CanCraft(Recipe recipe, Inventory input, Inventory output, ItemInstance tool)
        {
            if(recipe.ToolType != null &&  recipe.ToolType != tool.TypeOfItem)
            {
                return false;
            }
            for (int i = 0; i < recipe.Ingredients.Slots.Length; i++)
            {
                ItemAmount ingredient = recipe.Ingredients.Slots[i];
                if(!InventoryHandler.HasAmountOfItem(ingredient.Item,InventoryHandler.GetTotalAmountOfItem(ingredient.Item,recipe.Ingredients),input))
                {
                    return false;
                }
            }
            return true;
        }
    }
}