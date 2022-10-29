namespace InventoryAndCharacterLogic
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
    }


}
