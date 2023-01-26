using System;
using System.Collections.Generic;
using System.Linq;

namespace InventoryPackage
{
    public static class InventoryHandler
    {
        public static void AddToInventory(ItemAmount amountLeft, Inventory inventory)
        {
            if (HasSlotWithItemWithSpace(amountLeft.Item, inventory))
            {
                int[] slotsWithItemType = GetSlotsWithItemType(amountLeft.Item, inventory);

                for (int i = 0; i < slotsWithItemType.Length; i++)
                {

                    inventory.Slots[slotsWithItemType[i]].AddAmount(amountLeft.Amount, out amountLeft);
                    if (amountLeft.Item.TypeName == ItemType.Empty().TypeName)
                    {
                        break;
                    }
                    else
                    {
                        if(HasEmptySlot(inventory))
                        {
                            inventory.AddInEmptySlot(amountLeft, GetNextEmptySlot(inventory));
                        }
                    }
                }
            }
            else
            {
                if (HasEmptySlot(inventory))
                {
                    inventory.AddInEmptySlot(amountLeft, GetNextEmptySlot(inventory));
                }
            }
        }

        private static bool HasSlotWithItemWithSpace(ItemType item, Inventory inventory)
        {
            for(int i = 0; i < inventory.Slots.Length; i++) 
            {
                if(inventory.Slots[i].Item == item && inventory.Slots[i].Amount < item.StackSize)
                {
                    return true;
                }   
            }
            return false;
        }

        public static void AddToInventory(Inventory toAdd, Inventory toRecieve)
        {
            foreach (ItemAmount itemAmount in toAdd.Slots)
            {
                if (itemAmount.Item.TypeName != ItemType.Empty().TypeName)
                {
                    InventoryHandler.AddToInventory(itemAmount, toRecieve);
                }

            }
        }


        public static bool HasEmptySlot(Inventory inventory)
        {

            for (int i = 0; i < inventory.Slots.Length; i++)
            {
                if (inventory.Slots[i].Item.TypeName == ItemType.Empty().TypeName)
                {
                    return true;
                }
            }
            return false;
        }

        public static int GetNextEmptySlot(Inventory inventory)
        {
            for (int i = 0; i < inventory.Slots.Length; i++)
            {
                if (inventory.Slots[i].Item.TypeName == ItemType.Empty().TypeName)
                {
                    return i;
                }
            }
            return -1;
        }

        public static void PickSlotFromInventory(int slotIndex, Inventory inventory, out ItemAmount picked)
        {
            picked = new ItemAmount(inventory.Slots[slotIndex].Item, inventory.Slots[slotIndex].Amount);
            inventory.RemoveFromSlot(slotIndex);

        }

        public static void PickFromSlot(int slotIndex, int amount, Inventory inventory, out ItemAmount picked)
        {
            if (inventory.Slots[slotIndex].Amount >= amount)
            {
                picked = new ItemAmount(inventory.Slots[slotIndex].Item, amount);
                ItemAmount amountLeft;
                inventory.Slots[slotIndex].RemoveAmount(amount, out amountLeft);
            }
            else
            {
                picked = new ItemAmount(ItemType.Empty(), 0);
            }

        }

        public static int[] GetSlotsWithItemType(ItemType type, Inventory inventory)
        {
            List<int> slotsWithItemType = new List<int>();
            for (int i = 0; i < inventory.Slots.Length; i++)
            {
                if (inventory.Slots[i].Item == type)
                {
                    slotsWithItemType.Add(i);
                }
            }

            return slotsWithItemType.ToArray();

        }


        public static void RemoveFromInventory(ItemAmount itemAmount, Inventory inventory)
        {
            if (GetTotalAmountOfItem(itemAmount.Item, inventory) >= itemAmount.Amount)
            {

            }
        }

        public static int GetTotalAmountOfItem(ItemType itemType, Inventory inventory)
        {
            int totalAmount = 0;
            foreach (ItemAmount itemAmount in inventory.Slots)
            {
                if (itemAmount.Item == itemType)
                {
                    totalAmount += itemAmount.Amount;
                }
            }
            return totalAmount;
        }

        public static bool HasAmountOfItem(ItemType itemType, int amount, Inventory inventory)
        {

            if (GetTotalAmountOfItem(itemType, inventory) >= amount)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static object GetAmountOfItem(ItemType itemType, Inventory inventory)
        {
            int amount = 0;
            for (int i = 0; i < inventory.Slots.Length; i++)
            {
                if (inventory.Slots[i].Item == itemType)
                {
                    amount += inventory.Slots[i].Amount;
                }
            }
            return amount;
        }

        public static ItemType GetHighestLevelItemInInventory(Inventory inventory, ItemLibrary library)
        {
            ItemType highest =  inventory.Slots[0].Item;

            for(int i = 0; i < inventory.Slots.Length; i++)
            {
                if(inventory.Slots[i].Item.TypeName != "Empty")
                {
                    if(highest.TypeName == "Empty" || LibraryHandler.GetRootDistance(highest, library) < LibraryHandler.GetRootDistance(inventory.Slots[i].Item, library))
                    {
                        highest = inventory.Slots[i].Item;
                    }
                }
            }
            return highest;
        }
    }
}