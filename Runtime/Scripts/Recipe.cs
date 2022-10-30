using System;
using UnityEngine;


namespace InventoryPackage
{
    public class Recipe
    {
        public string[] Ingredients { get => ingredients.GetContentInfo();}

        public string ResultType { get => result.Item.TypeName;}

        public string ToolType { get => tool.TypeName; }

        public float  CraftingTime { get => craftingTime; }


        Inventory ingredients;
        ItemAmount result;
        float craftingTime;
        ItemType tool;
        

        internal Recipe(Inventory ingredients, float craftingTime, ItemType tool, ItemAmount result)
        {
            this.ingredients = ingredients;
            this.craftingTime = craftingTime;
            this.tool = tool;
            this.result = result;
        }

    }
}
