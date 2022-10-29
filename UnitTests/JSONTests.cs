using System;
using NUnit.Framework;
using InventoryAndCharacterLogic;
using UnityEngine;


namespace UnitTesting
{

   

    internal class JSONTests
    {

        string testFolerPath = "Assets/inventory-package/UnitTests/TestResources";
        [Test]
        public void TestReadFromResources()
        {
            string json = JSONDeserializer.ReadJSON(testFolerPath+"/TestFileOneString.txt");
            Console.WriteLine(json);
            Assert.AreEqual("One String",json);           
        }

        [Test]
        public void TestJSONReadLibraryName()
        {
            string actualName = "Test Library";

            string testName =  JSONDeserializer.ReadLibraryName(testFolerPath+"/TestItemLibrary.json");
            Console.WriteLine(testName);
            Assert.AreEqual(actualName,testName);    
        }

        [Test]
        public void TestJSONReadFirstItemTypeName()
        {
            string actualName = "apple";
            ItemLibrary itemLibrary = JSONDeserializer.CreateLibraryFromJSON(testFolerPath+"/TestItemLibrary.json");
     

            string testName = itemLibrary.AllItemTypes[0].TypeName;
            Console.WriteLine(testName);
            Assert.AreEqual(actualName,testName);    
        }

        [Test]
        public void TestJSONReadFirstItemStackSize()
        {
            int actualSize = 100;
            ItemLibrary itemLibrary = JSONDeserializer.CreateLibraryFromJSON(testFolerPath+"/TestItemLibrary.json");

            int testSize = itemLibrary.AllItemTypes[0].StackSize;
            Console.WriteLine(testSize);
            Assert.AreEqual(actualSize,testSize);    
        }
    }
}