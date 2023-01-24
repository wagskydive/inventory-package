

namespace InventoryPackage
{

    public static class InventoryBuilder
    {
        public static Inventory CreateInventory(int size = 100)
        {
            return new Inventory(size);
        }


    }
}