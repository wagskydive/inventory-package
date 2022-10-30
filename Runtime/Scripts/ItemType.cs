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

        public int FactoryTier {get => FactoryTier;}
        [SerializeField] private readonly int factoryTier;


        public ItemType(string typeName, int stackSize, int factoryTier = 0)
        {
            this.typeName = typeName;
            this.stackSize = stackSize;
            this.factoryTier = factoryTier;
        }

        public static ItemType Empty()
        {
            return new ItemType("Empty", 1);
        }
    }
}
