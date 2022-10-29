using NUnit.Framework;
using InventoryPackage;


namespace UnitTesting
{
    internal class CanTestItself
    {
        [Test]
        public void TestIfTestRuns()
        {
            bool testToggle;
            testToggle = true;
            Assert.IsTrue(testToggle);
        }
    }
}