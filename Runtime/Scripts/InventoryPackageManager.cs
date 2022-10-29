using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InventoryPackage;

public class InventoryPackageManager : MonoBehaviour
{
    [SerializeField]
    private string LibraryPath;

    [SerializeField]
    private string RecipesPath;

    public ItemLibrary library{get; private set;}

    private void Awake() 
    {
        library = JSONDeserializer.CreateLibraryFromJSON(LibraryPath);
    }

    
}
