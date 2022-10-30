using System;
using UnityEngine;

namespace InventoryPackage
{
    public class ItemLibrary
    {
        [SerializeField]
        private readonly string libraryName;
        public string LibraryName { get=>libraryName; }

        [SerializeField]
        private readonly ItemType[] allItemTypes;
        public ItemType[] AllItemTypes {get => allItemTypes;}

        internal ItemLibrary(string libraryName,ItemType[] allItemTypes)
        {
            this.libraryName = libraryName;
            this.allItemTypes = allItemTypes;            
        }
    }
    public static class LibraryHandler
    {
        //public static Inventory CreativeMenu{get => MakeCreativeMenu();}

        public static Inventory MakeCreativeMenu(ItemLibrary library)
        {          
            Inventory inventory = InventoryBuilder.CreateInventory(library.AllItemTypes.Length);
            for (int i = 0; i < library.AllItemTypes.Length; i++)
            {
                InventoryHandler.AddToInventory(new ItemAmount(library.AllItemTypes[i], library.AllItemTypes[i].StackSize), inventory);
            }
            return inventory;
        }

    }
}