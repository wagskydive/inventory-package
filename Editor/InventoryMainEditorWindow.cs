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

    }

    private void SetEditorIcons(ItemLibrary library)
    {
        int amountOfItems = library.AllItemTypes.Length;
        for (int i = 0; i < amountOfItems; i++)
        {
            //EditorGUIUtility.SetIconForObject(library.AllItemTypes[i], library.AllItemTypes[i].Icon);
        }
    }


    void OnGUI()
    {
        GUILayout.Label("Inventroy Editor");


        if (library == null)
        {
            GUILayout.BeginVertical();
            GUILayout.Label("No Library Loaded");
            if (GUILayout.Button(new GUIContent("Load inventory json", "select a json library file")))
            {
                path = EditorUtility.OpenFilePanel("Select Inventory json", "", "json");
                ResetLibrary(JSONDeserializer.CreateLibraryFromJSON(path));

            }
            GUILayout.Label("Or");
            prefab = (GameObject)EditorGUILayout.ObjectField("Prefab", prefab, typeof(GameObject), false);
            if (GUILayout.Button(new GUIContent("Read Library From Prefab", " Read library data from prefab file")))
            {
                if (prefab != null && PrefabToLibrary.ValidatePrefab(prefab))
                {
                    ResetLibrary(PrefabToLibrary.LibraryFromPrefab(prefab));
                }


            }

            GUILayout.EndVertical();
        }

        if (library != null)
        {
            GUILayout.BeginHorizontal();

            GUILayout.BeginVertical();

            GUILayout.Label("Library " + library.LibraryName + " loaded");

            for (int i = 0; i < library.AllItemTypes.Length; i++)
            {
                //GUILayout.BeginHorizontal();

                GUIContent icon = new GUIContent(library.AllItemTypes[i].Icon, library.AllItemTypes[i].TypeName);
                if (GUILayout.Button(icon, GUILayout.Width(32), GUILayout.Height(32)))
                {

                }

                //GUILayout.EndHorizontal();
            }
            GUILayout.EndVertical();


            GUILayout.BeginVertical();

            if (GUILayout.Button(new GUIContent("Recipe Editor", "Open Recipe Editor")))
            {
                recipeEditorWindow = GetWindow<RecipeEditorWindow>("Recipe Editor");
                recipeEditorWindow.SetLibrary(library);
            }
            GUILayout.EndVertical();



            GUILayout.EndHorizontal();









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