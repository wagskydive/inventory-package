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
    }
}