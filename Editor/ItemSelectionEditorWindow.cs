using System;
using UnityEngine;
using UnityEditor;
using InventoryPackage;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

public class ItemSelectionEditorWindow : EditorWindow
{
    public event Action<int> OnSelection;

    ItemType[] items;

    public void SetItems(ItemType[] items)
    {
        this.items = items;
    }
    private GUIContent[] MakeItemTypeButtons(ItemType[] itemTypes)
    {
        GUIContent[] buttons = new GUIContent[itemTypes.Length];

        for (int i = 0; i < itemTypes.Length; i++)
        {
            GUIContent buttonContent = new GUIContent(itemTypes[i].Icon, itemTypes[i].TypeName);
            buttons[i] = buttonContent;
        }
        return buttons;
    }
    int ItemTypeGrid(ItemType[] items, int columns, int slotSize)
    {
        int selected = -1;
        selected = GUILayout.SelectionGrid(selected, MakeItemTypeButtons(items), columns, GUILayout.Width(slotSize * columns), GUILayout.MaxHeight(items.Length / columns * slotSize * 2));
        return selected;
    }


    void OnGUI()
    {
        GUILayout.BeginVertical();
        GUILayout.Label("Item Selector");

        if (items != null)
        {
            int selected = ItemTypeGrid(items, 4, 32);
            if (selected != -1)
            {
                OnSelection?.Invoke(selected);
                this.Close();
            }
        }
        GUILayout.EndVertical();
    }

}















