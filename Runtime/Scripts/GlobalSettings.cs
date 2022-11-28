using System;
using UnityEngine;

namespace InventoryPackage
{
    public static class GlobalSettings
    {
        public static string LibraryKeyword { get => GlobalSettings.GetLibraryKeyWord(); }
        public static string DefaultDescription { get => GetDefaultDescription(); }
        public static int DefaultStackSize { get => GetDefaultStackSize(); }

        public static int DefaultCharacterInventorySize { get => GetDefaultCharacterInventorySize(); }

        private static int GetDefaultCharacterInventorySize()
        {
            TextAsset asset = Resources.Load("inventory-package-configuration.json") as TextAsset;
            return JSONDeserializer.ReadJsonConfigDefaultCharacterInventorySize(JSONDeserializer.ReadJSON(asset.text));
        }

        private static string GetLibraryKeyWord()
        {
            TextAsset asset = Resources.Load("inventory-package-configuration.json") as TextAsset;

            return "ItemLibrary";

        }


        private static string GetDefaultDescription()
        {
            return "No description written.";
        }



        private static int GetDefaultStackSize()
        {
            TextAsset asset = Resources.Load("inventory-package-configuration.json") as TextAsset;
            return JSONDeserializer.ReadJSONConfigDefaultStacksize(JSONDeserializer.ReadJSON(asset.text));
        }
    }
}