namespace InventoryAndCharacterLogic
{
    

    public class Inventory
    {
        public ItemAmount[] Slots{get; private set;}

        internal Inventory(int availableSlots)
        {
            Slots = new ItemAmount[availableSlots];
        }

        internal void AddInSlot(ItemAmount itemAmount, int slotIndex) 
        {
            if(Slots[slotIndex] == null)
            {
                Slots[slotIndex] = itemAmount;
            }

        }

        internal void RemoveFromSlot(int i)
        {
            Slots[i] = null;
        }
    }


}
