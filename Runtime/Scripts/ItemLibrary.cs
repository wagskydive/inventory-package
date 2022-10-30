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

        [SerializeField]
        private readonly Recipe[] allRecipes;
        public Recipe[] AllRecipes {get => allRecipes;}


        internal ItemLibrary(string libraryName, ItemType[] allItemTypes, Recipe[] allRecipes = null)
        {
            this.libraryName = libraryName;
            this.allItemTypes = allItemTypes;

            if(allRecipes != null)
            {
                this.allRecipes = allRecipes;
            }
        }
    }
}