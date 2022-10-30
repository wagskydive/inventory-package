using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using InventoryPackage;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

public class RecipeEditorWindow : EditorWindow
{

    ItemLibrary library;

    Recipe currentRecipe;

    public void SetLibrary(ItemLibrary library)
    {
        this.library = library;
    }

    public void SetCurrentRecipe(Recipe recipe)
    {
        this.currentRecipe = recipe;
    }

    void OnGUI()
    {
        GUILayout.Label("Recipe Editor");

        if (currentRecipe != null)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(currentRecipe.ResultType);
            int selection = GUILayout.SelectionGrid(-1, currentRecipe.Ingredients, 1);
            if (selection != -1)
            {
                Debug.Log("Grid Selection: " + selection);
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Add Ingredient"))
            {

            }


            if (GUILayout.Button("Remove Ingredient"))
            {

            }
            GUILayout.EndHorizontal();

        }

        else
        {
            this.Close();
        }


    }




}