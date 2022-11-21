using System;
using UnityEngine;

namespace InventoryPackage
{
    public class RecipeCreator
    {
        public static Recipe CreateRecipe(ItemAmount result, Inventory ingredients = null, float craftingTime = 1, ItemType tool = null)
        {
            return new Recipe(ingredients, craftingTime, tool, result);
        }

        public static Recipe CopyRecipe(Recipe recipe)
        {
            return new Recipe(recipe.Ingredients, recipe.CraftingTime, recipe.ToolType, recipe.Result);
        }

        public static void AddIngredient(Recipe recipe, ItemAmount ingredient)
        {
            Inventory newInventory;

            if (recipe.Ingredients == null)
            {
                newInventory = InventoryBuilder.CreateInventory(1);
                newInventory.AddInEmptySlot(ingredient, 0);
            }
            else
            {
                newInventory = InventoryBuilder.CreateInventory(recipe.Ingredients.Slots.Length + 1);

                for (int i = 0; i < recipe.Ingredients.Slots.Length; i++)
                {
                    newInventory.AddInEmptySlot(recipe.Ingredients.Slots[i], i);
                }
                newInventory.AddInEmptySlot(ingredient, recipe.Ingredients.Slots.Length);
            }



            recipe.SetIngredients(newInventory);
        }

        public static void RemoveIngredient(Recipe recipe, int index)
        {
            Inventory newInventory = InventoryBuilder.CreateInventory(recipe.Ingredients.Slots.Length - 1);
            for (int i = 0; i < recipe.Ingredients.Slots.Length; i++)
            {
                if (i < index)
                {
                    newInventory.AddInEmptySlot(recipe.Ingredients.Slots[i], i);
                }
                else if (i > index)
                {
                    newInventory.AddInEmptySlot(recipe.Ingredients.Slots[i], i - 1);
                }
            }
            recipe.SetIngredients(newInventory);
        }

        public static void SetIngredientAmount(Recipe recipe, int index, int amount)
        {
            recipe.Ingredients.Slots[index].SetAmount(amount);
        }
    }
}





