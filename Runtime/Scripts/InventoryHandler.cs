using System;
using System.Linq;

namespace InventoryAndCharacterLogic
{
    public static class InventoryHandler
    {
        public static void AddToInventory(ItemAmount itemAmount, Inventory inventory)
        {
            inventory.AddInSlot(itemAmount,0);
        }

        public static void RemoveSlotFromInventory(ItemAmount itemAmount, Inventory inventory)
        {  
            if(inventory.Slots.Contains(itemAmount))
            {
                for(int i = 0; i < inventory.Slots.Length; i++)
                {
                    if(inventory.Slots[i] == itemAmount)
                    {
                        inventory.RemoveFromSlot(i);
                        
                    }
                }
            }
        }
    }
}