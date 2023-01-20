using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using InventoryPackage;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

public class ItemTypeEditorWindow : EditorWindow
{
    ItemType itemType;
    ItemLibrary itemLibrary;
    Texture2D Icon;


    void OnGUI()
    {
        if (itemType == null)
        {
            return;
        }
        GUILayout.Label("Item Type Editor: " + itemType.TypeName);
        GUILayout.Space(10);

        GUILayout.Label("Name: " + itemType.TypeName);
        ItemType.SetTypeName(itemType, EditorGUILayout.TextField(itemType.TypeName));
        GUILayout.Space(20);

        GUILayout.Label("Description:");
        ItemType.SetDescription(itemType, GUILayout.TextField(itemType.Description));
        GUILayout.Space(20);

        GUILayout.Label("Resource Path:");
        ItemType.SetResourcePath(itemType, GUILayout.TextField(itemType.ResourceFolderPath));
        GUILayout.Space(20);

        string iconFileName = "";
        if (itemType.Icon != null)
        {
            iconFileName = itemType.Icon.name;
        }
        GUILayout.Label("Icon: " + iconFileName);
        EditorGUILayout.ObjectField(itemType.Icon, typeof(Texture2D), false, GUILayout.Width(150), GUILayout.Height(150));

        if (!LibraryHandler.IsRawIngredient(itemType, itemLibrary))
        {
            Recipe recipe = LibraryHandler.GetRecipe(itemType, itemLibrary);
            if (GUILayout.Button("Open Recipe"))
            {
                RecipeEditorWindow recipeEditorWindow = GetWindow<RecipeEditorWindow>("Recipe Editor");
                recipeEditorWindow.SetLibrary(itemLibrary);
                recipeEditorWindow.SetCurrentRecipe(recipe);
            }
            GUILayout.Label(IngredientsString(recipe));

        }

    }

    public void SetLibraryAndItemType(ItemLibrary itemLibrary, ItemType itemType)
    {
        this.itemType = itemType;
        this.itemLibrary = itemLibrary;
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
}
