namespace InventoryPackage
{
    public class ItemLibrary
    {
        public string LibraryName { get; private set; }

        public ItemType[] AllItemTypes {get;private set;}

        public ItemLibrary(string LibraryName,ItemType[] allItemTypes)
        {
            this.LibraryName = LibraryName;
            AllItemTypes = allItemTypes;            
        }

        public Inventory CreativeMenu{get => MakeCreativeMenu();}

        Inventory MakeCreativeMenu()
        {          
            Inventory inventory = InventoryBuilder.CreateInventory(AllItemTypes.Length);
            for (int i = 0; i < AllItemTypes.Length; i++)
            {
                InventoryHandler.AddToInventory(new ItemAmount(AllItemTypes[i], AllItemTypes[i].StackSize), inventory);
            }
            return inventory;
        }

    }
}