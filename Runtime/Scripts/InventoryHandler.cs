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
                        if (HasEmptySlot(inventory))
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
            for (int i = 0; i < inventory.Slots.Length; i++)
            {
                if (inventory.Slots[i].Item == item && inventory.Slots[i].Amount < item.StackSize)
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
        public static void AddToInventory(Inventory toAdd, Inventory toRecieve, float multiplier)
        {
            for (int i = 0; i < multiplier; i++)
            {
                AddToInventory(toAdd, toRecieve);
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
            inventory.RemoveSlot(slotIndex);

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
                for (int i = 0; i < inventory.Slots.Length; i++)
                {
                    if (inventory.Slots[i].Item == itemAmount.Item)
                    {
                        if (inventory.Slots[i].Amount == itemAmount.Amount)
                        {
                            inventory.RemoveSlot(i);
                            return;
                        }
                        else if (inventory.Slots[i].Amount > itemAmount.Amount)
                        {
                            inventory.RemoveAmountFromSlot(inventory.Slots[i].Amount, i);
                            return;
                        }
                        else
                        {
                            inventory.RemoveSlot(i);
                        }
                    }
                }

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


        public static bool CanCraftDirectly(Recipe recipe, Inventory inventory)
        {
            foreach (ItemAmount itemAmount in recipe.Ingredients.Slots)
            {
                if (!HasAmountOfItem(itemAmount.Item, itemAmount.Amount, inventory))
                {
                    return false;
                }

            }
            return true;
        }

        public static Inventory DuplicateInventory(Inventory inventory)
        {
            Inventory newInventory = InventoryBuilder.CreateInventory(inventory.Slots.Length);
            InventoryHandler.AddToInventory(inventory, newInventory);
            return newInventory;
        }


        public static ItemAmount CraftFromInventory(Recipe recipe, Inventory inventory)
        {
            if (InventoryHandler.CanCraftDirectly(recipe, inventory))
            {
                foreach (ItemAmount ingredient in recipe.Ingredients.Slots)
                {
                    InventoryHandler.RemoveFromInventory(ingredient, inventory);
                }

                return new ItemAmount(recipe.Result.Item, recipe.Result.Amount);
            }
            else
            {
                return ItemAmount.Empty();
            }
        }

        public static bool CanCraftRaw(Recipe recipe, Inventory inventory, ItemLibrary itemLibrary)
        {
            Inventory duplicate = InventoryHandler.DuplicateInventory(inventory);
            for (int i = 0; i < recipe.Ingredients.Slots.Length; i++)
            {
                if (InventoryHandler.HasAmountOfItem(recipe.Ingredients.Slots[i].Item, recipe.Ingredients.Slots[i].Amount, duplicate))
                {
                    InventoryHandler.RemoveFromInventory(recipe.Ingredients.Slots[i],duplicate);
                }
                else
                {
                    if(LibraryHandler.IsRawIngredient(recipe.Ingredients.Slots[i].Item, itemLibrary))
                    {
                        return false;
                    }
                    else
                    {
                        Recipe parent = LibraryHandler.GetRecipeByName(recipe.Ingredients.Slots[i].Item.TypeName,itemLibrary);
                        for(int j = 0; j < parent.Ingredients.Slots.Length; j++)
                        {
                            if(!CanCraftRaw(parent, duplicate,itemLibrary))
                            {
                                return false;
                            }
                            else
                            {
                                InventoryHandler.AddToInventory(CraftFromInventory(parent, duplicate), duplicate);
                                i -= 1;
                            }
                        }
                    }
                    
                }
            }
            return true;
        }

    }
}
