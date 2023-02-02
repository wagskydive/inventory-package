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
    ItemSelectionEditorWindow itemSelectionWindow;

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
        minSize = new Vector2(150, 150);
        GUILayout.BeginVertical();
        if (GUILayout.Button(new GUIContent("Load library json", "select a json library file")))
        {
            path = EditorUtility.OpenFilePanel("Select Library json", "", "json");
            ResetLibrary(JSONDeserializer.CreateLibraryFromJSON(path));
            JSONSerializer.SaveLoaderFile(path);
        }

        if (library == null)
        {
            string loaderPath = JSONDeserializer.LoaderPath();

            if (loaderPath != "")
            {
                ResetLibrary(JSONDeserializer.CreateLibraryFromJSON(loaderPath));
            }

            GUILayout.Label("No Library Loaded");

            GUILayout.Label("Or");
            prefab = (GameObject)EditorGUILayout.ObjectField("Prefab", prefab, typeof(GameObject), false);
            if (prefab != null && GUILayout.Button(new GUIContent("Read Library From Prefab", " Read library data from prefab file")))
            {
                if (PrefabToLibrary.ValidatePrefab(prefab))
                {
                    ResetLibrary(PrefabToLibrary.LibraryFromPrefab(prefab));
                }
            }
            GUILayout.Label("Or");
            if (GUILayout.Button(new GUIContent("Load library from png files in folder", "select a json library file")))
            {
                path = EditorUtility.OpenFolderPanel("Select Folder with png files", "", "png");


                ItemLibrary newLibrary = PngFinder.CreateItemLibraryFromPngFiles(path);
                if (newLibrary != null)
                {
                    ResetLibrary(newLibrary);
                }
            }

        }


        if (library != null)
        {
            GUILayout.Label("Library " + library.LibraryName + " loaded");

            int selected = -1;

            selected = EditorObjects.ItemTypeGrid(library.AllItemTypes, 7, 64);

            if (selected != -1)
            {
                itemTypeEditorWindow = GetWindow<ItemTypeEditorWindow>("Item Type Editor");
                itemTypeEditorWindow.SetLibraryAndItemType(library, library.AllItemTypes[selected]);
            }

            if (library.DefaultResourcePath == "")
            {
                if (GUILayout.Button(new GUIContent("Set Resource folder", "Set the default folder where the resources are stored")))
                {
                    string resourcePath = EditorUtility.OpenFolderPanel("Set Resource Folder", "", "resource folder");
                    if (resourcePath.Contains("/Assets/") && resourcePath.Contains("/Resources/"))
                    {
                        resourcePath = resourcePath.Split("/Assets/")[1];
                        resourcePath = "Assets/" + resourcePath;
                        LibraryHandler.SetResourcePath(resourcePath, library);
                        AddDefaultResourceFolderToItemTypes();
                        Repaint();
                    }
                    else
                    {
                        EditorUtility.DisplayDialog("Warning", "The default Resource folder has to be inside the 'Assets' folder and a folder named 'Resources' needs to be present in the chosen path.", "ok");
                    }
                }

            }

            if (GUILayout.Button(new GUIContent("Add New Item Type", "Create a new item type")))
            {
                itemTypeEditorWindow = GetWindow<ItemTypeEditorWindow>("Item Type Editor");
                ItemType newItemType = ItemType.CreateNew("my new item type", 100, "no description written", library.DefaultResourcePath);
                LibraryHandler.AddItemType(newItemType, library);
                itemTypeEditorWindow.SetLibraryAndItemType(library, newItemType);
            }

            if (GUILayout.Button(new GUIContent("Remove Item Type", "remove an item type from the library")))
            {
                itemSelectionWindow = GetWindow<ItemSelectionEditorWindow>();
                itemSelectionWindow.SetItems(library.AllItemTypes);
                itemSelectionWindow.OnSelection += RemoveItemTypeFromLibrary;
            }

            GUILayout.Label("Recipes");
            if (library.AllRecipes != null)
            {
                int recipeSelection = EditorObjects.RecipesGrid(library.AllRecipes, 7, 64);
                if (recipeSelection != -1)
                {
                    recipeEditorWindow = GetWindow<RecipeEditorWindow>("Recipe Editor");
                    recipeEditorWindow.SetLibrary(library);
                    recipeEditorWindow.SetCurrentRecipe(library.AllRecipes[recipeSelection]);
                }
            }
            AddRecipeButton();

            if (GUILayout.Button(new GUIContent("Crafting Tester", "open the crafting tester")))
            {
                GetWindow<CraftTestEditorWindow>().SetLibrary(library);
            }
            GUILayout.BeginHorizontal();
            if (GUILayout.Button(new GUIContent("Save Library", "Save Changes"), GUILayout.Width(100)))
            {
                SaveChanges(library);
            }
            if (GUILayout.Button(new GUIContent("Cancel changes", "undo all changes"), GUILayout.Width(100)))
            {
                CancelChanges();
            }

            GUILayout.EndHorizontal();
        }
        GUILayout.EndVertical();
    }

    private void AddDefaultResourceFolderToItemTypes()
    {
        for (int i = 0; i < library.AllItemTypes.Length; i++)
        {
            ItemType itemType = library.AllItemTypes[i];
            if (itemType.ResourceFolderPath == null || itemType.ResourceFolderPath == "")
            {
                ItemType.SetResourcePath(itemType, library.DefaultResourcePath);
            }
        }
    }
    private void RemoveItemTypeFromLibrary(int index)
    {
        LibraryHandler.RemoveItemType(library, index);
    }

    private void CancelChanges()
    {
        ResetLibrary(JSONDeserializer.CreateLibraryFromJSON(path));
    }

    private void SaveChanges(ItemLibrary library)
    {
        string savePath = "";
        savePath = EditorUtility.SaveFilePanel("Save Library", savePath, library.LibraryName, "json");
        if (savePath != "")
        {
            JSONSerializer.SaveLibrary(library, savePath);
        }
    }

    private void AddRecipeButton()
    {
        GUIContent content = EditorGUIUtility.IconContent("CreateAddNew@2x");
        if (GUILayout.Button(content, GUILayout.Width(32), GUILayout.Height(32)))
        {
            itemSelectionWindow = GetWindow<ItemSelectionEditorWindow>();
            itemSelectionWindow.SetItems(library.AllItemTypes);
            itemSelectionWindow.OnSelection += CreateNewRecipe;
        }
    }

    private void CreateNewRecipe(int obj)
    {
        LibraryHandler.AddRecipeToLibrary(library, RecipeCreator.CreateRecipe(new ItemAmount(library.AllItemTypes[obj], 1)));

        itemSelectionWindow.OnSelection -= CreateNewRecipe;
    }


    GUILayoutOption[] ObjectOptions()
    {
        GUILayoutOption[] options = new GUILayoutOption[1];
        return options;

    }



}