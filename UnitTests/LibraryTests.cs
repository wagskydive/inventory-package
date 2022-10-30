using NUnit.Framework;
using InventoryPackage;
using System;
using System.Diagnostics;

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
    }
}