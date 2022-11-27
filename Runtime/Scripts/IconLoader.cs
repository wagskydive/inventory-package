using System.IO;
using UnityEngine;

namespace InventoryPackage
{
    public static class IconLoader
    {
        public static Texture2D LoadIcon(ItemType itemType, string path)
        {
            if(File.Exists(path))
            {
                return Resources.Load(path) as Texture2D;
            }
            else
            {
                return null;
            }
        }
    }
}


