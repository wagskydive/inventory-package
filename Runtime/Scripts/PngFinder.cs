using System;
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
                    string resourceString = "Assets" + path.Split("Assets")[1];
                    List<ItemType> types = new List<ItemType>();
                    foreach (string file in files)
                    {
                        FileInfo fileInfo = new FileInfo(file);
                        string typeName = fileInfo.Name.Split('.')[0];

                        ItemType itemType = ItemType.CreateNew(typeName);

                        itemType.SetResourceFolder(resourceString);
                        types.Add(itemType);

                        Debug.Log(resourceString);
                    }
                    return new ItemLibrary("My New Library", types.ToArray(), resourceString);
                }
            }
            return null;
        }

        public static int CheckForNewPngFiles(ItemLibrary library)
        {
            string[] files = Directory.GetFiles(library.DefaultResourcePath, "*.png", SearchOption.TopDirectoryOnly);
            return files.Length - library.AllItemTypes.Length;

        }

        public static ItemType[] GetNewPngFiles(ItemLibrary library)
        {
            string path = library.DefaultResourcePath;
            List<string> files = Directory.GetFiles(library.DefaultResourcePath, "*.png", SearchOption.TopDirectoryOnly).ToList();


            string[] currentNames = LibraryHandler.ItemTypeNames(library);


            for (int i = 0; i < files.Count; i++)
            {
                FileInfo fileInfo = new FileInfo(files[i]);
                files[i] = fileInfo.Name.Split('.')[0];
            }

            foreach (string typeName in currentNames)
            {

                if (files.Contains(typeName))
                {
                    files.Remove(typeName);
                }
            }

            List<ItemType> newTypes = new List<ItemType>();

            for (int i = 0; i < files.Count; i++)
            {
                ItemType newType = ItemType.CreateNew(files[i]);
                newType.SetResourceFolder(library.DefaultResourcePath);

                newTypes.Add(newType);
            }
            return newTypes.ToArray();
        }

        public static string[] GetItemTypesWithoutPng(ItemLibrary library)
        {
            string path = library.DefaultResourcePath;
            List<string> files = Directory.GetFiles(library.DefaultResourcePath, "*.png", SearchOption.TopDirectoryOnly).ToList();

            List<string> currentNames = LibraryHandler.ItemTypeNames(library).ToList();


            for (int i = 0; i < files.Count; i++)
            {
                FileInfo fileInfo = new FileInfo(files[i]);
                files[i] = fileInfo.Name.Split('.')[0];
            }

            foreach (string typeName in files)
            {

                if (currentNames.Contains(typeName))
                {
                    currentNames.Remove(typeName);
                }
            }
            return currentNames.ToArray();
        }
    }
}


