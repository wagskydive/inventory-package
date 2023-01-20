using System.IO;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using InventoryPackage;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System;

public class InventoryListViewWindow : EditorWindow
{
    Inventory currentInventory;

    public void SetInventory(Inventory inventory)
    {
        currentInventory = inventory;
    }
    void OnGUI()
    {
        ShowList();

    }

    private void ShowList()
    {
        List<ItemAmountNonStackSize> itemsNonStackSize = new List<ItemAmountNonStackSize>();

        if(currentInventory == null || !currentInventory.Slots.Any())
        {
            return;
        }
        for (int i = 0; i < currentInventory.Slots.Length; i++)
        {
            if (currentInventory.Slots[i].Item.TypeName != "Empty")
            {
                bool hasItemType = false;
                for (int j = 0; j < itemsNonStackSize.Count; j++)
                {
                    if (itemsNonStackSize[j].Item == currentInventory.Slots[i].Item)
                    {
                        itemsNonStackSize[j].AddAmount(currentInventory.Slots[i].Amount);
                        hasItemType = true;
                        break;
                    }
                }
                if (!hasItemType)
                {
                    itemsNonStackSize.Add(new ItemAmountNonStackSize(currentInventory.Slots[i].Item, currentInventory.Slots[i].Amount));
                }
            }
        }

        for (int i = 0; i < itemsNonStackSize.Count; i++)
        {
            GUILayout.Label(itemsNonStackSize[i].Amount.ToString() + " " + itemsNonStackSize[i].Item.TypeName);
        }
    }
}