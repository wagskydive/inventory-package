using System;
using System.IO;
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
        public void TestJSONReadFirstItemDescription()
        {
            string actualDescription = "fruit that is delicious and green or red";
            ItemLibrary itemLibrary = JSONDeserializer.CreateLibraryFromJSON(testFolderPath + "/TestItemLibrary.json");

            string testDescription = itemLibrary.AllItemTypes[0].Description;
            Console.WriteLine(testDescription);
            Assert.AreEqual(actualDescription, testDescription);
        }

        [Test]
        public void TestIfRecipeReadsNotNull()
        {

            ItemLibrary itemLibrary = JSONDeserializer.CreateLibraryFromJSON(testFolderPath + "/TestItemLibrary.json");

            Recipe[] allRecipes = JSONDeserializer.ReadAllRecipes(testFolderPath + "/TestItemLibrary.json", itemLibrary);


            UnityEngine.Debug.Log(TestDataFormatter.ToSentence(GetCurrentMethodName()) + ": expected: " + "a recipe" + " actual: " + allRecipes[0].IngredientNames[0]);

            Assert.IsNotNull(allRecipes[0]);
        }

        [Test]
        public void TestIfLibraryNameGetsWrittenIntoFile()
        {
            string path = testFolderPath + "/TestSavingLibrary.json";
            string libraryName = "TestItemLibrary";
            ItemType[] itemTypes = new ItemType[1];
            itemTypes[0] = ItemType.CreateNew("testItemType", 10, "test description");
            ItemLibrary library = LibraryHandler.CreateNewLibrary(libraryName, itemTypes);

            JSONSerializer.SaveLibrary(library, path);


            ItemLibrary itemLibraryRead = JSONDeserializer.CreateLibraryFromJSON(path);

            Assert.AreEqual(libraryName, itemLibraryRead.LibraryName);
            File.Delete(path);

        }

        [Test]
        public void TestIfItemTypeNameGetsWrittenIntoFile()
        {
            string path = testFolderPath + "/TestSavingLibrary.json";
            string libraryName = "TestItemLibrary";
            string testItemTypeName = "testItemType";

            ItemType[] itemTypes = new ItemType[1];
            itemTypes[0] = ItemType.CreateNew(testItemTypeName, 10, "test description");
            ItemLibrary library = LibraryHandler.CreateNewLibrary(libraryName, itemTypes);

            JSONSerializer.SaveLibrary(library, path);


            ItemLibrary itemLibraryRead = JSONDeserializer.CreateLibraryFromJSON(path);

            Assert.AreEqual(testItemTypeName, itemLibraryRead.AllItemTypes[0].TypeName);
            File.Delete(path);
        }


        [Test]
        public void TestIfItemTypeStackSizeGetsWrittenIntoFile()
        {
            string path = testFolderPath + "/TestSavingLibrary.json";
            string libraryName = "TestItemLibrary";
            string testItemTypeName = "testItemType";
            string testItemTypeDescription = "test description";
            int testStackSize = 10;


            ItemType[] itemTypes = new ItemType[1];
            itemTypes[0] = ItemType.CreateNew(testItemTypeName, testStackSize, testItemTypeDescription);
            ItemLibrary library = LibraryHandler.CreateNewLibrary(libraryName, itemTypes);

            JSONSerializer.SaveLibrary(library, path);


            ItemLibrary itemLibraryRead = JSONDeserializer.CreateLibraryFromJSON(path);

            Assert.AreEqual(testStackSize, itemLibraryRead.AllItemTypes[0].StackSize);
            File.Delete(path);
        }

        [Test]
        public void TestIfItemTypeDescriptionGetsWrittenIntoFile()
        {
            string path = testFolderPath + "/TestSavingLibrary.json";
            string libraryName = "TestItemLibrary";
            string testItemTypeName = "testItemType";
            string testItemTypeDescription = "test description";


            ItemType[] itemTypes = new ItemType[1];
            itemTypes[0] = ItemType.CreateNew(testItemTypeName, 10, testItemTypeDescription);
            ItemLibrary library = LibraryHandler.CreateNewLibrary(libraryName, itemTypes);

            JSONSerializer.SaveLibrary(library, path);


            ItemLibrary itemLibraryRead = JSONDeserializer.CreateLibraryFromJSON(path);

            Assert.AreEqual(testItemTypeDescription, itemLibraryRead.AllItemTypes[0].Description);
            File.Delete(path);
        }

        [Test]
        public void TestIfRecipeGetsWrittenIntoFile()
        {
            string path = testFolderPath + "/TestSavingLibrary.json";
            string libraryName = "TestItemLibrary";
            string testItemResultTypeName = "testItemResultType";
            string testItemResultTypeDescription = "test result description";

            string testItemIngredientTypeName = "testItemIngedientType";
            string testItemIngredientTypeDescription = "test ingredient description";




            ItemType[] itemTypes = new ItemType[2];
            itemTypes[0] = ItemType.CreateNew(testItemResultTypeName, 10, testItemResultTypeDescription);
            itemTypes[1] = ItemType.CreateNew(testItemIngredientTypeName, 10, testItemIngredientTypeDescription);

            Inventory ingredients = InventoryBuilder.CreateInventory(1);
            InventoryHandler.AddToInventory(new ItemAmount(itemTypes[1], 1), ingredients);
            Recipe recipe = RecipeCreator.CreateRecipe(new ItemAmount(itemTypes[0], 1), ingredients,1);

            ItemLibrary library = LibraryHandler.CreateNewLibrary(libraryName, itemTypes);

            LibraryHandler.AddRecipeToLibrary(library, recipe);
        



            JSONSerializer.SaveLibrary(library, path);


            ItemLibrary itemLibraryRead = JSONDeserializer.CreateLibraryFromJSON(path);

            Assert.NotNull(itemLibraryRead.AllRecipes);
            Assert.AreEqual(1, itemLibraryRead.AllRecipes.Length);
            Assert.AreEqual(itemLibraryRead.AllRecipes[0].Result.Item.TypeName, testItemResultTypeName);
            Assert.AreEqual(itemLibraryRead.AllRecipes[0].Ingredients.Slots[0].Item.TypeName, testItemIngredientTypeName);
            File.Delete(path);
        }



    }
}