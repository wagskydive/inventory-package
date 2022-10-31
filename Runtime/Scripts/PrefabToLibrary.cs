using System.Collections.Generic;
using UnityEngine;

namespace InventoryPackage
{
    public static class PrefabToLibrary
    {
        public static bool ValidatePrefab(GameObject prefab)
        {

            Transform[] libraryTransforms = prefab.transform.GetComponentsInChildren<Transform>();
            Transform libraryTransform = null;
            for (int i = 0; i <libraryTransforms.Length; i++)
            {
                string keyword = libraryTransforms[i].gameObject.name;
                if (keyword.Contains(GlobalSettings.LibraryKeyword))
                {
                    libraryTransform = libraryTransforms[i];
                }
            }
            return libraryTransform != null;
        }
        public static ItemLibrary LibraryFromPrefab(GameObject prefab)
        {

            Transform[] libraryTransforms = prefab.transform.GetComponentsInChildren<Transform>();
            Transform libraryTransform = null;
            for (int i = 0; i < libraryTransforms.Length; i++)
            {
                string keyword = libraryTransforms[i].gameObject.name.TrimEnd(' ');
                if (keyword.Contains(GlobalSettings.LibraryKeyword))
                {
                    libraryTransform = libraryTransforms[i];
                }

            }
            if (libraryTransform)
            {
                Transform[] itemTransforms = libraryTransform.GetComponentsInChildren<Transform>();

                List<ItemType> itemTypes = new List<ItemType>();

                for (int i = 0; i < itemTransforms.Length; i++)
                {
                    if (itemTransforms[i].name != prefab.name && !itemTransforms[i].name.Contains(GlobalSettings.LibraryKeyword))
                    {


                        ItemType itemType = ItemType.CreateNew(itemTransforms[i].name.TrimEnd(' '));

                        if (itemTransforms[i].GetComponent<SpriteRenderer>() != null)
                        {
                            SpriteRenderer spriteRenderer = itemTransforms[i].GetComponent<SpriteRenderer>();
                            itemType.SetIcon(spriteRenderer.sprite.texture);
                            
                        }
                        else
                        {
                            itemType.SetIcon(Resources.Load<Texture2D>("fallbackIcon.png") as Texture2D);
                        }
                        itemTypes.Add(itemType);
                    }
                }
                return new ItemLibrary(prefab.name, itemTypes.ToArray());
            }
            return null;
        }



    }
    public static class TextureHelper
    {
        public static Texture2D GenerateTextureFromSprite(Sprite aSprite)
        {
            var rect = aSprite.rect;
            var tex = new Texture2D((int)rect.width, (int)rect.height);
            var data = aSprite.texture.GetPixels((int)rect.x, (int)rect.y, (int)rect.width, (int)rect.height);
            tex.SetPixels(data);
            tex.Apply(true);
            return tex;
        }
    }
}