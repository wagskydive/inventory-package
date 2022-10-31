using System.IO;
using System.Collections.Generic;

using UnityEngine;
using UnityEditor;
using InventoryPackage;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System;

public class InventoryMainEditorWindow : EditorWindow
{
    string path = "";

    GUIContent[] itemTypeGuiContents;

    ItemLibrary library;
    RecipeEditorWindow recipeEditorWindow;

    [MenuItem("Inventory Package/Inventory Editor")]
    static void ShowWindow()
    {
        GetWindow<InventoryMainEditorWindow>("Inventory Editor");
    }

    GameObject prefab;

    void ResetLibrary(ItemLibrary library)
    {
        this.library = library;
        SetEditorIcons(library);
        ResetGuiContent(library);
    }

    private void SetEditorIcons(ItemLibrary library)
    {
        int amountOfItems = library.AllItemTypes.Length;
        for (int i = 0; i < amountOfItems; i++)
        {
            //EditorGUIUtility.SetIconForObject(library.AllItemTypes[i], library.AllItemTypes[i].Icon);
        }
    }

    private void ResetGuiContent(ItemLibrary library)
    {
        int amountOfItems = library.AllItemTypes.Length;
        itemTypeGuiContents = new GUIContent[amountOfItems];


        for (int i = 0; i < amountOfItems; i++)
        {
            itemTypeGuiContents[i] = new GUIContent(library.AllItemTypes[i].TypeName, library.AllItemTypes[i].TypeName);

        }
    }

    void OnGUI()
    {
        GUILayout.Label("Inventroy Editor");

        GUILayout.BeginVertical();
        if (library == null)
        {
            if (path == "" && GUILayout.Button(new GUIContent("Select Inventory Location", "select location to read and write inventory data files")))
            {
                path = EditorUtility.OpenFolderPanel("Select Inventory Location", "", "inventory");
            }
            if (path != "")
            {
                GUILayout.TextField(path);
                if (File.Exists(path + "itemLibrary.json"))
                {
                    ResetLibrary(JSONDeserializer.CreateLibraryFromJSON(path));

                }
                else
                {
                    prefab = (GameObject)EditorGUILayout.ObjectField("Prefab", prefab, typeof(GameObject), false);
                    if (GUILayout.Button(new GUIContent("Read Library From Prefab", " Read library data from prefab file")))
                    {

                        ResetLibrary(PrefabToLibrary.LibraryFromPrefab(prefab));

                    }
                }
            }
        }
        GUILayout.EndVertical();











        if (library != null)
        {
            GUILayout.BeginVertical();

            for (int i = 0; i < itemTypeGuiContents.Length; i++)
            {
                GUILayout.BeginHorizontal();
                GUIContent icon = new GUIContent(library.AllItemTypes[i].TypeName, library.AllItemTypes[i].Icon, library.AllItemTypes[i].TypeName);


                GUILayout.EndHorizontal();
            }
            GUILayout.EndVertical();








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