using NUnit.Framework;
using InventoryAndCharacterLogic;


namespace UnitTesting
{
    internal class CharacterTests
    {
        [Test]
        public void TestIfCharacterCanBeCreatedWithName()
        {
            string testName = "test name";
            Character character = new Character(testName);

            Assert.AreEqual(testName,character.CharacterName);
        }



    }
}