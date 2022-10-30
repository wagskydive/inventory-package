using System.IO;
using System.Collections.Generic;

using UnityEngine;
using UnityEditor;
using InventoryPackage;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

public class InventoryMainEditorWindow : EditorWindow
{
    string path = "Assets/inventory-package/UnitTests/TestResources/TestItemLibrary.json";




    ItemLibrary library;
    RecipeEditorWindow recipeEditorWindow;

    [MenuItem("Inventory Package/Inventory Editor")]
    static void ShowWindow()
    {
        GetWindow<InventoryMainEditorWindow>("Inventory Editor");
    }

    GameObject prefab;
    void OnGUI()
    {
        GUILayout.Label("Inventroy Editor");

        //inventoryPackageManager = EditorGUILayout.PropertyField()
        //GUILayout.ObjectField(inventoryPackageManager
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Read Library From Prefab"))
        {
            library = JSONDeserializer.CreateLibraryFromJSON(path);
        }
                if (GUILayout.Button("Read Library From Json"))
        {
            library = JSONDeserializer.CreateLibraryFromJSON(path);
        }
        GUILayout.EndHorizontal();


        path = GUILayout.TextField(path, 256);





        prefab = (GameObject)EditorGUILayout.ObjectField("Example GO", prefab, typeof(GameObject), true);
        if (library != null)
        {
            int selection = GUILayout.SelectionGrid(-1, LibraryHandler.LibraryNames(library), 10);
            if (selection != -1)
            {
                Debug.Log("Grid Selection: " + selection);
            }

            if (library.AllRecipes != null)
            {
                int recipeSelection = GUILayout.SelectionGrid(-1, LibraryHandler.RecipesResultTypes(library), 10);
                if (recipeSelection != -1)
                {
                    recipeEditorWindow = GetWindow<RecipeEditorWindow>("Recipe Editor");
                    recipeEditorWindow.SetLibrary(library);
                    recipeEditorWindow.SetCurrentRecipe(library.AllRecipes[recipeSelection]);
                }

            }
        }

    }
    GUILayoutOption[] ObjectOptions()
    {
        GUILayoutOption[] options = new GUILayoutOption[1];
        return options;

    }



}