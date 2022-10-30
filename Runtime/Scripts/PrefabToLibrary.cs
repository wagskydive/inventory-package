using System.Collections.Generic;
using UnityEngine;

namespace InventoryPackage
{
    public static class PrefabToLibrary
    {
        public static ItemLibrary LibraryFromPrefab(GameObject prefab)
        {
            string keywordString = "ItemLibrary";

            Transform[] libraryTransforms = prefab.transform.GetComponentsInChildren<Transform>();
            Transform libraryTransform = null;
            for (int i = 0; i < libraryTransforms.Length; i++)
            {
                string keyword = libraryTransforms[i].gameObject.name.TrimEnd(' ');
                if (keyword.Contains(keywordString))
                {
                    libraryTransform = libraryTransforms[i];
                }

            }
            if(libraryTransform)
            {
                
                Transform[] itemTransforms = libraryTransform.GetComponentsInChildren<Transform>();

                
                List<ItemType> itemTypes = new List<ItemType>();

                for (int i = 0; i < itemTransforms.Length; i++)
                {
                    itemTypes.Add(new ItemType(itemTransforms[i].name.TrimEnd(' ')));
                }
                return new ItemLibrary(prefab.name, itemTypes.ToArray());
            }
            return null;

            
        }

    }
}