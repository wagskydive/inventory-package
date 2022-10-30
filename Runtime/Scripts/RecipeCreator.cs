using System;
using UnityEngine;

namespace InventoryPackage
{
    public class RecipeCreator
    {
        public static Recipe CreateRecipe(ItemAmount result, Inventory ingredients, float craftingTime, ItemType tool)
        {
            return new Recipe(ingredients,craftingTime,tool,result);
        }

        
    }
}
