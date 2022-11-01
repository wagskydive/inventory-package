using System;
using UnityEngine;


namespace InventoryPackage
{
    public class Recipe
    {
        public string[] Ingredients { get => ingredients.GetContentInfo();}

        public string ResultType { get => result.Item.TypeName;}

        public ItemAmount Result { get => result;}

        public string ToolType { get => tool.TypeName; }

        public float  CraftingTime { get => craftingTime; }


        readonly Inventory ingredients;
        readonly ItemAmount result;
        readonly float craftingTime;
        readonly ItemType tool;
        

        internal Recipe(Inventory ingredients, float craftingTime, ItemType tool, ItemAmount result)
        {
            this.ingredients = ingredients;
            this.craftingTime = craftingTime;
            this.tool = tool;
            this.result = result;
        }

    }
}
