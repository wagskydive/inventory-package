using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using InventoryPackage;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System;
using System.Linq;

public class RecipeEditorWindow : EditorWindow
{

    ItemLibrary library;

    Recipe currentRecipe;

    ItemSelectionEditorWindow itemSelectionWindow;

    public void SetLibrary(ItemLibrary library)
    {
        this.library = library;
    }

    public void SetCurrentRecipe(Recipe recipe)
    {
        this.currentRecipe = recipe;
    }

    void IngredientField(int index)
    {
        ItemAmount itemAmount = currentRecipe.Ingredients.Slots[index];
        EditorGUILayout.BeginHorizontal();
        int amount = itemAmount.Amount;
        amount = EditorGUILayout.IntField(itemAmount.Amount, GUILayout.MaxWidth(60));
        if (amount != currentRecipe.Ingredients.Slots[index].Amount)
        {
            RecipeCreator.SetIngredientAmount(currentRecipe, index, amount);

        }
        GUIContent buttonContent = new GUIContent(itemAmount.Item.Icon, itemAmount.Item.TypeName);
        if (GUILayout.Button(buttonContent, GUILayout.Width(32), GUILayout.Height(32)))
        {
            GetWindow<ItemTypeEditorWindow>("ItemType Editor").SetLibraryAndItemType(library, itemAmount.Item);
        }
        GUILayout.Label(itemAmount.Item.TypeName);

        RemoveIngredientButton(index);
        EditorGUILayout.EndHorizontal();
    }

    ItemType[] ValidItems()
    {
        List<ItemType> invalidItemsForRecipe = new List<ItemType>();
        invalidItemsForRecipe.Add(currentRecipe.Result.Item);
        if (currentRecipe.Ingredients != null)
        {
            for (int i = 0; i < currentRecipe.Ingredients.Slots.Length; i++)
            {
                invalidItemsForRecipe.Add(currentRecipe.Ingredients.Slots[i].Item);
            }
        }
        for (int i = 0; i < library.AllRecipes.Length; i++)
        {
            Inventory recipeIngredients = library.AllRecipes[i].Ingredients;
            if (recipeIngredients != null)
            {
                for (int j = 0; j < recipeIngredients.Slots.Length; j++)
                {
                    if (recipeIngredients.Slots[j].Item == currentRecipe.Result.Item)
                    {
                        invalidItemsForRecipe.Add(library.AllRecipes[i].Result.Item);
                    }
                }
            }

        }

        return LibraryHandler.FilteredTypes(library, invalidItemsForRecipe.ToArray());

    }
    private void AddIngredientButton()
    {
        GUIContent content = EditorGUIUtility.IconContent("CreateAddNew@2x");
        if (GUILayout.Button(content, GUILayout.Width(32), GUILayout.Height(32)))
        {
            itemSelectionWindow = GetWindow<ItemSelectionEditorWindow>();

            List<ItemType> invalidItemsForRecipe = new List<ItemType>();
            invalidItemsForRecipe.Add(currentRecipe.Result.Item);
            if (currentRecipe.Ingredients != null)
            {
                for (int i = 0; i < currentRecipe.Ingredients.Slots.Length; i++)
                {
                    invalidItemsForRecipe.Add(currentRecipe.Ingredients.Slots[i].Item);
                }
            }
            itemSelectionWindow.SetItems(ValidItems());
            itemSelectionWindow.OnSelection += AddIngredientRequest;
        }
    }

    private void AddIngredientRequest(int obj)
    {
        RecipeCreator.AddIngredient(currentRecipe, new ItemAmount(ValidItems()[obj], 1));
        itemSelectionWindow.OnSelection -= AddIngredientRequest;
        Repaint();
    }

    private void RemoveIngredientButton(int index)
    {
        GUIContent content = EditorGUIUtility.IconContent("P4_DeletedLocal@2x");
        if (GUILayout.Button(content, GUILayout.Width(32), GUILayout.Height(32)))
        {
            RecipeCreator.RemoveIngredient(currentRecipe, index);
            Repaint();
        }
    }

    void OnGUI()
    {
        GUILayout.Label("Recipe Editor");

        if (currentRecipe != null)
        {
            GUILayout.BeginHorizontal();

            GUILayout.BeginVertical();
            GUILayout.Label(currentRecipe.ResultType);
            GUIContent buttonContent = new GUIContent(currentRecipe.Result.Item.Icon, currentRecipe.Result.Item.TypeName);
            GUILayout.Button(buttonContent, GUILayout.Width(64), GUILayout.Height(64));
            GUILayout.EndVertical();

            GUILayout.BeginVertical();
            GUILayout.Label("Ingredients");
            if (currentRecipe.Ingredients != null)
            {
                for (int i = 0; i < currentRecipe.Ingredients.Slots.Length; i++)
                {
                    IngredientField(i);
                }
            }


            AddIngredientButton();

            GUILayout.EndVertical();

            GUILayout.EndHorizontal();

            GUILayout.Label("CraftingTime");
            float craftingTime = currentRecipe.CraftingTime;
            craftingTime = EditorGUILayout.FloatField(craftingTime);
            if (craftingTime != currentRecipe.CraftingTime)
            {
                RecipeCreator.SetCraftingTime(currentRecipe, craftingTime);
            }

            GUILayout.Label("Output Amount");
            int outputAmount = currentRecipe.Result.Amount;
            outputAmount = EditorGUILayout.IntField(outputAmount);
            if (outputAmount != currentRecipe.Result.Amount)
            {
                RecipeCreator.SetOutputAmount(currentRecipe, outputAmount);
            }



            GUIContent toolTypeButtonContent;
            if (currentRecipe.ToolType != null)
            {
                GUILayout.Label("Required tool");
                toolTypeButtonContent = new GUIContent(currentRecipe.ToolType.Icon, currentRecipe.ToolType.TypeName);
            }
            else
            {
                GUILayout.Label("No tool required");
                toolTypeButtonContent = EditorGUIUtility.IconContent("CreateAddNew@2x");
            }
            if (GUILayout.Button(toolTypeButtonContent, GUILayout.Width(32), GUILayout.Height(32)))
            {
                itemSelectionWindow = GetWindow<ItemSelectionEditorWindow>();
                itemSelectionWindow.SetItems(ValidItems());
                itemSelectionWindow.OnSelection += SetToolType;
            }


            GUILayout.Label("Raw Ingredients");
            GUIContent rawIngedientButtonContent = new GUIContent("show");
            if (GUILayout.Button(rawIngedientButtonContent, GUILayout.Width(32), GUILayout.Height(32)))
            {
                InventoryListViewWindow inventoryListViewWindow = GetWindow<InventoryListViewWindow>();
                inventoryListViewWindow.SetInventory(LibraryHandler.GetRawIngredientsOfRecipe(currentRecipe, library));
            }

            GUILayout.Label("Delete recipe");
            GUIContent content = EditorGUIUtility.IconContent("P4_DeletedLocal@2x");
            if (GUILayout.Button(content, GUILayout.Width(32), GUILayout.Height(32)))
            {
                LibraryHandler.RemoveRecipe(library, currentRecipe);
                Close();
            }

        }
    }

    private void SetToolType(int obj)
    {
        RecipeCreator.SetToolType(currentRecipe, ValidItems()[obj]);
        Repaint();
    }
}