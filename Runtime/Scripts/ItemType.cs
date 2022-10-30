using System;
using UnityEngine;

namespace InventoryPackage
{
    [Serializable]
    public class ItemType
    {
        public string TypeName {get => typeName; }
        [SerializeField] private readonly string typeName;

        public string Description {get =>  description; }
        [SerializeField] private readonly string  description;

        public int StackSize {get => stackSize;}
        [SerializeField] private readonly int stackSize;


        public ItemType(string typeName, int stackSize = 100, string discription = "no description written.")
        {
            this.typeName = typeName;
            this.description = discription;
            this.stackSize = stackSize;
        }



        public static ItemType Empty()
        {
            return new ItemType("Empty", 1);
        }
    }
}
