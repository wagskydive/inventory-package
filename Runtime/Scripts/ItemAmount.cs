namespace InventoryPackage
{
    public class ItemAmount
    {
        public ItemAmount(ItemType type, int amount)
        {
            Item = type;
            Amount = amount;
        }
        public ItemType Item {get; private set;}
        public int Amount {get; private set;}

        public void RemoveAmount(int amount, out ItemAmount AmountLeft)
        {            
            if(amount > Amount)
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

        public void AddAmount(int amount, out ItemAmount AmountLeft)
        {
            AmountLeft = new ItemAmount(ItemType.Empty(),0);
            int newAmount = Amount + amount;
            if(newAmount > Item.StackSize)
            {
                
                AmountLeft = new ItemAmount(Item, amount-Amount);

                newAmount = Item.StackSize;
            }
            Amount = newAmount;
            
        }
    }


}
