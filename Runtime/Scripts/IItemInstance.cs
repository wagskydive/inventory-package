namespace InventoryPackage
{
    public interface IItemInstance
    {
        ItemType TypeOfItem { get; }
        void SetItemType(ItemType itemType);
        void Use();
    }
}
