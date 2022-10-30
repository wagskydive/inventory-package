using System;
using UnityEngine;


namespace InventoryPackage
{
    public class Recipe
    {
        ItemAmount[] ingredients;
        float craftingTime;
        int craftingTier;

        ItemAmount[] result;

        internal Recipe(ItemAmount[] ingredients, float craftingTime, int craftingTier = 0)
        {
            this.ingredients = ingredients;
            this.craftingTime = craftingTime;
            this.craftingTier = craftingTier;           
        }
    }
}
