using Microsoft.VisualStudio.TestTools.UnitTesting;
using LinearData;
using System;

namespace Testing {
    [TestClass]
    public class UnitTest {
        #region LinkedList
        [TestMethod]
        public void TestSingleAdd_EmptyList() {
            LinkedList<int> list = new LinkedList<int>();
            list.Add(42);

            Assert.IsTrue(list.head != null, "append failed, tail 42 expected value to be 42 but tail was null");
            Assert.IsTrue(list.head.value == 42, "append failed, tail was not equal to 42 was " + list.head.value);
            Assert.IsTrue(list.Count == 1, "append failed, Count was not Accurate");
        }

        [TestMethod]
        public void TestSingleAdd_NonEmptyList() {
            int expectedValue = 404;
            LinkedList<int> list = new LinkedList<int>();
            list.Add(42);
            list.Add(expectedValue);

            //Assert.IsTrue(list.head != null, "append failed, tail 42 expected value to be 42 but tail was null");
            Assert.IsTrue(list.head.next != null, "append failed, expected head have next but was null");

            Assert.IsTrue(list.head.next.value == expectedValue, "Append failed, tail was not equal to " + expectedValue + " was " + list.head.next.value);

        }

        [TestMethod]
        public void TestSingleInsert_HappyPath() {
            LinkedList<int> list = new LinkedList<int>();
            list.Add(42);
            list.Add(42);
            list.Insert(404, 1);

            Assert.IsTrue(list.head.next.value == 404, "Insert failed, expected 404 at index 1");
        }

        [TestMethod]
        public void TestSingleInsert_SingleList() {

            LinkedList<int> list = new LinkedList<int>();
            list.Add(42);
            list.Insert(404, 0);

            Assert.IsTrue(list.head.value == 404, "Insert failed, expected 404 at index 1");
        }
        [TestMethod]
        public void TestRemoveAll_HappyPath() {
            int valueToRemove = 404;
            LinkedList<int> linkedList = new LinkedList<int>();
            linkedList.Add(valueToRemove);
            linkedList.Add(valueToRemove);
            linkedList.Add(42);
            linkedList.Add(valueToRemove);
            linkedList.RemoveAll(valueToRemove);

            Assert.IsTrue(linkedList.head.next == null, "remove failed, expected head next to be null, but was not");
            Assert.IsTrue(linkedList.head.value == 42, "remove failed, head value was not correct");
            Assert.IsTrue(linkedList.Count == 1, "remove failed, Count Inaccurate");
        }

        #endregion

    }
}
