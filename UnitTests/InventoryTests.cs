using NUnit.Framework;
using InventoryAndCharacterLogic;
using UnityEngine;
using System;
using System.Diagnostics;

namespace UnitTesting
{
    internal class InventoryTests
    {

        public static string GetCurrentMethodName()
        {
            StackTrace stackTrace = new StackTrace();
            StackFrame stackFrame = stackTrace.GetFrame(1);
 
            return stackFrame.GetMethod().Name;
        }




        [Test]
        public void TestIfInventoryCanBeCreatedWithSize()
        {
            int testSize = 5;

            Inventory inventory = InventoryBuilder.CreateInventory(testSize);
            UnityEngine.Debug.Log(TestDataFormatter.ToSentence(GetCurrentMethodName())+": expected: "+testSize+" actual: "+inventory.Slots.Length.ToString());
            Assert.AreEqual(testSize,inventory.Slots.Length);
        }

        [Test]
        public void TestIfInventoryCanBeCreatedWithEmptySlots()
        {
            int testSize = 2;
            Inventory inventory = InventoryBuilder.CreateInventory(testSize);
            UnityEngine.Debug.Log(TestDataFormatter.ToSentence(GetCurrentMethodName())+": expected: "+ItemType.Empty().TypeName+" actual:"+inventory.Slots[0].Item.TypeName);
            Assert.AreEqual(ItemType.Empty().TypeName,inventory.Slots[0].Item.TypeName);
            Assert.AreEqual(ItemType.Empty().TypeName,inventory.Slots[1].Item.TypeName);
        }




        [Test]
        public void TestIfInventoryHandlerCanFindEmptySlots()
        {
            int testSize = 2;
            Inventory inventory = InventoryBuilder.CreateInventory(testSize);
            int emptySlot = InventoryHandler.GetNextEmptySlot(inventory);
            UnityEngine.Debug.Log(TestDataFormatter.ToSentence(GetCurrentMethodName())+": expected: "+0+" actual:"+emptySlot);          
        }

        [Test]
        public void TestIfItemAmountCanBeAddedToInventory()
        {
            int testSize = 5;
            Inventory inventory = InventoryBuilder.CreateInventory(testSize);
            ItemAmount testItemAmount = new ItemAmount(new ItemType("testItemType",100), 10);
            InventoryHandler.AddToInventory(testItemAmount, inventory);
            ItemAmount lookup = LookupItemAmount(inventory, testItemAmount);

            UnityEngine.Debug.Log(TestDataFormatter.ToSentence(GetCurrentMethodName())+": expected: "+testItemAmount.Item.TypeName+" "+testItemAmount.Amount+" actual: "+lookup.Item.TypeName+" "+lookup.Amount);

            Assert.AreEqual(testItemAmount,lookup);
        }

        [Test]
        public void TestTEMPLATE()
        {
            UnityEngine.Debug.Log(TestDataFormatter.ToSentence(GetCurrentMethodName())+": expected: "+0+" actual: "+1);

            Assert.IsTrue(true);
        }

        [Test]
        public void TestIfInventoryReactsWhenNoEmptySlot()
        {
            int testSize = 1;
            Inventory inventory = InventoryBuilder.CreateInventory(testSize);


            ItemType testType0 = new ItemType("testItemType1",100);
            ItemType testType1 = new ItemType("testItemType2",100);

            ItemAmount testItemAmount0 = new ItemAmount(testType0,100);
            ItemAmount testItemAmount1 = new ItemAmount(testType1,100);

            InventoryHandler.AddToInventory(testItemAmount0, inventory);
            


            UnityEngine.Debug.Log(TestDataFormatter.ToSentence(GetCurrentMethodName())+": expected: "+false+" actual: "+InventoryHandler.HasEmptySlot(inventory));

            Assert.IsFalse(InventoryHandler.HasEmptySlot(inventory));
        }


        [Test]
        public void TestIfItemAmountCanBeAddedToInventoryInEmptySlot()
        {
            int testSize = 5;
            Inventory inventory = InventoryBuilder.CreateInventory(testSize);


            ItemType testType0 = new ItemType("testItemType1",100);
            ItemType testType1 = new ItemType("testItemType2",100);

            ItemAmount testItemAmount0 = new ItemAmount(testType0,100);
            ItemAmount testItemAmount1 = new ItemAmount(testType1,100);

            InventoryHandler.AddToInventory(testItemAmount0, inventory);
            InventoryHandler.AddToInventory(testItemAmount1, inventory);

            UnityEngine.Debug.Log(TestDataFormatter.ToSentence(GetCurrentMethodName())+": expected: "+testItemAmount0.Item.TypeName+" "+testItemAmount0.Amount+" actual: "+inventory.Slots[0].Item.TypeName+" "+inventory.Slots[0].Amount);
            UnityEngine.Debug.Log(TestDataFormatter.ToSentence(GetCurrentMethodName())+": expected: "+testItemAmount1.Item.TypeName+" "+testItemAmount1.Amount+" actual: "+inventory.Slots[1].Item.TypeName+" "+inventory.Slots[1].Amount);

            Assert.AreEqual(testItemAmount0.Item.TypeName,inventory.Slots[0].Item.TypeName);
            Assert.AreEqual(testItemAmount0.Amount,inventory.Slots[0].Amount);
            Assert.AreEqual(testItemAmount1.Item.TypeName,inventory.Slots[1].Item.TypeName);
            Assert.AreEqual(testItemAmount1.Amount,inventory.Slots[1].Amount);





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


            UnityEngine.Debug.Log(TestDataFormatter.ToSentence(GetCurrentMethodName())+": expected: " + "not found"+  " " + 0 +" actual: " + lookupIfEmpty.Item.TypeName + " " + lookupIfEmpty.Amount);

            Assert.AreEqual("not found",lookupIfEmpty.Item.TypeName);
            Assert.AreEqual(0,lookupIfEmpty.Amount);

        }

        [Test]
        public void TestIfInventoryHandlerCanCheckCorrectlyIfHasItemAmount()
        {
            int testInventorySize = 5;
            Inventory inventory = InventoryBuilder.CreateInventory(testInventorySize);

            int testAmount = 10;

            ItemType testItemType = new ItemType("testItemType",100);
            ItemAmount testItemAmount = new ItemAmount(testItemType, testAmount);
            InventoryHandler.AddToInventory(testItemAmount, inventory);


            Assert.IsTrue(InventoryHandler.HasAmountOfItem(testItemType,testAmount,inventory));

        }

        [Test]
        public void TestIfInventoryHandlerCanCheckCorrectlyIfDoesNotHaveAmount()
        {
            int testInventorySize = 5;
            Inventory inventory = InventoryBuilder.CreateInventory(testInventorySize);

            int testAmount = 10;
            int testAmountEvaluated = 11;

            ItemType testItemType = new ItemType("testItemType",100);
            ItemAmount testItemAmount = new ItemAmount(testItemType, testAmount);
            InventoryHandler.AddToInventory(testItemAmount, inventory);

            Assert.IsFalse(InventoryHandler.HasAmountOfItem(testItemType,testAmountEvaluated,inventory));

        }

        [Test]
        public void TestIfAmount5CanBeRemovedFrom10AndKeeps5()
        {
            int testSize = 5;
            Inventory inventory = InventoryBuilder.CreateInventory(testSize);

            ItemType testItemType = new ItemType("testItemType",100);

            ItemAmount testItemAmount = new ItemAmount(testItemType, 10);
            InventoryHandler.AddToInventory(testItemAmount, inventory);

            ItemAmount lookupIfPresent = LookupItemAmount(inventory, testItemAmount);
            Assert.AreEqual(testItemAmount, lookupIfPresent);


            ItemAmount testItemAmountToRemove = new ItemAmount(testItemType, 5);

            InventoryHandler.RemoveFromInventory(testItemAmountToRemove, inventory);
            
            int amountLeft = InventoryHandler.GetTotalAmountOfItem(testItemType, inventory);

            


            UnityEngine.Debug.Log(TestDataFormatter.ToSentence(GetCurrentMethodName())+": expected: " + 5 +" actual: " + amountLeft);

            Assert.AreEqual(5,amountLeft);

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