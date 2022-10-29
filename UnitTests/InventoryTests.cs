using NUnit.Framework;
using InventoryAndCharacterLogic;
using System;

namespace UnitTesting
{
    internal class InventoryTests
    {
        [Test]
        public void TestIfInventoryCanBeCreatedWithSize()
        {
            int testSize = 5;

            Inventory inventory = InventoryBuilder.CreateInventory(testSize);
            Console.WriteLine("TestIfInventoryCanBeCreatedWithSize: expected: "+testSize+" actual: "+inventory.Slots.Length.ToString());
            Assert.AreEqual(testSize,inventory.Slots.Length);
        }

        [Test]
        public void TestIfItemAmountCanBeAddedToInventory()
        {
            int testSize = 5;
            Inventory inventory = InventoryBuilder.CreateInventory(testSize);

            ItemAmount testItemAmount = new ItemAmount(new ItemType("testItemType",100), 10);

            InventoryHandler.AddToInventory(testItemAmount, inventory);

            ItemAmount lookup = LookupItemAmount(inventory, testItemAmount);


            Console.WriteLine("TestIfItemAmountCanBeAddedToInventory: expected: "+testItemAmount.Item.TypeName+" "+testItemAmount.Amount+" actual: "+lookup.Item.TypeName+" "+lookup.Amount);

            Assert.AreEqual(testItemAmount,lookup);
        }

        [Test]
        public void TestIfItemAmountCanBeRemovedFromInventory()
        {
            int testSize = 5;
            Inventory inventory = InventoryBuilder.CreateInventory(testSize);
            ItemAmount testItemAmount = new ItemAmount(new ItemType("testItemType", 100), 10);
            InventoryHandler.AddToInventory(testItemAmount, inventory);

            ItemAmount lookupIfPresent = LookupItemAmount(inventory, testItemAmount);
            Assert.AreEqual(testItemAmount, lookupIfPresent);

            InventoryHandler.RemoveSlotFromInventory(testItemAmount, inventory);
            ItemAmount lookupIfEmpty = LookupItemAmount(inventory, testItemAmount);


            Console.WriteLine("TestIfItemAmountCanBeRemovedFromInventory: expected: " + "not found"+  " " + 0 +" actual: " + lookupIfEmpty.Item.TypeName + " " + lookupIfEmpty.Amount);

            Assert.AreEqual("not found",lookupIfEmpty.Item.TypeName);
            Assert.AreEqual(0,lookupIfEmpty.Amount);

        }




        private static ItemAmount LookupItemAmount(Inventory inventory, ItemAmount testItemAmount)
        {

            for (int i = 0; i < inventory.Slots.Length; i++)
            {
                if (testItemAmount == inventory.Slots[i])
                {
                    return inventory.Slots[i];
                }
            }

            return new ItemAmount(new ItemType("not found", 0), 0);
        }
    }
}