using NUnit.Framework;
using InventoryPackage;

namespace UnitTesting
{
    internal class CraftingTests
    {
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


        ItemLibrary UnSortedLLibrary()
        {
            ItemType[] itemTypes = new ItemType[4];

            itemTypes[0] = ItemType.CreateNew("level 0 root type");
            itemTypes[1] = ItemType.CreateNew("level 1 type");
            itemTypes[2] = ItemType.CreateNew("level 2 type");
            itemTypes[3] = ItemType.CreateNew("level 3 type");

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
            UnityEngine.Debug.Log("Root Distance 0 = " + rootDistance0);
            Assert.That(rootDistance0 == 0);

            int rootDistance1 = LibraryHandler.GetRootDistance(library.AllItemTypes[1], library);
            UnityEngine.Debug.Log("Root Distance 1 = " + rootDistance1);
            Assert.That(rootDistance1 == 1);

            int rootDistance2 = LibraryHandler.GetRootDistance(library.AllItemTypes[2], library);
            UnityEngine.Debug.Log("Root Distance 2 = " + rootDistance2);
            Assert.That(rootDistance2 == 2);

        }

        [Test]
        public void TestGetHighestLevelInInventory()
        {
            ItemLibrary library = UnSortedLLibrary();

            Inventory inventory = InventoryBuilder.CreateInventory(library.AllItemTypes.Length);
            for (int i = 0; i < library.AllItemTypes.Length; i++)
            {
                InventoryHandler.AddToInventory(new ItemAmount(library.AllItemTypes[i], 1), inventory);
            }


            ItemType itemType = InventoryHandler.GetHighestLevelItemInInventory(inventory, library);

            Assert.That(itemType.TypeName == "level 2 type");
        }

        [Test]
        public void TestCraftingFunction()
        {
            ItemLibrary library = ThreeRootLevelLibrary();

            Inventory input = InventoryBuilder.CreateInventory(10);
            Inventory output = InventoryBuilder.CreateInventory(10);

            Recipe recipe = library.AllRecipes[0];

            for (int i = 0; i < recipe.Ingredients.Slots.Length; i++)
            {
                ItemAmount slot = recipe.Ingredients.Slots[i];
                InventoryHandler.AddToInventory(new ItemAmount(slot.Item, slot.Amount), input);
            }
            Assert.That(Crafter.CraftNow(recipe, input, output));
            Assert.That(InventoryHandler.HasAmountOfItem(recipe.Result.Item, recipe.Result.Amount, output));
        }


    }
}