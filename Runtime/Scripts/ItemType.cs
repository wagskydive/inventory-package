using System;
using UnityEngine;

namespace InventoryPackage
{
    public class ItemType : IItemType
    {
        public string TypeName { get => typeName; }
        [SerializeField] private readonly string typeName;

        public string Description { get => description; }
        [SerializeField] private string description;

        public int StackSize { get => stackSize; }
        [SerializeField] private int stackSize;

        public Texture2D Icon { get => icon; }

        private Texture2D icon;


        public ItemType(string typeName, int stackSize = 100, string discription = "no description written.")
        {
            this.typeName = typeName;
            this.description = discription;
            this.stackSize = stackSize;
        }

        internal void SetDescription(string description)
        {
            this.description = description;
        }

        internal void SetStackSize(int stackSize)
        {
            this.stackSize = stackSize;
        }

        internal void SetIcon(Texture2D icon)
        {
            this.icon = icon;
        }




        public static ItemType Empty()
        {
            return new ItemType("Empty", 1);
        }


    }
}
