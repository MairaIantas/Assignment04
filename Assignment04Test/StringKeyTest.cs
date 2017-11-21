using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assignment04;

namespace Assignment04Test
{
    [TestClass]
    public class StringKeyTest
    {
        string keyName = "0";
        StringKey sk = new StringKey("0");
        StringKey skzero = new StringKey("0");

        [TestMethod]
        public void Constructor()
        {
            Assert.AreEqual(sk.GetKeyName(), keyName);
        }

        [TestMethod]
        public void GetHashCodeSuccess()
        {
            Assert.AreEqual(sk.GetHashCode(), skzero.GetHashCode());
        }

        [TestMethod]
        public void EqualsSuccess()
        {
            Assert.IsTrue(sk.Equals(skzero));
            Assert.IsFalse(sk.Equals("0"));
            Assert.IsFalse(sk.Equals(" "));
            Assert.IsFalse(sk.Equals("1"));
        }

        [TestMethod]
        public void GetKeyNameSuccess()
        {
            Assert.AreEqual(sk.GetKeyName(), keyName);
            Assert.AreNotEqual(sk.GetKeyName(), " ");
        }

        [TestMethod]
        public void ToStringSuccess()
        {
            Assert.AreEqual(sk.ToString(), keyName);
            Assert.AreNotEqual(sk.ToString(), " ");
        }

        [TestMethod]
        public void CompareToSuccess()
        {
            StringKey newSK = new StringKey("0");
            StringKey secondSK = new StringKey("1");

            Assert.AreEqual(sk.CompareTo(newSK), 0);
            Assert.AreNotEqual(sk.CompareTo(secondSK), 0);
        }
    }
}
