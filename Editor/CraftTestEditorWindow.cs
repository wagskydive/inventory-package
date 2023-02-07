using UnityEngine;
using UnityEditor;
using InventoryPackage;
using System;

public class CraftTestEditorWindow : EditorWindow
{
    ItemLibrary library;
    Inventory input;
    Inventory output;
    IItemInstance tool;
    Recipe recipe;
    ItemAmount selectedItemAmount;


    ItemSelectionEditorWindow itemSelectionWindow;
    AmountWindow amountWindow;


    public void SetInput(Inventory input)
    {
        this.input = input;
    }

    public void SetOutput(Inventory output)
    {
        this.output = output;
    }

    public void SetTool(ItemInstance tool)
    {
        this.tool = tool;
    }

    public void SetRecipe(Recipe recipe)
    {
        this.recipe = recipe;
    }

    public void SetLibrary(ItemLibrary library)
    {
        this.library = library;
    }


    private void AddItemButton(Inventory inventory, ItemLibrary itemLibrary)
    {
        GUIContent content = EditorGUIUtility.IconContent("CreateAddNew@2x");
        if (GUILayout.Button(content, GUILayout.Width(32), GUILayout.Height(32)))
        {
            itemSelectionWindow = GetWindow<ItemSelectionEditorWindow>();


            itemSelectionWindow.SetItems(itemLibrary.AllItemTypes);
            itemSelectionWindow.OnSelection += AddItemRequest;
        }
    }

    private void AddItemRequest(int itemIndex)
    {
        InventoryHandler.AddToInventory(new ItemAmount(library.AllItemTypes[itemIndex], 1), input);
    }



    void OnGUI()
    {

        // INPUT
        GUILayout.Label("Input Inventory");
        if (input == null)
        {
            if (GUILayout.Button(new GUIContent("Create test inventory", "click here to create a test inventory")))
            {
                SetInput(InventoryBuilder.CreateInventory(100));
            }
        }
        else
        {
            AddItemButton(input, library);

            int itemAmounSelection = EditorObjects.ItemAmountGrid(input.NonEmptySlots, 5, 60);
            if (itemAmounSelection != -1)
            {
                selectedItemAmount = input.NonEmptySlots[itemAmounSelection];
                amountWindow = GetWindow<AmountWindow>();
                amountWindow.SetAmount(selectedItemAmount.Amount);

                amountWindow.OnConfirm += HandleAmountOfInputInventoryItem;
                amountWindow.OnClose += UnsubscribeAmountWindow;
            }

        }

        if (GUILayout.Button(new GUIContent("Select recipe", "click here to select a recipe")))
        {
            itemSelectionWindow = GetWindow<ItemSelectionEditorWindow>();
            itemSelectionWindow.SetItems(LibraryHandler.RecipeResultTypes(library));
            itemSelectionWindow.OnSelection += HandleRecipeSelection;
        }

        if (recipe != null)
        {
            GUILayout.Label("Recipe");
            EditorObjects.RecipeSummery(recipe);
            if (Crafter.CanCraft(recipe, input, output, tool))
            {
                if (GUILayout.Button(new GUIContent("Craft", "click here to execute the test craft")))
                {
                    Crafter.CraftNow(recipe, input, output, tool);
                }

            }
            else
            {
                GUILayout.Label("Can not craft, missing ingredients or tool");
            }

        }

        if (GUILayout.Button(new GUIContent("add tool", "click here to create an item instance to test as a tool")))
        {
            itemSelectionWindow = GetWindow<ItemSelectionEditorWindow>();


            itemSelectionWindow.SetItems(library.AllItemTypes);
            itemSelectionWindow.OnSelection += ToolSelection;
        }
        if (tool != null)
        {
            EditorGUILayout.ObjectField(tool.TypeOfItem.Icon, typeof(Texture2D), false, GUILayout.Width(150), GUILayout.Height(150));
        }

        if (output == null)
        {
            SetOutput(InventoryBuilder.CreateInventory(100));
        }
        else
        {
            EditorObjects.ItemAmountGrid(output.NonEmptySlots, 5, 60);
        }



    }

    private void HandleAmountOfInputInventoryItem(int obj)
    {
        if (obj > selectedItemAmount.Amount)
        {
            int rest = obj - selectedItemAmount.Amount;
            InventoryHandler.AddToInventory(new ItemAmount(selectedItemAmount.Item, rest), input);
        }
        else if(obj < selectedItemAmount.Amount)
        {
            int diffrence  = selectedItemAmount.Amount - obj;
            InventoryHandler.RemoveFromInventory(new ItemAmount(selectedItemAmount.Item, diffrence), input);
        }

    }

    private void UnsubscribeAmountWindow()
    {
        amountWindow.OnConfirm -= selectedItemAmount.SetAmount;
        amountWindow.OnClose -= UnsubscribeAmountWindow;

    }

    private void ToolSelection(int selection)
    {
        TestItemInstance testItemInstance = new TestItemInstance(library.AllItemTypes[selection]);
        this.tool = testItemInstance;
    }

    private void HandleRecipeSelection(int selection)
    {
        SetRecipe(library.AllRecipes[selection]);
        itemSelectionWindow.OnSelection -= HandleRecipeSelection;
    }
}
