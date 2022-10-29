namespace InventoryPackage
{
    

    public class Inventory
    {
        public ItemAmount[] Slots{get; private set;}

        internal Inventory(int availableSlots)
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
    }


}
