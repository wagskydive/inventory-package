using System.IO;
using UnityEditor;
using UnityEngine;

namespace InventoryPackage
{
    public static class ResourceLoader
    {
        public static void LoadIcon(ItemType itemType, string path)
        {
            if (Application.isPlaying)
            {
                path = path.Split("/Resources/")[1];
                Texture2D texture = (Texture2D)Resources.Load(path) as Texture2D;
                ItemType.SetIcon(itemType, texture);
            }
            else
            {
                Texture2D texture = AssetDatabase.LoadAssetAtPath(path, typeof(Texture2D)) as Texture2D;
                ItemType.SetIcon(itemType, texture);
            }
        }
    }
}


