using System;
using UnityEngine;

namespace InventoryPackage
{
    public class ItemType : ScriptableObject, IItemType
    {
        
        
        public string TypeName { get => typeName; }
        [SerializeField] private string typeName;

        public string Description { get => description; }
        [SerializeField] private string description;

        public int StackSize { get => stackSize; }
        [SerializeField] private int stackSize;

        public Texture2D Icon { get => icon; }

        private Texture2D icon;


        public static ItemType CreateNew(string typeName, int stackSize = 100, string discription = "no description written.")
        {
            ItemType itemType = (ItemType)ScriptableObject.CreateInstance(typeof(ItemType));
            itemType.SetTypeName(typeName);
            itemType.SetDescription(discription);
            itemType.SetStackSize(stackSize);

            return itemType;
        }

        internal void SetTypeName(string name)
        {
            this.typeName = name;
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
            return ItemType.CreateNew("Empty", 1);
        }


    }
}
