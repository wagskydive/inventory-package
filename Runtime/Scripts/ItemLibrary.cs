using System;
using UnityEngine;

namespace InventoryPackage
{
    public class ItemLibrary
    {
        [SerializeField]
        private readonly string libraryName;
        public string LibraryName { get => libraryName; }

        [SerializeField]
        private ItemType[] allItemTypes;
        public ItemType[] AllItemTypes { get => allItemTypes; }

        [SerializeField]
        private Recipe[] allRecipes;
        public Recipe[] AllRecipes { get => allRecipes; }


        [SerializeField]
        private string defaultResourcePath = "";
        public string DefaultResourcePath { get => defaultResourcePath; }


        internal ItemLibrary(string libraryName, ItemType[] allItemTypes)
        {
            this.libraryName = libraryName;
            this.allItemTypes = allItemTypes;
        }

        internal void ReplaceRecipes(Recipe[] recipes)
        {
            this.allRecipes = recipes;
        }

        internal void SetDefaultResourcePath(string iconsPath)
        {
            this.defaultResourcePath = iconsPath;
        }

        internal void AddItemType(ItemType itemType)
        {
            Array.Resize(ref allItemTypes, allItemTypes.Length + 1);
            allItemTypes[allItemTypes.Length - 1] = itemType;
        }
    }
}