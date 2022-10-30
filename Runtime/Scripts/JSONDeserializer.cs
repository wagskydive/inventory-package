using SimpleJSON;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace InventoryPackage
{
    public static class JSONDeserializer
    {
        public static string ReadJSON(string path)
        {
            return File.ReadAllText(path);
        }

        public static string ReadLibraryName(string path)
        {
            JSONObject library = JSONObject.Parse(ReadJSON(path)).AsObject;
            return library["LibraryName"];
        }

        public static ItemType[] ReadAllItemTypes(string path)
        {
            JSONObject json = JSONObject.Parse(ReadJSON(path)).AsObject;

            List<ItemType> itemTypes = new List<ItemType>();

            foreach(JSONObject obj in json.GetValueOrDefault("ItemTypes", json))
            {
                itemTypes.Add(new ItemType(obj["name"], obj["stack size"]));
            }
            return itemTypes.ToArray();
        }
        
        public static ItemLibrary CreateLibraryFromJSON(string path)
        {
            JSONObject json = JSONObject.Parse(ReadJSON(path)).AsObject;
            string libraryName = json["LibraryName"];
            ItemType[] itemTypes = ReadAllItemTypes(path);
            ItemLibrary library = new ItemLibrary(libraryName, itemTypes);

            Recipe[] allRecipes = ReadAllRecipes(path, library);

            library.ReplaceRecipes(allRecipes);
            return library;
        }

        public static Recipe ReadRecipe(JSONObject recipeJson, ItemLibrary library, string outputType)
        {
            float craftingTime = recipeJson["craft time"];

            ItemType tool = LibraryHandler.GetItemTypeByName(recipeJson["tool"], library);

            JSONArray ingredientsJson = recipeJson.GetValueOrDefault("ingredients", recipeJson).AsArray;

            Inventory ingredients = InventoryBuilder.CreateInventory(ingredientsJson.Count);
            

            for(int i = 0; i < ingredientsJson.Count; i++)         
            {
                ItemType itemType = LibraryHandler.GetItemTypeByName(ingredientsJson[i]["type name"], library);
                if(itemType.TypeName == "Empty")
                {
                    Debug.LogWarning("Ingredient type not found in library");
                }
                int amount = ingredientsJson[i]["quantity"];

                ingredients.AddInEmptySlot(new ItemAmount(itemType, amount),i);
            }
            
            ItemAmount results = new ItemAmount(LibraryHandler.GetItemTypeByName(outputType,library),recipeJson["output amount"]);


            Recipe recipe = new Recipe(ingredients,craftingTime,tool,results);
            return recipe;
        }
        
        public static Recipe[] ReadAllRecipes(string path, ItemLibrary library)
        {
            JSONObject json = JSONObject.Parse(ReadJSON(path)).AsObject;

            List<Recipe> recipes = new List<Recipe>();

            foreach(JSONObject obj in json.GetValueOrDefault("ItemTypes", json))
            {
                JSONObject recipeJson = obj.GetValueOrDefault("recipe",obj).AsObject;
                if(recipeJson != null && obj["recipe"] != null)
                {
                    
                    Recipe recipe = ReadRecipe(recipeJson,library, obj["name"]);
                    recipes.Add(recipe);
                }
            }
            return recipes.ToArray();
        }
        
    }
}


