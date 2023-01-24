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

            LibraryHandler.RemoveItemType(library, indexToRemove);


            // assert that itemtype doesnt still exists
            for (int i = 0; i < library.AllItemTypes.Length; i++)
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

        ItemLibrary TestLibrary()
        {
            return LibraryHandler.LoadLibrary(testFolderPath + "/TestItemLibrary.json");
        }



        [Test]
        public void TestIfLibraryHandlerCanCalculateRawIngredients()
        {
            // Load Library
            // This Library contains a recipe for apple cake that has 30 flour as one of its ingredients
            // '10' 'flour' is made from 100 grain so the raw ingredients should contain 300 grain and no flour      

            ItemLibrary library = TestLibrary();

            // Get Item Type Grain
            ItemType grain = LibraryHandler.GetItemTypeByName("grain", library);

            // Get ItemType Flour
            ItemType flour = LibraryHandler.GetItemTypeByName("flour", library);

            // Find Recipe For Apple Cake

            Recipe recipe = LibraryHandler.GetRecipeByName("apple cake", library);

            Inventory rawIngredients = LibraryHandler.GetRawIngredientsOfRecipe(recipe, library);
            // has not 30 flour
            Assert.That(!InventoryHandler.HasAmountOfItem(flour, 30, rawIngredients));

            // has raw 300 grain
            UnityEngine.Debug.Log(InventoryHandler.GetTotalAmountOfItem(grain, rawIngredients));
            Assert.That(InventoryHandler.HasAmountOfItem(grain, 300, rawIngredients));



        }

        [Test]
        public void TestIfCanFindRecipeByName()
        {

            ItemLibrary library = TestLibrary();

            Assert.IsNotNull(LibraryHandler.GetRecipeByName("apple cake", library));
        }


        [Test]
        public void TestRecipeMinimalProduction()
        {
            // Load Library
            ItemLibrary library = TestLibrary();


            // This Library contains a recipe for orange cake that has 31 flour as one of its ingredients
            // '10' 'flour' is made from 100 grain so the raw ingredients should contain 400 grain and no flour      



            // Get Item Type Grain
            ItemType grain = LibraryHandler.GetItemTypeByName("grain", library);

            // Get ItemType Flour
            ItemType flour = LibraryHandler.GetItemTypeByName("flour", library);

            Recipe recipe = LibraryHandler.GetRecipeByName("orange cake", library);

            // Check If 31 flour is in the recipe
            int amountOfFlourInRecipe = InventoryHandler.GetTotalAmountOfItem(flour, recipe.Ingredients);
            UnityEngine.Debug.Log(amountOfFlourInRecipe);
            Assert.AreEqual(31, amountOfFlourInRecipe);



            Inventory rawIngredients = LibraryHandler.GetRawIngredientsOfRecipe(recipe, library);

            // check if flouir is not in the raw ingredients
            Assert.That(!InventoryHandler.HasAmountOfItem(flour, 31, rawIngredients));


            int amountOfGrainInRawIngredients = InventoryHandler.GetTotalAmountOfItem(grain, rawIngredients);


            UnityEngine.Debug.Log(amountOfGrainInRawIngredients);
            Assert.AreEqual(400, amountOfGrainInRawIngredients);
        }

        [Test]
        public void TestForMoreComplicatedRawCalculation()
        {
            ItemLibrary library = TestLibrary();
            
            //


                        // Get Item Type Grain
            ItemType grain = LibraryHandler.GetItemTypeByName("grain", library);


            Recipe recipe = LibraryHandler.GetRecipeByName("pear cake", library);

            Inventory rawIngredients = LibraryHandler.GetRawIngredientsOfRecipe(recipe, library);


            int amountOfGrainInRawIngredients = InventoryHandler.GetTotalAmountOfItem(grain, rawIngredients);
            
            for(int i = 0; i < rawIngredients.Slots.Length; i++)
            {
                ItemAmount itemAmount = rawIngredients.Slots[i];
                UnityEngine.Debug.Log(itemAmount.Item.TypeName +" "+itemAmount.Amount);
            }

            UnityEngine.Debug.Log(amountOfGrainInRawIngredients);

            Assert.AreEqual(500, amountOfGrainInRawIngredients);
        }


        [Test]
        public void TestIfCanCraftFromInventory()
        {
            ItemLibrary library = TestWoodWorkLibrary();

            Inventory testInventory = InventoryBuilder.CreateInventory();

            ItemType woodenLog = LibraryHandler.GetItemTypeByName("wooden log", library);
            ItemType ironOre = LibraryHandler.GetItemTypeByName("iron ore", library);

            ItemType workBench = LibraryHandler.GetItemTypeByName("workbench", library);

            InventoryHandler.AddToInventory(new ItemAmount(woodenLog, 3),testInventory);
            InventoryHandler.AddToInventory(new ItemAmount(ironOre, 60),testInventory);

            Recipe workBenchRecipe = LibraryHandler.GetRecipeByName(workBench.TypeName,library);
            foreach(ItemAmount ingredientName in workBenchRecipe.Ingredients.Slots)
            {
                UnityEngine.Debug.Log(ingredientName.Item.TypeName+" "+ingredientName.Amount);
            }
            
            Assert.That(InventoryHandler.CanCraftRaw(workBenchRecipe,testInventory, library));
        }

        private ItemLibrary TestWoodWorkLibrary()
        {
            return LibraryHandler.LoadLibrary(testFolderPath + "/Wood Work Test Library.json");
        }
    }
}