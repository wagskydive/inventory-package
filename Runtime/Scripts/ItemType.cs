using System;
using System.IO;
using UnityEngine;

namespace InventoryPackage
{
    public class ItemType : ScriptableObject, IItemType
    {
        public event Action<ItemType> OnResourceFolderSet;
        public event Action<ItemType> OnItemTypeNameSet;


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
            if ((this.icon == null && resourceFolder.Contains("Assets/") && resourceFolder.Contains("Resources")) || this.icon != null && this.icon.name != typeName && resourceFolder.Contains("Assets/") && resourceFolder.Contains("Resources"))
            {
                ResourceLoader.LoadIcon(this, resourceFolder + "/" + typeName + ".png");
            }
            return this.icon;
        }


        public static ItemType CreateNew(string typeName, int stackSize = 100, string discription = "no description written.", string resourceFolder = "")
        {
            ItemType itemType = (ItemType)ScriptableObject.CreateInstance(typeof(ItemType));
            itemType.SetTypeName(typeName);
            itemType.SetDescription(discription);
            itemType.SetStackSize(stackSize);
            itemType.SetResourceFolder(resourceFolder);

            itemType.OnResourceFolderSet += UpdateResources;
            itemType.OnItemTypeNameSet += UpdateResources;

            return itemType;
        }

        private static void UpdateResources(ItemType itemType)
        {
            Texture2D icon = itemType.Icon;
        }

        public static void SetTypeName(ItemType itemType, string typeName)
        {
            itemType.SetTypeName(typeName);
        }

        internal void SetTypeName(string name)
        {
            this.typeName = name;
            OnItemTypeNameSet?.Invoke(this);
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
            OnResourceFolderSet?.Invoke(this);
        }

        public static void SetResourcePath(ItemType itemType, string path)
        {
            itemType.SetResourceFolder(path);
        }


        public static ItemType Empty()
        {
            return ItemType.CreateNew("Empty", 1);
        }


    }
}
