using SimpleJSON;
using System.Collections.Generic;
using System.IO;

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
            return new ItemLibrary(libraryName, itemTypes);
        }
    }
}


