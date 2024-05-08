using Microsoft.VisualStudio.TestTools.UnitTesting;
using LinearData;
using System;
using System.Collections;
using Hierarchical;

namespace Testing {
    #region BST
    [TestClass]
    public class HierarchicalUnitTest {
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
    }
    #endregion
}
