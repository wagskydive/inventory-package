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

        internal void RemoveItemType(int index)
        {
            RemoveFromRecipes(allItemTypes[index]);

            List<ItemType> itemTypes = allItemTypes.ToList();
            itemTypes.RemoveAt(index);
            allItemTypes = itemTypes.ToArray();
        }

        private void RemoveFromRecipes(ItemType itemType)
        {
            for (int i = 0; i < allRecipes.Length; i++)
            {
                if(allRecipes[i].Result.Item == itemType)
                {
                    RemoveRecipe(allRecipes[i]);
                }
                for(int j = 0; j < allRecipes[i].Ingredients.Slots.Length; j++)
                {
                    if(allRecipes[i].Ingredients.Slots[j].Item == itemType)
                    {
                        allRecipes[i].RemoveIngredient(itemType);
                    }
                }
            }
        }

        private void RemoveRecipe(Recipe recipe)
        {
            List<Recipe> recipesList = allRecipes.ToList();
            if(recipesList.Contains(recipe))
            {
                recipesList.Remove(recipe);
            }
            ReplaceRecipes(recipesList.ToArray());         
        }
    }
}