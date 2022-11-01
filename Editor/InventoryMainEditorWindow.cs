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
        GUILayout.BeginVertical();
        GUILayout.Label("Inventory Editor");


        if (library == null)
        {

            GUILayout.Label("No Library Loaded");
            if (GUILayout.Button(new GUIContent("Load inventory json", "select a json library file")))
            {
                path = EditorUtility.OpenFilePanel("Select Inventory json", "", "json");
                ResetLibrary(JSONDeserializer.CreateLibraryFromJSON(path));

            }
            GUILayout.Label("Or");
            prefab = (GameObject)EditorGUILayout.ObjectField("Prefab", prefab, typeof(GameObject), false);
            if (prefab != null && GUILayout.Button(new GUIContent("Read Library From Prefab", " Read library data from prefab file")))
            {
                if (PrefabToLibrary.ValidatePrefab(prefab))
                {
                    ResetLibrary(PrefabToLibrary.LibraryFromPrefab(prefab));
                }


            }


        }

        if (library != null)
        {
            GUILayout.Label("Library " + library.LibraryName + " loaded");

            int selected = -1;
            int gridcolumns = 8;
            selected = GUILayout.SelectionGrid(selected, MakeLibraryButtons(library), gridcolumns, GUILayout.MaxWidth(250), GUILayout.MaxHeight(library.AllItemTypes.Length / gridcolumns * 64));

            if (selected != -1)
            {
                itemTypeEditorWindow = GetWindow<ItemTypeEditorWindow>("Item Type Editor");
                itemTypeEditorWindow.SetItemType(library.AllItemTypes[selected]);

            }



            if (library.IconsPath == "")
            {


                if (GUILayout.Button(new GUIContent("Load icons from matching names in folder", "Load icons into Item types from matching names")))
                {
                    string iconsPath = "";
                    iconsPath = EditorUtility.OpenFolderPanel("Set Icon Folder", "", "ItemIcons");

                    if (iconsPath != "")
                    {
                        DirectoryInfo dir = new DirectoryInfo(iconsPath);
                        FileInfo[] fileInfos = dir.GetFiles();
                        for (int i = 0; i < fileInfos.Length; i++)
                        {
                            if (fileInfos[i].Extension == ".png" || fileInfos[i].Extension == ".PNG")
                            {
                                string itemTypeName = fileInfos[i].Name.ToString().Split('.')[0];
                                ItemType itemType = LibraryHandler.GetItemTypeByName(itemTypeName, library);
                                if (itemType != null && itemType.TypeName != "Empty")
                                {
                                    string[] pathArray = fileInfos[i].FullName.Split("\\", StringSplitOptions.RemoveEmptyEntries);
                                    string resultPath = "";
                                    bool started = false;
                                    for (int j = 0; j < pathArray.Length; j++)
                                    {
                                        if (started)
                                        {
                                            resultPath = resultPath + "/" + pathArray[j];
                                        }
                                        if (pathArray[j] == "Assets")
                                        {
                                            started = true;
                                            resultPath = resultPath + pathArray[j];
                                        }


                                    }

                                    LibraryHandler.SetIconsPath(resultPath, library);
                                    ItemType.SetIcon(itemType, (Texture2D)AssetDatabase.LoadAssetAtPath(library.IconsPath, typeof(Texture2D)));
                                }

                            }
                        }
                    }
                }

            }
            else
            {

            }

            if (GUILayout.Button(new GUIContent("Recipe Editor", "Open Recipe Editor")))
            {
                recipeEditorWindow = GetWindow<RecipeEditorWindow>("Recipe Editor");
                recipeEditorWindow.SetLibrary(library);
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
        GUILayout.EndVertical();
    }


    GUILayoutOption[] ObjectOptions()
    {
        GUILayoutOption[] options = new GUILayoutOption[1];
        return options;

    }



}