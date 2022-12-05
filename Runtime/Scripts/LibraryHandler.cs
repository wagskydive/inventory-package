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


        public static string[] LibraryNames(ItemLibrary library)
        {
            List<string> libraryNames = new List<string>();
            foreach (var item in library.AllItemTypes)
            {
                libraryNames.Add(item.TypeName);
            }
            return libraryNames.ToArray();

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

        public static void SetIconsPath(string path, ItemLibrary library)
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
            if(library.AllRecipes != null)
            {
                List<Recipe> recipesList = library.AllRecipes.ToList();
                if(recipesList.Contains(recipe))
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

        public static void AddItemType(ItemLibrary library,  ItemType newItemType)
        {
            library.AddItemType(newItemType);
        }

        public static void RemoveItemType(ItemLibrary library, int index)
        {
            library.RemoveItemType(index);
        }
    }
}
