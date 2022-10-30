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
        private Recipe[] allRecipes;
        public Recipe[] AllRecipes {get => allRecipes;}


        internal ItemLibrary(string libraryName, ItemType[] allItemTypes)
        {
            this.libraryName = libraryName;
            this.allItemTypes = allItemTypes;
        }
        
        internal void ReplaceRecipes(Recipe[] recipes)
        {
            this.allRecipes = recipes;
        }

        
    }
}