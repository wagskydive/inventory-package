using UnityEngine;
using InventoryPackage;
using System;
using System.Collections.Generic;

public static class EditorObjects
{
    public static int ItemTypeGrid(ItemType[] items, int columns, int slotSize)
    {
        int selected = -1;
        var height = slotSize * Mathf.CeilToInt(items.Length / (float)columns);
        selected = GUILayout.SelectionGrid(selected, MakeItemTypeButtons(items), columns, GUILayout.Width(slotSize * columns), GUILayout.Height(height));
        return selected;
    }

    public static int RecipesGrid(Recipe[] recipes, int columns, int slotSize)
    {
        int selected = -1;
        var height = slotSize * Mathf.CeilToInt(recipes.Length / (float)columns);
        selected = GUILayout.SelectionGrid(selected, MakeRecipeButtons(recipes), columns, GUILayout.Width(slotSize * columns), GUILayout.Height(height));
        return selected;
    }


    public static GUIContent[] MakeItemTypeButtons(ItemType[] itemTypes)
    {
        GUIContent[] buttons = new GUIContent[itemTypes.Length];

        for (int i = 0; i < itemTypes.Length; i++)
        {
            GUIContent buttonContent = new GUIContent(itemTypes[i].Icon, itemTypes[i].TypeName);
            buttons[i] = buttonContent;
        }
        return buttons;
    }

    public static GUIContent[] MakeRecipeButtons(Recipe[] recipes)
    {
        GUIContent[] buttons = new GUIContent[recipes.Length];

        for (int i = 0; i < recipes.Length; i++)
        {
            Recipe recipe = recipes[i];
            GUIContent buttonContent = new GUIContent(recipes[i].Result.Item.Icon, recipes[i].Result.Item.TypeName + IngredientsString(recipe));
            buttons[i] = buttonContent;
        }
        return buttons;
    }

    private static string IngredientsString(Recipe recipe)
    {
        string recipeString = "";
        if (recipe.Ingredients != null)
        {
            recipeString = "\nIngredients: ";
            for (int j = 0; j < recipe.Ingredients.Slots.Length; j++)
            {
                ItemAmount ingredient = recipe.Ingredients.Slots[j];
                recipeString = recipeString + "\n      " + ingredient.Item.TypeName + ": " + ingredient.Amount.ToString();
            }
        }
        return recipeString;
    }

    internal static int ItemAmountGrid(ItemAmount[] slots, int columns, int slotSize)
    {
        int selected = -1;
        var height = slotSize * Mathf.CeilToInt(slots.Length / (float)columns);
        selected = GUILayout.SelectionGrid(selected, MakeItemAmountButtons(slots), columns, GUILayout.Width(slotSize * columns), GUILayout.Height(height));
        return selected;
    }

    private static GUIContent[] MakeItemAmountButtons(ItemAmount[] slots)
    {
        List<GUIContent> buttons = new List<GUIContent>();

        for (int i = 0; i < slots.Length; i++)
        {
            GUIContent buttonContent = new GUIContent(slots[i].Amount.ToString(), slots[i].Item.Icon, slots[i].Item.TypeName);
            buttons.Add(buttonContent);
        }
        return buttons.ToArray();
    }

    public static ItemAmount InventorySelectionGrid(Inventory inventory, int columns, int slotSize)
    {
        GUILayout.Label("Inventory");
        return inventory.NonEmptySlots[ItemAmountGrid(inventory.NonEmptySlots, columns, slotSize)];
    }
}
