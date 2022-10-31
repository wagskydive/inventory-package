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
            for (int i = 0; i <libraryTransforms.Length; i++)
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

    }
}