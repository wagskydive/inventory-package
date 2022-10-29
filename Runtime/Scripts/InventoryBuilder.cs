

namespace InventoryAndCharacterLogic
{
    public static class InventoryBuilder
    {
        public static Inventory CreateInventory(int size)
        {
            return new Inventory(size);
        }
    }
}