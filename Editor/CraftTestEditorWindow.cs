using UnityEngine;
using UnityEditor;
using InventoryPackage;

public class CraftTestEditorWindow : EditorWindow
{
    ItemLibrary library;
    Inventory input;
    Inventory output;
    ItemInstance tool;
    Recipe recipe;


    ItemSelectionEditorWindow itemSelectionWindow;


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
        if (input == null)
        {
            if (GUILayout.Button(new GUIContent("Create test inventory", "click here to create a test inventory")))
            {
                SetInput(InventoryBuilder.CreateInventory(100));
            }
        }
        else
        {
            AddItemButton(input,library);
            EditorObjects.ItemAmountGrid(input.NonEmptySlots, 5,60);
            
        }

        if (output == null)
        {

        }

        if (recipe == null)
        {

        }
        else
        {
            if (Crafter.CanCraft(recipe, input, output, tool))
            {
                if (GUILayout.Button("Craft", "click here to execute the test craft"))
                {
                    Crafter.CraftNow(recipe, input, output, tool);
                }
            }
        }
    }
}
