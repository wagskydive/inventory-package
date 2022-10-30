using System;
using UnityEngine;

namespace InventoryPackage
{
    [Serializable]
    public class ItemLibrary
    {
        [SerializeField]
        private readonly string libraryName;
        public string LibraryName { get=>libraryName; }


        [SerializeField]
        private readonly ItemType[] allItemTypes;

        public ItemType[] AllItemTypes {get => allItemTypes;}

        public ItemLibrary(string libraryName,ItemType[] allItemTypes)
        {
            this.libraryName = libraryName;
            this.allItemTypes = allItemTypes;            
        }

        public Inventory CreativeMenu{get => MakeCreativeMenu();}

        Inventory MakeCreativeMenu()
        {          
            Inventory inventory = InventoryBuilder.CreateInventory(AllItemTypes.Length);
            for (int i = 0; i < AllItemTypes.Length; i++)
            {
                InventoryHandler.AddToInventory(new ItemAmount(AllItemTypes[i], AllItemTypes[i].StackSize), inventory);
            }
            return inventory;
        }

    }
}