using NUnit.Framework;
using InventoryPackage;
using System;
using System.Diagnostics;
using UnityEngine;

namespace UnitTesting
{
    internal class LibraryTests
    {
        string testFolderPath = "Assets/inventory-package/UnitTests/TestResources";

        public static string GetCurrentMethodName()
        {
            StackTrace stackTrace = new StackTrace();
            StackFrame stackFrame = stackTrace.GetFrame(1);

            return stackFrame.GetMethod().Name;
        }

        [Test]
        public void TestIfLibraryHandlerCreatesLibrary()
        {
            ItemLibrary library = LibraryHandler.LoadLibrary(testFolderPath + "/TestItemLibrary.json");
            UnityEngine.Debug.Log(TestDataFormatter.ToSentence(GetCurrentMethodName()) + ": expected: " + "apple" + " actual: " + library.AllItemTypes[0].TypeName);
            Assert.AreEqual("apple", library.AllItemTypes[0].TypeName);
        }

        [Test]
        public void TestIfPrefabItemLibraryCanBeFound()
        {
            GameObject prefab = Resources.Load<GameObject>("ItemLibrary/ExampleLibrary") as GameObject;

            Transform[] libraryTransforms = prefab.transform.GetComponentsInChildren<Transform>();
            Transform libraryTransform = null;

            bool found = false;
            for (int i = 0; i < libraryTransforms.Length; i++)
            {
                string keyword = libraryTransforms[i].gameObject.name;
                if (keyword.Contains(InventoryPackage.GlobalSettings.LibraryKeyword))
                {
                    libraryTransform = libraryTransforms[i];
                    found = true;
                    break;
                }
            }

            UnityEngine.Debug.Log(TestDataFormatter.ToSentence(GetCurrentMethodName()) + ": expected: " + InventoryPackage.GlobalSettings.LibraryKeyword + " actual: " + libraryTransform.name);

            Assert.True(found);
        }

        [Test]
        public void TestIfPrefabToLibraryCreatesLibrary()
        {
            string keywordString = "Bottle cap";
            ItemLibrary library = PrefabToLibrary.LibraryFromPrefab(Resources.Load<GameObject>("ItemLibrary/ExampleLibrary") as GameObject);

            UnityEngine.Debug.Log(TestDataFormatter.ToSentence(GetCurrentMethodName()) + ": expected: " + keywordString + " actual: " + LibraryHandler.GetItemTypeByName(keywordString, library).TypeName);

            Assert.IsNotNull(LibraryHandler.GetItemTypeByName(keywordString, library));
        }

        [Test]
        public void TestIfItemTypeCanBeRemovedFromLibrary()
        {
            int indexToRemove = 0;

            ItemLibrary library = LibraryHandler.LoadLibrary(testFolderPath + "/TestItemLibrary.json");

            ItemType type = library.AllItemTypes[indexToRemove];

            LibraryHandler.RemoveItemType(library,indexToRemove);


            // assert that itemtype doesnt still exists
            for (int i = 0; i <library.AllItemTypes.Length; i++)
            {
                Assert.That(library.AllItemTypes[i] != type, "item type still exists in library");
            }

            // assert that no recipe exists that has the itemtype as result or as ingredient
            for (int i = 0; i < library.AllRecipes.Length; i++)
            {
                // no recipe has item type as result
                Assert.That(library.AllRecipes[i].Result.Item != type, "recipe has item type as result");
                //Assert.IsFalse(library.AllRecipes[i].Result.Item == type);

                // no recipe has item type as ingredient
                Inventory ingredients = library.AllRecipes[i].Ingredients;

                for (int j = 0; j < ingredients.Slots.Length; j++)
                {
                    ItemAmount slot = ingredients.Slots[j];
                    Assert.That(slot.Item != type, "recipe has item type as ingredient");
                }
            }
        }
    }
}