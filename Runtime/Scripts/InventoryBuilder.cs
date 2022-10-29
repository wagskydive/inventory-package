

namespace InventoryPackage
{

    public static class InventoryBuilder
    {
        public static Inventory CreateInventory(int size)
        {
            Inventory inventory = new Inventory(size);

            return new Inventory(size);
        }
    }
}