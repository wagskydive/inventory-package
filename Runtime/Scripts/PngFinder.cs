using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace InventoryPackage
{
    public static class PngFinder
    {
        public static ItemLibrary CreateItemLibraryFromPngFiles(string path)
        {
            if (Directory.Exists(path))
            {

                string[] files = Directory.GetFiles(path, "*.png", SearchOption.TopDirectoryOnly);

                if (files.Any())
                {
                    List<ItemType> types = new List<ItemType>();
                    foreach (string file in files)
                    {
                        FileInfo fileInfo = new FileInfo(file);
                        string typeName = fileInfo.Name.Split('.')[0];

                        ItemType itemType = ItemType.CreateNew(typeName);

                        string resourceString ="Assets"+ path.Split("Assets")[1];
                        itemType.SetResourceFolder(resourceString);
                        types.Add(itemType);

                        Debug.Log(resourceString);



                    }
                    return new ItemLibrary("My New Library", types.ToArray());
                }

            }
            return null;
        }
    }
}


