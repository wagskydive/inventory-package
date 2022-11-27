using System;
using System.IO;
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
        public Texture2D Icon { get => GetIcon(); }
        private Texture2D icon;
        [SerializeField] private string resourceFolder;
        public string ResourceFolderPath { get => resourceFolder; }

        private Texture2D GetIcon()
        {
            if (icon == null)
            {
                icon = Resources.Load(resourceFolder+"/"+typeName+".png") as Texture2D;
            }
            return icon;
        }


        public static ItemType CreateNew(string typeName, int stackSize = 100, string discription = "no description written.", string resourceFolder = "")
        {
            ItemType itemType = (ItemType)ScriptableObject.CreateInstance(typeof(ItemType));
            itemType.SetTypeName(typeName);
            itemType.SetDescription(discription);
            itemType.SetStackSize(stackSize);
            itemType.SetResourceFolder(resourceFolder);

            return itemType;
        }



        public static void SetTypeName(ItemType itemType, string typeName)
        {
            itemType.SetTypeName(typeName);
        }

        internal void SetTypeName(string name)
        {
            this.typeName = name;
        }



        public static void SetDescription(ItemType itemType, string description)
        {
            itemType.SetDescription(description);
        }

        internal void SetDescription(string description)
        {
            this.description = description;
        }

        public static void SetStackSize(ItemType itemType, int stackSize)
        {
            itemType.SetStackSize(stackSize);
        }

        internal void SetStackSize(int stackSize)
        {
            this.stackSize = stackSize;
        }


        public static void SetIcon(ItemType itemType, Texture2D icon)
        {
            itemType.SetIcon(icon);
        }

        private void SetIcon(Texture2D icon)
        {
            this.icon = icon;
        }

        private void SetResourceFolder(string path)
        {
            resourceFolder = path;         
        }

        public static void SetIconPath(ItemType itemType,string path)
        {
            itemType.SetResourceFolder(path);
        }


        public static ItemType Empty()
        {
            return ItemType.CreateNew("Empty", 1);
        }


    }
}
