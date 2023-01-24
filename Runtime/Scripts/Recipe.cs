using System;
using UnityEngine;


namespace InventoryPackage
{
    public class Recipe
    {
        public string[] IngredientNames { get => ingredients.GetContentInfo(); }

        public Inventory Ingredients { get => ingredients; }

        public string ResultType { get => result.Item.TypeName; }

        public ItemAmount Result { get => result; }

        public ItemType ToolType { get => toolType; }

        public float CraftingTime { get => craftingTime; }


        private Inventory ingredients;
        private ItemAmount result;
        private float craftingTime;
        private ItemType toolType;


        internal Recipe(Inventory ingredients, float craftingTime, ItemType tool, ItemAmount result)
        {
            this.ingredients = ingredients;
            this.craftingTime = craftingTime;
            this.toolType = tool;
            this.result = result;
        }

        internal void SetIngredients(Inventory ingredients)
        {
            this.ingredients = ingredients;
        }

        internal void SetToolType(ItemType toolType)
        {
            this.toolType = toolType;
        }

        internal void SetCraftingTime(float craftingTime)
        {
            this.craftingTime = craftingTime;
        }
        internal void RemoveIngredient(ItemType itemType)
        {
            for (int i = 0; i < this.ingredients.Slots.Length; i++)
            {
                if(this.ingredients.Slots[i].Item == itemType)
                {
                    this.ingredients.RemoveSlot(i);
                }
            }
        }

        internal void SetOutputAmount(int outputAmount)
        {
            this.result = new ItemAmount(this.result.Item, outputAmount);
        }


    }
}



