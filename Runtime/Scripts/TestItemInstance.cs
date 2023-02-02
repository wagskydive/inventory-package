namespace InventoryPackage
{
    public class TestItemInstance : IItemInstance
    {
        ItemType itemType;
        public ItemType TypeOfItem { get => itemType; }


        public TestItemInstance(ItemType itemType)
        {
            SetItemType(itemType);
        }
        public void SetItemType(ItemType itemType)
        {
            this.itemType = itemType;
        }

        public void Use()
        {

        }
    }
}
