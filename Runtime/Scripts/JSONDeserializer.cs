using SimpleJSON;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace InventoryPackage
{
    public static class JSONDeserializer
    {
        public static string ReadJSON(string path)
        {
            string json = "";
            if (File.Exists(path))
            {
                json = File.ReadAllText(path);
            }
            return json;
        }

        public static int ReadJSONConfigDefaultStacksize(string jsonString)
        {
            JSONObject jsonObject = JSONObject.Parse(ReadJSON(jsonString)).AsObject;

            return jsonObject["default stack size"];
        }

        public static string ReadJsonConfigDefaultDescription(string jsonString)
        {
            JSONObject jsonObject = JSONObject.Parse(ReadJSON(jsonString)).AsObject;

            return jsonObject["default description"];
        }

        public static int ReadJsonConfigDefaultCharacterInventorySize(string jsonString)
        {
            JSONObject jsonObject = JSONObject.Parse(ReadJSON(jsonString)).AsObject;
            return jsonObject["default character inventory size"];
        }

        public static string ReadLibraryName(string path)
        {
            JSONObject library = JSONObject.Parse(ReadJSON(path)).AsObject;
            return library["LibraryName"];
        }

        public static string ReadDefaultResourcePath(string path)
        {
            JSONObject library = JSONObject.Parse(ReadJSON(path)).AsObject;
            string defaultResourcePath = library["DefaultResourcePath"];
            if(defaultResourcePath != null)
            {
                return defaultResourcePath;
            }
            return "";
        }

        public static ItemType[] ReadAllItemTypes(string path)
        {
            JSONObject json = JSONObject.Parse(ReadJSON(path)).AsObject;

            List<ItemType> itemTypes = new List<ItemType>();

            foreach (JSONObject obj in json.GetValueOrDefault("ItemTypes", json))
            {
                ItemType itemType = ReadItemType(obj);

                itemTypes.Add(itemType);
            }
            return itemTypes.ToArray();
        }

        private static ItemType ReadItemType(JSONObject obj)
        {
            string typeName = obj.GetValueOrDefault("name", obj);
            int stackSize = obj.GetValueOrDefault("stack size", obj);
            string description = obj.GetValueOrDefault("description", obj);
            string resourcePath = obj.GetValueOrDefault("resource path", obj);

            ItemType itemType = ItemType.CreateNew(typeName, stackSize, description, resourcePath);
            return itemType;
        }

        public static ItemLibrary CreateLibraryFromJSON(string path)
        {
            JSONObject json = JSONObject.Parse(ReadJSON(path)).AsObject;
            string libraryName = json["LibraryName"];
            ItemType[] itemTypes = ReadAllItemTypes(path);
            ItemLibrary library = new ItemLibrary(libraryName, itemTypes);
            //LibraryHandler.SetResourcePath(ReadDefaultResourcePath(json),library);

            Recipe[] allRecipes = ReadAllRecipes(path, library);

            library.ReplaceRecipes(allRecipes);
            return library;
        }

        public static Recipe ReadRecipe(JSONObject recipeJson, ItemLibrary library)
        {
            float craftingTime = recipeJson["craft time"];

            ItemType tool = LibraryHandler.GetItemTypeByName(recipeJson["tool"], library);

            JSONArray ingredientsJson = recipeJson.GetValueOrDefault("ingredients", recipeJson).AsArray;

            Inventory ingredients = InventoryBuilder.CreateInventory(ingredientsJson.Count);


            for (int i = 0; i < ingredientsJson.Count; i++)
            {
                ItemType itemType = LibraryHandler.GetItemTypeByName(ingredientsJson[i]["type name"], library);
                if (itemType.TypeName == "Empty")
                {
                    Debug.LogWarning("Ingredient type not found in library");
                }
                int amount = ingredientsJson[i]["quantity"];

                ingredients.AddInEmptySlot(new ItemAmount(itemType, amount), i);
            }

            ItemAmount results = new ItemAmount(LibraryHandler.GetItemTypeByName(recipeJson["result"], library), recipeJson["output amount"]);


            Recipe recipe = new Recipe(ingredients, craftingTime, tool, results);
            return recipe;
        }

        public static Recipe[] ReadAllRecipes(string path, ItemLibrary library)
        {
            JSONObject json = JSONObject.Parse(ReadJSON(path)).AsObject;

            List<Recipe> recipes = new List<Recipe>();

            JSONArray recipeArray = json.GetValueOrDefault("Recipes", json).AsArray;
            if (recipeArray != null)
            {


                foreach (JSONObject recipeJson in recipeArray)
                {

                    if (recipeJson != null)
                    {

                        Recipe recipe = ReadRecipe(recipeJson, library);
                        recipes.Add(recipe);
                    }
                }
            }
            return recipes.ToArray();
        }

    }
}


