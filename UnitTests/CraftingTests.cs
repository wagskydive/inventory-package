using NUnit.Framework;
using InventoryPackage;

namespace UnitTesting
{
    internal class CraftingTests
    {
        string testFolderPath = "Assets/inventory-package/UnitTests/TestResources";
        ItemLibrary ThreeRootLevelLibrary()
        {
            ItemType[] itemTypes = new ItemType[3];

            itemTypes[0] = ItemType.CreateNew("level 0 root type");
            itemTypes[1] = ItemType.CreateNew("level 1 type");
            itemTypes[2] = ItemType.CreateNew("level 2 type");

            ItemLibrary library = LibraryHandler.CreateNew("test library", itemTypes); ;

            ItemAmount result0 = new ItemAmount(itemTypes[1], 1);
            Inventory ingredients0 = InventoryBuilder.CreateInventory(1);
            InventoryHandler.AddToInventory(new ItemAmount(itemTypes[0], 1), ingredients0);
            Recipe recipe0 = RecipeCreator.CreateRecipe(result0, ingredients0);
            LibraryHandler.AddRecipeToLibrary(library, recipe0);


            ItemAmount result1 = new ItemAmount(itemTypes[2], 1);
            Inventory ingredients1 = InventoryBuilder.CreateInventory(1);
            InventoryHandler.AddToInventory(new ItemAmount(itemTypes[1], 1), ingredients1);
            Recipe recipe1 = RecipeCreator.CreateRecipe(result1, ingredients1);
            LibraryHandler.AddRecipeToLibrary(library, recipe1);

            return library;
        }

        [Test]
        public void TestGetRootDistance()
        {
            ItemLibrary library = ThreeRootLevelLibrary();

            int rootDistance0 = LibraryHandler.GetRootDistance(library.AllItemTypes[0], library);
            UnityEngine.Debug.Log("Root Distance 0 = "+rootDistance0);
            Assert.That(rootDistance0 == 0);
            
            int rootDistance1 = LibraryHandler.GetRootDistance(library.AllItemTypes[1], library);
            UnityEngine.Debug.Log("Root Distance 1 = "+rootDistance1);
            Assert.That(rootDistance1 == 1);

            int rootDistance2 = LibraryHandler.GetRootDistance(library.AllItemTypes[2], library);
            UnityEngine.Debug.Log("Root Distance 2 = "+rootDistance2);
            Assert.That(rootDistance2 == 2);

        }


    }
}