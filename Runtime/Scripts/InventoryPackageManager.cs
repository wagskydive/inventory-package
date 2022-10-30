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
    private string[] LibraryNames;

    [SerializeField]
    public ItemLibrary library;

    private void Awake() 
    {
        library = JSONDeserializer.CreateLibraryFromJSON(libraryPath);
        LibraryNames = InventoryHandler.LibraryNames(library);
    }
}
}
