using System;
using System.Collections.Generic;
using System.Linq;

namespace InventoryPackage
{ 
    public class Inventory
    {
        public ItemAmount[] Slots{get; private set;}

        internal Inventory(int availableSlots = 100)
        {
            Slots = new ItemAmount[availableSlots];
            Validate();
        }

        private void Validate()
        {
            for (int i = 0; i < Slots.Length; i++)
            {
                if (Slots[i] == null)
                {
                    Slots[i] = new ItemAmount(ItemType.Empty(),0);
                }
            }
        }



        internal void AddInEmptySlot(ItemAmount itemAmount, int slotIndex) 
        {
            if(Slots[slotIndex].Item.TypeName == ItemType.Empty().TypeName)
            {
                Slots[slotIndex] = itemAmount;
            }
        }

        internal void RemoveFromSlot(int i)
        {
            Slots[i] = new ItemAmount(ItemType.Empty(),0);
        }

        public string[] GetContentInfo()
        {
            string[] info = new string[Slots.Length];
            for (int i = 0; i < info.Length; i++)
            {
                string itemAmountInfo = Slots[i].Item.TypeName+ " "+Slots[i].Amount;
                info[i] = itemAmountInfo;
            }
            return info;

        }

        internal void RemoveSlot(int i)
        {
            List<ItemAmount> slots = Slots.ToList();
            slots.RemoveAt(i);
            Slots = slots.ToArray();
        }
    }


}
