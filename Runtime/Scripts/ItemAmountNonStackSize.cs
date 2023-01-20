namespace InventoryPackage
{
    public class ItemAmountNonStackSize
    {
        public ItemType Item { get; private set; }
        public int Amount { get; private set; }

        public ItemAmountNonStackSize(ItemType itemType, int amount)
        {
            Item = itemType;
            Amount = amount;
        }

        public void AddAmount(int amount)
        {
            Amount += amount;
        }
    }
}






