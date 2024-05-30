using Microsoft.VisualStudio.TestTools.UnitTesting;
using LinearData;
using System;
using System.Collections;
using Hierarchical;
using System.Security.Cryptography;

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
        public void BSTRemove_RemoveRoot() {
            BST<int> bst = new BST<int>();
            bst.Add(1);
            bst.Add(3);
            bst.Add(2);
            bst.Add(4);
            bst.Remove(1);
            Assert.IsTrue(bst.Contains(2));
            Assert.IsTrue(bst.Contains(4));
            Assert.IsTrue(bst.Root.value == 3, bst.Root.value.ToString());
            Assert.IsTrue(bst.Count == 3);
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
            for (int i = 0; i < array.Length; i++) {
                Assert.AreEqual(array[i], newArray[i]);
            }
        }

        [TestMethod]
        public void BSTToArray_Empty() {
            BST<int> bst = new BST<int>();

            int[] newArray = bst.ToArray();
            Assert.IsTrue(newArray.Length == 0);
        }

        [TestMethod]
        public void BSTInOrder_HappyPath() {
            string expected = "1, 2, 3, 4";
            BST<int> bst = new BST<int>();
            bst.Add(1);
            bst.Add(3);
            bst.Add(2);
            bst.Add(4);
            string order = bst.InOrder();
            Assert.AreEqual(expected, order, order);
        }

        [TestMethod]
        public void BSTInOrder_Empty() {
            BST<int> bst = new BST<int>();
            string order = bst.InOrder();
            Assert.AreEqual("", order, order);
        }

        [TestMethod]
        public void BSTPreOrder_HappyPath() {
            string expected = "1, 3, 2, 4";
            BST<int> bst = new BST<int>();
            bst.Add(1);
            bst.Add(3);
            bst.Add(2);
            bst.Add(4);
            string order = bst.PreOrder();
            Assert.AreEqual(expected, order, order);
        }
        [TestMethod]
        public void BSTPreOrder_Empty() {
            BST<int> bst = new BST<int>();
            string order = bst.PreOrder();
            Assert.AreEqual("", order, order);
        }
        [TestMethod]
        public void BSTPostOrder_HappyPath() {
            string expected = "2, 4, 3, 1";
            BST<int> bst = new BST<int>();
            bst.Add(1);
            bst.Add(3);
            bst.Add(2);
            bst.Add(4);
            string order = bst.PostOrder();
            Assert.AreEqual(expected, order, order);
        }
        [TestMethod]
        public void BSTPostOrder_Empty() {
            BST<int> bst = new BST<int>();
            string order = bst.PostOrder();
            Assert.AreEqual("", order, order);
        }
        #endregion

        #region AVL
        [TestMethod]
        public void AVLLeft_HappyPath() {
            AVL<int> avl = new AVL<int>();
            avl.Add(1);
            avl.Root.right = new AVLNode<int>(2);
            avl.Root.right.right = new AVLNode<int>(3);

            avl.rotateLeft(avl.Root, avl.Root.right);

            Assert.AreEqual(avl.Root.value, 2);
            Assert.AreEqual(avl.Root.left.value, 1);
            Assert.AreEqual(avl.Root.right.value, 3);
        }
		[TestMethod]
		public void AVLLeft_NotOnRoot() {
			AVL<int> avl = new AVL<int>();
			avl.Add(1);
			avl.Root.right = new AVLNode<int>(2);
			avl.Root.right.right = new AVLNode<int>(3);
			avl.Root.right.right.right = new AVLNode<int>(4);

			avl.rotateLeft(avl.Root, avl.Root.right.right);

			Assert.AreEqual(avl.Root.value, 1);
			Assert.AreEqual(avl.Root.right.value, 3);
			Assert.AreEqual(avl.Root.right.left.value, 2);
			Assert.AreEqual(avl.Root.right.right.value, 4);
		}

		[TestMethod]
		public void AVLRight_HappyPath() {
			AVL<int> avl = new AVL<int>();
			avl.Add(3);
			avl.Root.left = new AVLNode<int>(2);
			avl.Root.left.left = new AVLNode<int>(1);

			avl.rotateRight(avl.Root, avl.Root.left);

			Assert.AreEqual(avl.Root.value, 2);
			Assert.AreEqual(avl.Root.left.value, 1);
			Assert.AreEqual(avl.Root.right.value, 3);
		}
		[TestMethod]
		public void AVLRight_NotOnRoot() {
			AVL<int> avl = new AVL<int>();
			avl.Add(4);
			avl.Root.left = new AVLNode<int>(3);
			avl.Root.left.left = new AVLNode<int>(2);
			avl.Root.left.left.left = new AVLNode<int>(1);

			avl.rotateRight(avl.Root, avl.Root.left.left);

			Assert.AreEqual(avl.Root.value, 4);
			Assert.AreEqual(avl.Root.left.value, 2);
			Assert.AreEqual(avl.Root.left.right.value, 3);
			Assert.AreEqual(avl.Root.left.left.value, 1);
		}
		[TestMethod]
        public void AVLLeftRight_HappyPath() {

			AVL<int> avl = new AVL<int>();
			avl.Add(3);
			avl.Root.left = new AVLNode<int>(1);
			avl.Root.left.right = new AVLNode<int>(2);

            avl.rotateLeftRight(avl.Root, avl.Root.left);

			Assert.AreEqual(avl.Root.value, 2);
			Assert.AreEqual(avl.Root.left.value, 1);
			Assert.AreEqual(avl.Root.right.value, 3);
		}
		[TestMethod]
		public void AVLLeftRight_NotOnRoot() {

			AVL<int> avl = new AVL<int>();
			avl.Add(4);
			avl.Root.left = new AVLNode<int>(3);
			avl.Root.left.left = new AVLNode<int>(1);
			avl.Root.left.left.right = new AVLNode<int>(2);

			avl.rotateLeftRight(avl.Root, avl.Root.left.left);

			Assert.AreEqual(avl.Root.value, 4);
			Assert.AreEqual(avl.Root.left.value, 2);
			Assert.AreEqual(avl.Root.left.right.value, 3);
			Assert.AreEqual(avl.Root.left.left.value, 1);
		}
		[TestMethod]
		public void AVLRightLeft_HappyPath() {

			AVL<int> avl = new AVL<int>();
			avl.Add(1);
			avl.Root.right = new AVLNode<int>(3);
			avl.Root.right.left = new AVLNode<int>(2);

			avl.rotateRightLeft(avl.Root, avl.Root.right);

			Assert.AreEqual(avl.Root.value, 2);
			Assert.AreEqual(avl.Root.left.value, 1);
			Assert.AreEqual(avl.Root.right.value, 3);
		}
		[TestMethod]
		public void AVLRightLeft_NotOnRoot() {

			AVL<int> avl = new AVL<int>();
			avl.Add(1);
			avl.Root.right = new AVLNode<int>(2);
			avl.Root.right.right = new AVLNode<int>(4);
			avl.Root.right.right.left = new AVLNode<int>(3);

			avl.rotateRightLeft(avl.Root, avl.Root.right.right);

			Assert.AreEqual(avl.Root.value, 1);
			Assert.AreEqual(avl.Root.right.value, 3);
			Assert.AreEqual(avl.Root.right.right.value, 4);
			Assert.AreEqual(avl.Root.right.left.value, 2);
		}

        [TestMethod]
        public void AVLAdd_HappyPath() {

			AVL<int> avl = new AVL<int>();
			avl.Add(1);
            avl.Add(2);
            avl.Add(3);

            Assert.AreEqual(avl.Root.value, 2);
            Assert.AreEqual(avl.Root.left.value, 1);
            Assert.AreEqual(avl.Root.right.value, 3);
		}

        [TestMethod]
        public void AVLAdd_BiggerTree() {

			AVL<int> avl = new AVL<int>();
			avl.Add(1);
			avl.Add(2);
			avl.Add(3);
			avl.Add(4);
			avl.Add(5);
			avl.Add(6);
			avl.Add(7);

            Assert.AreEqual(avl.Root.value, 4);
            Assert.AreEqual(avl.Root.left.value, 2);
            Assert.AreEqual(avl.Root.left.left.value, 1);
            Assert.AreEqual(avl.Root.left.right.value, 3);
            Assert.AreEqual(avl.Root.right.value, 6);
            Assert.AreEqual(avl.Root.right.left.value, 5);
            Assert.AreEqual(avl.Root.right.right.value, 7);
		}

		[TestMethod]
		public void AVLClear_HappyPath() {
			AVL<int> avl = new AVL<int>();
			avl.Add(1);
			avl.Add(2);
			avl.Clear();

			Assert.AreEqual(0, avl.Count);
			Assert.IsTrue(avl.Root == null, "Clear did not work");
		}

		[TestMethod]
		public void AVLClear_empty() {
			AVL<int> avl = new AVL<int>();
            avl.Clear();

			Assert.AreEqual(0, avl.Count);
            Assert.IsTrue(avl.Root == null, "Clear did not work"); ;
		}

		[TestMethod]
		public void AVLContains_HappyPath() {
			AVL<int> bst = new AVL<int>();
			bst.Add(1);
			bst.Add(2);
			Assert.IsTrue(bst.Contains(1));
			Assert.IsTrue(bst.Contains(2));
			Assert.IsFalse(bst.Contains(3));
		}

		[TestMethod]
		public void AVLContains_Clear() {
			AVL<int> bst = new AVL<int>();
			Assert.IsFalse(bst.Contains(1));
		}

		[TestMethod]
		public void AVLHeight_HappyPath() {
			AVL<int> bst = new AVL<int>();
			bst.Add(3);
			bst.Add(2);
			bst.Add(1);
			Assert.IsTrue(bst.Height() == 2);
		}

		[TestMethod]
		public void AVLHeight_Empty() {
			AVL<int> bst = new AVL<int>();
			Assert.IsTrue(bst.Height() == 0);
		}

		[TestMethod]
		public void AVLToArray_HappyPath() {
			int[] array = { 4, 2, 6, 1, 3, 5, 7 };
			AVL<int> bst = new AVL<int>();
			bst.Add(1);
			bst.Add(2);
			bst.Add(3);
			bst.Add(4);
			bst.Add(5);
			bst.Add(6);
			bst.Add(7);
			int[] newArray = bst.ToArray();
			for (int i = 0; i < array.Length; i++) {
				Assert.AreEqual(array[i], newArray[i]);
			}
		}

		[TestMethod]
		[ExpectedException(typeof(NullReferenceException))]
		public void AVLToArray_Empty() {
			AVL<int> bst = new AVL<int>();

			int[] newArray = bst.ToArray();
			Assert.IsTrue(newArray.Length == 0);
		}

		[TestMethod]
		public void AVLInOrder_HappyPath() {
			string expected = "1, 2, 3, 4";
			AVL<int> bst = new AVL<int>();
			bst.Add(1);
			bst.Add(3);
			bst.Add(2);
			bst.Add(4);
			string order = bst.InOrder();
			Assert.AreEqual(expected, order, order);
		}

		[TestMethod]
		public void AVLInOrder_Empty() {
			AVL<int> bst = new AVL<int>();
			string order = bst.InOrder();
			Assert.AreEqual("", order, order);
		}

		[TestMethod]
		public void AVLPreOrder_HappyPath() {
			string expected = "4, 2, 1, 3, 6, 5, 7";
			AVL<int> bst = new AVL<int>();
			bst.Add(1);
			bst.Add(2);
			bst.Add(3);
			bst.Add(4);
			bst.Add(5);
			bst.Add(6);
			bst.Add(7);
			string order = bst.PreOrder();
			Assert.AreEqual(expected, order, order);
		}
		[TestMethod]
		public void AVLPreOrder_Empty() {
			AVL<int> bst = new AVL<int>();
			string order = bst.PreOrder();
			Assert.AreEqual("", order, order);
		}
		[TestMethod]
		public void AVLPostOrder_HappyPath() {
			string expected = "1, 3, 2, 5, 7, 6, 4";
			AVL<int> bst = new AVL<int>();
			bst.Add(1);
			bst.Add(2);
			bst.Add(3);
			bst.Add(4);
			bst.Add(5);
			bst.Add(6);
			bst.Add(7);
			string order = bst.PostOrder();
			Assert.AreEqual(expected, order, order);
		}
		[TestMethod]
		public void AVLPostOrder_Empty() {
			AVL<int> bst = new AVL<int>();
			string order = bst.PostOrder();
			Assert.AreEqual("", order, order);
		}

		[TestMethod]
		public void AVLRemove_Happy() {
			AVL<int> avl = new AVL<int>();
			avl.Add(4);
			avl.Add(3);
			avl.Add(2);
			avl.Add(1);
			avl.Remove(3);


			Assert.AreEqual(avl.Root.value, 2);
			Assert.AreEqual(avl.Root.left.value, 1);
			Assert.AreEqual(avl.Root.right.value, 4);
		}
		[TestMethod]
		public void AVLRemove_Empty() {
			AVL<int> avl = new AVL<int>();
			avl.Remove(3);
			Assert.IsNull(avl.Root);
		}
		#endregion
	}
}
