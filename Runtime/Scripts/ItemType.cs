using System;

namespace InventoryAndCharacterLogic
{

    public class ItemType
    {
        public string TypeName {get; private set;}
        public int StackSize {get; private set;}
        public ItemType(string typeName, int stackSize)
        {
            TypeName = typeName;
            StackSize = stackSize;
        }

        public static ItemType Empty()
        {
            return new ItemType("Empty", 1);
        }
    }
}
