using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace InventoryPackage
{
    public static class LibraryHandler
    {


        public static Inventory MakeCreativeMenu(ItemLibrary library)
        {
            Inventory inventory = InventoryBuilder.CreateInventory(library.AllItemTypes.Length);
            for (int i = 0; i < library.AllItemTypes.Length; i++)
            {
                InventoryHandler.AddToInventory(new ItemAmount(library.AllItemTypes[i], library.AllItemTypes[i].StackSize), inventory);
            }
            return inventory;
        }

        public static ItemLibrary LoadLibrary(string path)
        {
            return JSONDeserializer.CreateLibraryFromJSON(path);
        }

        public static ItemType GetItemTypeByName(string name, ItemLibrary library)
        {
            for (int i = 0; i < library.AllItemTypes.Length; i++)
            {
                if (library.AllItemTypes[i].TypeName == name)
                {
                    return library.AllItemTypes[i];
                }
            }
            return ItemType.Empty();
        }


        public static string[] ItemTypeNames(ItemLibrary library)
        {
            List<string> itemTypeNames = new List<string>();
            foreach (var item in library.AllItemTypes)
            {
                itemTypeNames.Add(item.TypeName);
            }
            return itemTypeNames.ToArray();

        }

        public static string[] RecipesResultTypes(ItemLibrary library)
        {
            List<string> recipes = new List<string>();
            foreach (var recipe in library.AllRecipes)
            {
                recipes.Add(recipe.ResultType);
            }

            return recipes.ToArray();
        }

        public static ItemAmount[] RecipeResults(ItemLibrary library)
        {
            List<ItemAmount> recipes = new List<ItemAmount>();
            foreach (var recipe in library.AllRecipes)
            {
                recipes.Add(recipe.Result);
            }

            return recipes.ToArray();
        }

        public static ItemType[] RecipeResultTypes(ItemLibrary library)
        {
            List<ItemType> recipes = new List<ItemType>();
            foreach (var recipe in library.AllRecipes)
            {
                recipes.Add(recipe.Result.Item);
            }

            return recipes.ToArray();
        }


        public static void SetIcons(ItemType[] itemTypes, Texture2D[] icons)
        {
            if (itemTypes.Length != icons.Length)
            {
                for (int i = 0; i < itemTypes.Length; i++)
                {
                    ItemType.SetIcon(itemTypes[i], icons[i]);
                }
            }

        }

        public static void SetResourcePath(string path, ItemLibrary library)
        {
            library.SetDefaultResourcePath(path);
        }

        public static ItemType[] FilteredTypes(ItemLibrary library, ItemType[] excludedTypes)
        {
            List<ItemType> filteredTypes = new List<ItemType>();
            foreach (var item in library.AllItemTypes)
            {
                if (!excludedTypes.Contains(item)) filteredTypes.Add(item);
            }
            return filteredTypes.ToArray();
        }

        public static void AddRecipeToLibrary(ItemLibrary library, Recipe recipe)
        {
            Recipe[] recipes;
            if (library.AllRecipes != null)
            {
                recipes = new Recipe[library.AllRecipes.Length + 1];
                for (int i = 0; i < library.AllRecipes.Length; i++)
                {
                    recipes[i] = library.AllRecipes[i];
                }

                recipes[library.AllRecipes.Length] = recipe;
            }
            else
            {
                recipes = new Recipe[1];
                recipes[0] = recipe;
            }
            library.ReplaceRecipes(recipes);
        }

        public static void RemoveRecipe(ItemLibrary library, Recipe recipe)
        {
            if (library.AllRecipes != null)
            {
                List<Recipe> recipesList = library.AllRecipes.ToList();
                if (recipesList.Contains(recipe))
                {
                    recipesList.Remove(recipe);
                    library.ReplaceRecipes(recipesList.ToArray());
                }
            }
        }

        public static ItemLibrary CreateNewLibrary(string libraryName, ItemType[] itemTypes)
        {
            return new ItemLibrary(libraryName, itemTypes);
        }

        public static void RemoveItemType(ItemType itemType, ItemLibrary library)
        {
            for (int i = 0; i < library.allItemTypes.Length; i++)
            {
                if (library.AllItemTypes[i] == itemType)
                {
                    RemoveItemType(library, i);
                }

            }
        }
        public static void RemoveItemType(ItemLibrary library, int index)
        {
            ItemType itemType = library.AllItemTypes[index];
            for (int i = 0; i < library.AllRecipes.Length; i++)
            {

                for (int j = 0; j < library.AllRecipes[i].Ingredients.Slots.Length; j++)
                {
                    ItemAmount slot = library.AllRecipes[i].Ingredients.Slots[j];

                    if (slot.Item == itemType)
                    {
                        RecipeCreator.RemoveIngredient(library.AllRecipes[i], j);
                    }
                }
            }
            if (!LibraryHandler.IsRawIngredient(itemType, library))
            {
                Recipe recipe = LibraryHandler.GetRecipe(itemType, library);
                RemoveRecipe(library, recipe);
            }
            List<ItemType> itemTypes = library.AllItemTypes.ToList();
            itemTypes.RemoveAt(index);
            library.allItemTypes = itemTypes.ToArray();
        }



        public static void AddItemType(ItemType itemType, ItemLibrary library)
        {
            Array.Resize(ref library.allItemTypes, library.allItemTypes.Length + 1);
            library.allItemTypes[library.allItemTypes.Length - 1] = itemType;
        }



        internal static void RemoveRecipe(Recipe recipe, ItemLibrary library)
        {
            List<Recipe> recipesList = library.allRecipes.ToList();
            if (recipesList.Contains(recipe))
            {
                recipesList.Remove(recipe);
            }
            library.ReplaceRecipes(recipesList.ToArray());
        }

        public static Inventory GetRawIngredientsOfRecipe(Recipe recipe, ItemLibrary library)
        {
            ItemAmount[] recipeResults = RecipeResults(library);
            string[] recipeResultNames = RecipesResultTypes(library);
            Inventory finalInventory = InventoryBuilder.CreateInventory(999);

            for (int i = 0; i < recipe.Ingredients.Slots.Length; i++)
            {
                ItemAmount ingredient = new ItemAmount(recipe.Ingredients.Slots[i].Item, recipe.Ingredients.Slots[i].Amount);
                if (recipeResultNames.Contains(ingredient.Item.TypeName))
                {
                    for (int j = 0; j < recipeResults.Length; j++)
                    {
                        if (recipeResults[j].Item == ingredient.Item)
                        {
                            int recipeRuns = (int)Mathf.Ceil(ingredient.Amount / recipeResults[j].Amount);


                            for (int k = 0; k < recipeRuns; k++)
                            {
                                InventoryHandler.AddToInventory(GetRawIngredientsOfRecipe(library.AllRecipes[j], library), finalInventory);
                            }

                            break;
                        }
                    }
                }
                else
                {
                    InventoryHandler.AddToInventory(ingredient, finalInventory);
                }
            }

            return finalInventory;
        }

        public static bool IsRawIngredient(ItemType itemType, ItemLibrary itemLibrary)
        {
            for (int i = 0; i < itemLibrary.AllRecipes.Length; i++)
            {
                if (itemLibrary.AllRecipes[i].Result.Item == itemType)
                {
                    return false;
                }
            }
            return true;
        }

        public static Recipe GetRecipe(ItemType itemType, ItemLibrary itemLibrary)
        {
            for (int i = 0; i < itemLibrary.AllRecipes.Length; i++)
            {
                if (itemLibrary.AllRecipes[i].Result.Item == itemType)
                {
                    return itemLibrary.AllRecipes[i];
                }
            }
            return null;
        }
    }
}
