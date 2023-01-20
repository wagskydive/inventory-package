namespace InventoryPackage
{

    public class ItemAmount
    {
        public ItemType Item { get; private set; }
        public int Amount { get; private set; }
        public ItemAmount(ItemType type, int amount)
        {
            Item = type;
            Amount = amount;
        }

        public void RemoveAmount(int amount, out ItemAmount AmountLeft)
        {
            if (amount > Amount)
            {
                AmountLeft = new ItemAmount(Item, amount - Amount);
            }
            else
            {
                AmountLeft = null;
            }

            Amount -= amount;

            if (Amount <= 0)
            {
                Amount = 0;
            }
        }

        public void AddAmount(int amount, out ItemAmount amountLeft)
        {
            amountLeft = Empty();
            int newAmount = Amount + amount;
            if (newAmount > Item.StackSize)
            {
                amountLeft = new ItemAmount(Item, amount - Amount);
                newAmount = Item.StackSize;
            }
            Amount = newAmount;
        }

        public static ItemAmount Empty()
        {
            return new ItemAmount(ItemType.Empty(), 0);
        }

        public void SetAmount(int amount)
        {
            Amount = amount;
        }
    }
}






