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
    ItemTypeEditorWindow itemTypeEditorWindow;




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

    private GUIContent[] MakeLibraryButtons(ItemLibrary library)
    {


        GUIContent[] buttons = new GUIContent[library.AllItemTypes.Length];

        for (int i = 0; i < library.AllItemTypes.Length; i++)
        {
            GUIContent buttonContent = new GUIContent(library.AllItemTypes[i].Icon, library.AllItemTypes[i].TypeName);
            buttons[i] = buttonContent;
        }
        return buttons;
    }

    private void SetEditorIcons(ItemLibrary library)
    {
        int amountOfItems = library.AllItemTypes.Length;
        for (int i = 0; i < amountOfItems; i++)
        {
            if (library.AllItemTypes[i].Icon != null)
            {
                EditorGUIUtility.SetIconForObject(library.AllItemTypes[i], library.AllItemTypes[i].Icon);
            }
        }
    }


    void OnGUI()
    {
        GUILayout.Label("Inventory Editor");


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

            int selected = -1;
            selected = GUILayout.SelectionGrid(selected, MakeLibraryButtons(library), 8,GUILayout.MaxWidth(250));

            if(selected != -1)
            {
                itemTypeEditorWindow = GetWindow<ItemTypeEditorWindow>("Item Type Editor");
                itemTypeEditorWindow.SetItemType(library.AllItemTypes[selected]);
                
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