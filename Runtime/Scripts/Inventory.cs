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

        internal void RemoveSlot(int i)
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

        internal void RemoveAmountFromSlot(int amount, int slot)
        {
            Slots[slot] = new ItemAmount(Slots[slot].Item, Slots[slot].Amount - amount);
        }

       
    }


}
