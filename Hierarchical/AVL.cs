using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hierarchical {
	public class AVL<T> where T : IComparable<T> {
		public AVLNode<T> Root { get; set; }
		public int Count { get; set; }

		public void Add(T value) {
			AVLNode<T> node = new AVLNode<T>(value);
			if (Root == null) {
				Root = node;
				return;
			}

		}

		private AVLNode<T> Traverse(AVLNode<T> node, T value) {
			if (node == null) {
				return null;
			}
			AVLNode<T> nextNode;
			int valueCompared = value.CompareTo(node.value);
			if (valueCompared == 0) {
				nextNode = null;
			} else if (valueCompared > 0) {
				nextNode = node.right;
			} else {
				nextNode = node.left;
			}


			return nextNode == null ? node : Traverse(nextNode, value);
		}

		//bigO(logn)
		private AVLNode<T> TraverseParent(AVLNode<T> node, T value) {
			if (node == null) {
				return null;
			}
			AVLNode<T> nextNode;
			int valueCompared = value.CompareTo(node.value);
			if (valueCompared == 0) {
				nextNode = null;
			} else if (valueCompared > 0) {
				valueCompared = value.CompareTo(node.right.value);
				if (valueCompared == 0) {
					nextNode = null;
				} else if (valueCompared > 0) {
					nextNode = node.right;
				} else {
					nextNode = node.left;
				}
			} else {
				valueCompared = value.CompareTo(node.left.value);
				if (valueCompared == 0) {
					nextNode = null;
				} else if (valueCompared > 0) {
					nextNode = node.right;
				} else {
					nextNode = node.left;
				}
			}


			return nextNode == null ? node : TraverseParent(nextNode, value);
		}

		/*
         * Clear() 
         *  traverse through each item recursivly
         *      deallocate each node, once all children of said node has been dealocatted
         *      
         *  reset count
         */
		//clears the tree and resets the root BigO(1)
		public void Clear() {
			Root = null;
			Count = 0;
		}

		/// <summary>
		/// return whether the tree contains a value
		/// </summary>
		/// <param name="value">the value to check for</param>
		/// <returns>true if the value is found in the tree, false if not</returns>
		public bool Contains(T value) {
			AVLNode<T> node = Traverse(Root, value);
			if (node == null) { return false; }
			if (node.value.CompareTo(value) == 0) return true;
			return false;
		}

		public void Remove(T value) {

		}

		/// <summary>
		/// find the height of the tree BigO(n)
		/// </summary>
		/// <returns>an integer value repesenting the height of the tree</returns>
		public int Height() {
			if (Root == null) return 0;

			return HeightR(Root, 1);
		}

		public int HeightR(AVLNode<T> node, int height) {
			if (node.left != null) {
				int leftHeight = HeightR(node.left, height + 1);
				if (leftHeight > height) {
					height = leftHeight;
				}
			}
			if (node.right != null) {
				int rightHeight = HeightR(node.right, height);
				if (rightHeight > height) {
					height = rightHeight;
				}
			}
			return height;
		}

		/// <summary>
		/// generate the tree into a array BigO(n)
		/// </summary>
		/// <returns>a sorted array representing the values in the tree</returns>
		public T[] ToArray() {
			T[] array = new T[0];
			//array = ToArray(Root, array);
			return array;
		}


		/// <summary>
		/// generates a string representation of the tree InOrder BigO(n)
		/// </summary>
		/// <returns>a string in order from least to greatest</returns>
		public string InOrder() {
			string final = string.Empty;
			final = InOrder(Root, final);
			return final;
		}

		/*
         * InOrder() {
         *  create string
         *  string equals InOrderRecursive(root, string)
         *  return string
         * }
         * 
         * InOrderRecusive(Node, string) {
         *  if node is null, return string
         *  string = InOrderRecursive(node.left, string)
         *  string += node value + ", "
         *  string = InOrderRecursive(node.right, string)
         *  return string
         * }
         */

		private string InOrder(AVLNode<T> node, string final) {
			if (node == null) return final;
			if (node.left != null) {
				final = InOrder(node.left, final);
				final += ", ";
			}
			final += node.value.ToString();
			if (node.right != null) {
				final += ", ";
				final = InOrder(node.right, final);
			}
			return final;
		}

		/// <summary>
		/// generates a string representation of the tree in pre order(Root, left, right) BigO(n)
		/// </summary>
		/// <returns>a string in pre order(root, left, right) representing the tree</returns>
		public string PreOrder() {
			string final = string.Empty;
			final = PreOrder(Root, final);
			return final;
		}

		private string PreOrder(AVLNode<T> node, string final) {
			if (node == null) return final;
			final += node.value.ToString();
			if (node.left != null) {
				final += ", ";
				final = PreOrder(node.left, final);
			}
			if (node.right != null) {
				final += ", ";
				final = PreOrder(node.right, final);
			}
			return final;


		}

		/// <summary>
		/// generates a string representation of the tree in PostOrder(left, right, root) BigO(n)
		/// </summary>
		/// <returns>a string in postOrder(left, right, root) representing the tree</returns>
		public string PostOrder() {
			string final = string.Empty;
			final = PostOrder(Root, final);
			return final;
		}

		private string PostOrder(AVLNode<T> node, string final) {
			if (node == null) return final;
			if (node.left != null) {
				final = PostOrder(node.left, final);
				final += ", ";
			}
			if (node.right != null) {
				final = PostOrder(node.right, final);
				final += ", ";
			}
			final += node.value.ToString();
			return final;


		}

		public void rotateRight(AVLNode<T> parent, AVLNode<T> pivot) {
			//Create a node to be the top node
			AVLNode<T> topNode = null;
			//If(parent.right is equal to pivot) {
			if (parent.left != null && parent.left.value.CompareTo(pivot.value) == 0) {
				topNode = parent;
				//Top node = parent //top node is root
				Root = pivot;
				//Root = pivot
			} else if (pivot.value.CompareTo(parent.value) > 0) {
				//else If(pivot is greater than parent) {
				//Set top node equal to parent’s right
				topNode = parent.right;
				// Parent right node equal to pivot
				parent.right = pivot;
			} else {
				//			Set top node equal to parent’s left
				topNode = parent.left;
				//Parent left node equal to pivot
				parent.left = pivot;
			}
			//		Top node right equal to null
			topNode.left = pivot.right;
			//Pivot left equal to top node
			pivot.right = topNode;
		}

		public void rotateLeft(AVLNode<T> parent, AVLNode<T> pivot) {
			//Create a node to be the top node
			AVLNode<T> topNode = null;
			//If(parent.right is equal to pivot) {
			if (parent.right != null && parent.right.value.CompareTo(pivot.value) == 0) {
				topNode = parent;
				//Top node = parent //top node is root
				Root = pivot;
				//Root = pivot
			} else if (pivot.value.CompareTo(parent.value) > 0) {
				//else If(pivot is greater than parent) {
				//Set top node equal to parent’s right
				topNode = parent.right;
				// Parent right node equal to pivot
				parent.right = pivot;
			} else {
				//			Set top node equal to parent’s left
				topNode = parent.left;
				//Parent left node equal to pivot
				parent.left = pivot;
			}
			//		Top node right equal to null
			topNode.right = pivot.left;
			//Pivot left equal to top node
			pivot.left = topNode;
		}

		public void rotateRightLeft(AVLNode<T> parent, AVLNode<T> pivot) {
			if (parent.right.value.CompareTo(pivot.value) == 0) {
				rotateRight(parent, pivot.left);
				rotateLeft(parent, parent.right);
			} else if (pivot.value.CompareTo(parent.value) > 0) {
				rotateRight(parent.right, pivot.left);
				rotateLeft(parent, parent.right.right);
			} else {
				rotateRight(parent.left, pivot.left);
				rotateLeft(parent, parent.left.right);
			}
		}

		public void rotateLeftRight(AVLNode<T> parent, AVLNode<T> pivot) {
			if (parent.left.value.CompareTo(pivot.value) == 0) {
				rotateLeft(parent, pivot.right);
			rotateRight(parent, parent.left);
			} else if (pivot.value.CompareTo(parent.value) > 0) {
				rotateLeft(parent.right, pivot.right);
			rotateRight(parent, parent.right.left);
			} else {
				rotateLeft(parent.left, pivot.right);
			rotateRight(parent, parent.left.left);
			}
		}



	}

	public class AVLNode<T> where T : IComparable<T> {

		public AVLNode(T value) {
			this.value = value;
		}
		public T value { get; set; }
		public AVLNode<T> left { get; set; }
		public AVLNode<T> right { get; set; }
		int height;
	}
}
