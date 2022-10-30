using System;
using System.Diagnostics;
using NUnit.Framework;
using InventoryPackage;
using UnityEngine;


namespace UnitTesting
{



    internal class JSONTests
    {

        public static string GetCurrentMethodName()
        {
            StackTrace stackTrace = new StackTrace();
            StackFrame stackFrame = stackTrace.GetFrame(1);

            return stackFrame.GetMethod().Name;
        }



        string testFolderPath = "Assets/inventory-package/UnitTests/TestResources";
        [Test]
        public void TestReadFromResources()
        {
            string json = JSONDeserializer.ReadJSON(testFolderPath + "/TestFileOneString.txt");
            Console.WriteLine(json);
            Assert.AreEqual("One String", json);
        }

        [Test]
        public void TestJSONReadLibraryName()
        {
            string actualName = "Test Library";

            string testName = JSONDeserializer.ReadLibraryName(testFolderPath + "/TestItemLibrary.json");
            Console.WriteLine(testName);
            Assert.AreEqual(actualName, testName);
        }

        [Test]
        public void TestJSONReadFirstItemTypeName()
        {
            string actualName = "apple";
            ItemLibrary itemLibrary = JSONDeserializer.CreateLibraryFromJSON(testFolderPath + "/TestItemLibrary.json");


            string testName = itemLibrary.AllItemTypes[0].TypeName;
            Console.WriteLine(testName);
            Assert.AreEqual(actualName, testName);
        }

        [Test]
        public void TestJSONReadFirstItemStackSize()
        {
            int actualSize = 100;
            ItemLibrary itemLibrary = JSONDeserializer.CreateLibraryFromJSON(testFolderPath + "/TestItemLibrary.json");

            int testSize = itemLibrary.AllItemTypes[0].StackSize;
            Console.WriteLine(testSize);
            Assert.AreEqual(actualSize, testSize);
        }

        [Test]
        public void TestIfRecipeReadsNotNull()
        {

            ItemLibrary itemLibrary = JSONDeserializer.CreateLibraryFromJSON(testFolderPath + "/TestItemLibrary.json");

            Recipe[] allRecipes = JSONDeserializer.ReadAllRecipes(testFolderPath + "/TestItemLibrary.json",itemLibrary);

            
            UnityEngine.Debug.Log(TestDataFormatter.ToSentence(GetCurrentMethodName()) + ": expected: " + "a recipe" + " actual: " + allRecipes[0].Ingredients[0]);

            Assert.IsNotNull(allRecipes[0]);
        }

    }
}