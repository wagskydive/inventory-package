using System;
using UnityEngine;

namespace InventoryPackage
{
    [Serializable]
    public class ItemType
    {
        public string TypeName {get => typeName; }
        [SerializeField] private readonly string typeName;

        public int StackSize {get => stackSize;}
        [SerializeField] private readonly int stackSize;
        
        public ItemType(string typeName, int stackSize)
        {
            this.typeName = typeName;
            this.stackSize = stackSize;
        }

        public static ItemType Empty()
        {
            return new ItemType("Empty", 1);
        }
    }
}
