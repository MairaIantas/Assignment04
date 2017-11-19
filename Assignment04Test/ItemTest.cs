using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assignment04;

namespace Assignment04Test
{
    [TestClass]
    public class ItemTest
    {
        Item item = new Item("name", 1, 0.5);

        [TestMethod]
        public void Constructor()
        {
            Assert.IsNotNull(item);
        }

        [TestMethod]
        public void GetNameSuccess()
        {
            Assert.IsNotNull(item.Name);
            Assert.AreEqual(item.Name, "name");
        }

        [TestMethod]
        public void GetGoldPiecesSuccess()
        {
            Assert.IsNotNull(item.GoldPieces);
            Assert.AreEqual(item.GoldPieces, 1);
        }

        [TestMethod]
        public void GetWeightSuccess()
        {
            Assert.IsNotNull(item.Weight);
            Assert.AreEqual(item.Weight, 0.5);
        }

        [TestMethod]
        public void GetToStringSuccess()
        {
            Assert.AreEqual(item.ToString(), String.Format("{0} {1} {2}", "name", 1, 0.5));
        }

        [TestMethod]
        public void CompareToSuccess()
        {
            Item newItem = new Item("name", 1, 0.5);
            Item secondItem = new Item("secondItem", 2, 0.7);

            Assert.AreEqual(item.CompareTo(newItem), 0);
            Assert.AreEqual(item.CompareTo(secondItem), -1);
            Assert.AreNotEqual(item.CompareTo(secondItem), 0);
        }
    }
}
