using Microsoft.VisualStudio.TestTools.UnitTesting;
using LinearData;
using System;
using System.Collections;
using Hierarchical;

namespace Testing {
    [TestClass]
    public class HierarchicalUnitTest {
        #region BST
        [TestMethod]
        public void BSTAdd_HappyPath() {
            BST<int> bst = new BST<int>();
            bst.Add(1);
            Assert.IsTrue(1 == bst.Count, "Add failed, count was inaccurate");
            Assert.IsTrue(bst.Root.value == 1, "Add failed, root was not correct");
        }

        [TestMethod]
        public void BSTAdd_MultipleNodes() {
            BST<int> bST = new BST<int>();
            bST.Add(1);
            bST.Add(2);
            Assert.IsTrue(2 == bST.Count, "Add failed, count was inaccurate");
            Assert.IsTrue(bST.Root.value == 1, "Add failed, root was not correct");
            Assert.IsTrue(bST.Root.right.value == 2, "Add failed, right was not correct");
        }

        [TestMethod]
        public void BSTAdd_duplicateNodes() {
            BST<int> bST = new BST<int>();
            bST.Add(1);
            bST.Add(1);// duplicated 1 will not be added
            Assert.IsTrue(1 == bST.Count, "Add failed, count was inaccurate");
            Assert.IsTrue(bST.Root.value == 1, "Add failed, root was not correct");
            Assert.IsTrue(bST.Root.right == null, "Add failed, rest of tree was not null");
            Assert.IsTrue(bST.Root.left == null, "Add failed, rest of tree was not null");
        }

        [TestMethod]
        public void BSTClear_HappyPath() {
            BST<int> bST = new BST<int>();
            bST.Add(1);
            bST.Add(2);
            bST.Clear();

            Assert.AreEqual(0, bST.Count);
            Assert.IsTrue(bST.Root == null, "Clear did not work");
        }

        [TestMethod]
        public void BSTClear_Clear() {
            BST<int> bST = new BST<int>();
            bST.Clear();

            Assert.AreEqual(0, bST.Count);
            Assert.IsTrue(bST.Root == null, "Clear did not work");
        }

        [TestMethod]
        public void BSTContains_HappyPath() {
            BST<int> bst = new BST<int>();
            bst.Add(1);
            bst.Add(2);
            Assert.IsTrue(bst.Contains(1));
            Assert.IsTrue(bst.Contains(2));
            Assert.IsFalse(bst.Contains(3));
        }

        [TestMethod]
        public void BSTContains_Clear() {
            BST<int> bst = new BST<int>();
            Assert.IsFalse(bst.Contains(1));
        }

        [TestMethod]
        public void BSTRemove_HappyPath() {
            BST<int> bst = new BST<int>();
            bst.Add(1);
            bst.Add(2);
            Assert.IsTrue(bst.Contains(2));
            bst.Remove(2);
            Assert.IsFalse(bst.Contains(2));
            Assert.IsTrue(bst.Count == 1);
        }

        [TestMethod]
        public void BSTRemove_extraItems() {
            BST<int> bst = new BST<int>();
            bst.Add(1);
            bst.Add(3);
            bst.Add(2);
            bst.Add(4);
            bst.Remove(3);
            Assert.IsTrue(bst.Contains(2));
            Assert.IsTrue(bst.Contains(4));
            Assert.IsTrue(bst.Count == 3);
        }

        [TestMethod]
        public void BSTHeight_HappyPath() {
            BST<int> bst = new BST<int>();
            bst.Add(3);
            bst.Add(2);
            bst.Add(1);
            Assert.IsTrue(bst.Height() == 3);
        }

        [TestMethod]
        public void BSTHeight_Empty() {
            BST<int> bst = new BST<int>();
            Assert.IsTrue(bst.Height() == 0);
        }

        [TestMethod]
        public void BSTToArray_HappyPath() {
            int[] array = { 1, 2, 3, 4 };
            BST<int> bst = new BST<int>();
            bst.Add(1);
            bst.Add(3);
            bst.Add(2);
            bst.Add(4);
            int[] newArray = bst.ToArray();
            for (int i = 0; i < newArray.Length; i++) {
                Assert.AreEqual(array[i], newArray[i]);
            }
        }
        #endregion
    }
}
