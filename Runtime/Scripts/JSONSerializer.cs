using SimpleJSON;
using System;
using System.IO;

namespace InventoryPackage
{
    public static class JSONSerializer
    {
        public static JSONObject JSONFromLibrary(ItemLibrary library)
        {
            JSONObject json = new JSONObject();
            json.Add("LibraryName", library.LibraryName);

            if (library.AllItemTypes != null)
            {
                json.Add("ItemTypes", JSONArrayFromItemTypes(library.AllItemTypes));
            }

            if (library.AllRecipes != null)
            {
                json.Add("Recipes", JSONArrayFromRecipes(library.AllRecipes));
            }

            return json;
        }

        private static JSONArray JSONArrayFromRecipes(Recipe[] recipes)
        {
            JSONArray jsonArray = new JSONArray();
            foreach (Recipe recipe in recipes)
            {
                jsonArray.Add(JSONFromRecipe(recipe));
            }
            return jsonArray;
        }

        private static JSONArray JSONArrayFromItemTypes(ItemType[] itemTypes)
        {
            JSONArray itemTypesObject = new JSONArray();
            for (int i = 0; i < itemTypes.Length; i++)
            {
                itemTypesObject.Add(JSONFromItemType(itemTypes[i]));
            }

            return itemTypesObject;
        }

        public static JSONObject JSONFromItemType(ItemType itemType)
        {
            JSONObject json = new JSONObject();
            json.Add("name", itemType.TypeName);
            json.Add("stack size", itemType.StackSize);
            json.Add("description", itemType.Description);
            if(itemType.ResourceFolderPath != null && itemType.ResourceFolderPath != "")
            {
                 json.Add("resource path", itemType.ResourceFolderPath);
            }
           

            return json;
        }


        public static JSONObject JSONFromRecipe(Recipe recipe)
        {
            JSONObject json = new JSONObject();
            json.Add("result", recipe.Result.Item.TypeName);

            JSONArray ingredientsArray = new JSONArray();
            for (int i = 0; i < recipe.Ingredients.Slots.Length; i++)
            {
                JSONObject ingredient = new JSONObject();
                ingredient.Add("type name", recipe.Ingredients.Slots[i].Item.TypeName);
                ingredient.Add("quantity", recipe.Ingredients.Slots[i].Amount);

                ingredientsArray.Add(ingredient);
            }
            json.Add("ingredients", ingredientsArray);


            if (recipe.ToolType != null)
            {
                json.Add("tool", recipe.ToolType.TypeName);
            }
            json.Add("craft time", recipe.CraftingTime.ToString());
            json.Add("output amount", recipe.Result.Amount);
            return json;
        }


        public static void SaveLibrary(ItemLibrary library, string path)
        {
            string jsonString = JSONFromLibrary(library).ToString();
            File.WriteAllText(path, jsonString);
        }


    }
}


