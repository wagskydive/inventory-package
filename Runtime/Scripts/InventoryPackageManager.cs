using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InventoryPackage;

namespace InventoryPackage
{
    public class InventoryPackageManager : MonoBehaviour
    {
        [SerializeField]
        private string libraryPath;

        [SerializeField]
        private string recipesPath;

        [SerializeField]
        private string[] libraryNames;

        public string[] LibraryNames { get => libraryNames; }

        [SerializeField]
        public ItemLibrary library;

        public void CreateLibrary()
        {
            library = JSONDeserializer.CreateLibraryFromJSON(libraryPath);
            libraryNames = LibraryHandler.LibraryNames(library);
        }
    }
}
