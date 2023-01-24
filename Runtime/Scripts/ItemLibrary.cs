using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace InventoryPackage
{
    public class ItemLibrary
    {
        [SerializeField]
        private readonly string libraryName;
        public string LibraryName { get => libraryName; }

        [SerializeField]
        internal ItemType[] allItemTypes;
        public ItemType[] AllItemTypes { get => allItemTypes; }

        [SerializeField]
        internal Recipe[] allRecipes;
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

        internal void SetDefaultResourcePath(string resourcePath)
        {
            this.defaultResourcePath = resourcePath;
        }


    }
}