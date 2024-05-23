using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hierarchical {
	public class AVL<T> where T : IComparable<T> {
		public AVLNode<T> Root { get; set; }
		public int Count { get; set; }

		//will not add duplicate nodes BigO(logn)
		public void Add(T value) {
			if (Root == null) {
				AVLNode<T> node = new AVLNode<T>(value);
				Root = node;
				return;
			}

			Add(value, Root);
		}

		//bigO(logn)
		private int Add(T value, AVLNode<T> current) {
			int childHeight;
			if (value.CompareTo(current.value) > 0) {
				if (current.right == null) {
					current.right = new AVLNode<T>(value);
					childHeight = current.right.height;
				} else {
					childHeight = Add(value, current.right);
				}
			} else if (value.CompareTo(current.value) < 0) {
				if (current.left == null) {
					current.left = new AVLNode<T>(value);
					childHeight = current.left.height;
				} else {
					childHeight = Add(value, current.left);
				}
			} else {// case of duplicate
				return 0;
			}
			if (current.height == childHeight) current.height += 1;
			if (current.value.CompareTo(Root.value) == 0) {
				int currentLeft;
				int currentright;
				if (current.left == null) {
					currentLeft = 0;
				} else {
					currentLeft = current.left.height;
				}

				if (current.right == null) {
					currentright = 0;
				} else {
					currentright = current.right.height;
				}
				current.balance = currentLeft - currentright;
				if (current.balance >= 2) {
					if (current.left.balance < 0) {
						rotateLeftRight(current, current.left);
					} else {
						rotateRight(current, current.left);
					}
				} else if (current.balance <= -2) {
					if (current.right.balance > 0) {
						rotateRightLeft(current, current.right);
					} else {
						rotateLeft(current, current.right);
					}
				}
				current.balance = 0;
			}

			if (current.left != null && current.left.balance <= -2) {//negative is left rotation
				AVLNode<T> temp = current.left;
				if (current.left.right.balance > 0) {//check for extra rotation
					rotateRightLeft(current, current.left.right);
				} else {
					rotateLeft(current, current.left.right);
				}

				temp.balance = 0;
			}
			if (current.left != null && current.left.balance >= 2) {
				AVLNode<T> temp = current.left;
				if (current.left.left.balance < 0) {
					rotateLeftRight(current, current.left.left);
				} else {
					rotateRight(current, current.left.left);
				}
				temp.balance = 0;
			}
			if (current.right != null && current.right.balance >= 2) {
				AVLNode<T> temp = current.right;
				if (current.right.left.balance < 0) {
					rotateLeftRight(current, current.right.left);
				} else {
					rotateRight(current, current.right.left);
				}
				temp.balance = 0;
			}
			if (current.right != null && current.right.balance <= -2) {
				AVLNode<T> temp = current.right;
				if (current.right.right != null && current.right.right.balance > 0) {
					rotateRightLeft(current, current.right.right);
				} else {
					rotateLeft(current, current.right.right);
				}

				temp.balance = 0;
			}
			{
				int currentLeft;
				int currentright;
				if (current.left == null) {
					currentLeft = 0;
				} else {
					currentLeft = current.left.height;
				}

				if (current.right == null) {
					currentright = 0;
				} else {
					currentright = current.right.height;
				}
				current.balance = currentLeft - currentright;
			}

			return current.height;
		}

		public void Remove(T value) {
			if (Root == null) { return; }
			if (Root.value.CompareTo(value) == 0) {
				Count--;
				AVLNode<T> temp = Root.left;
				Root = Root.right;
				AVLNode<T> temp2 = Root;
				while (temp2 != null && temp2.left != null) {//log n
					temp2 = temp2.left;
				}
				if (temp2 == null) {
					temp2 = temp;
				} else {
					temp2.left = temp;
				}
			} else {
				AVLNode<T> node = TraverseParent(Root, value); //logn
				if (node == null) { return; }
				if (node.right.value.CompareTo(value) == 0) {
					Count--;
					AVLNode<T> temp = node.right.left;
					node.right = node.right.right;
					AVLNode<T> temp2 = node.right;
					while (temp2 != null && temp2.left != null) {//log n
						temp2 = temp2.left;
					}
					if (temp2 == null) {
						temp2 = temp;
					} else {
						temp2.left = temp;
					}
				} else if (node.left.value.CompareTo(value) == 0) {
					Count--;
					AVLNode<T> temp = node.left.left;
					node.left = node.left.right;
					AVLNode<T> temp2 = node.left;
					while (temp2 != null && temp2.left != null) {
						temp2 = temp2.left;
					}
					if (temp2 == null) {
						temp2 = temp;
					} else {
						temp2.left = temp;
					}
				}
			}

			//check balance
			CheckBalance(Root);
		}

		private int CheckBalance(AVLNode<T> current) {
			if (current == null) { return 0; }
			//recursivly traverse left
			int left = CheckBalance(current.left);
			//recursivly traverse right
			int right = CheckBalance(current.right);
			current.height = (left > right) ? left + 1: right + 1;
			current.balance = left - right;
			if (current.value.CompareTo(Root.value) == 0) {
				int currentLeft;
				int currentright;
				if (current.left == null) {
					currentLeft = 0;
				} else {
					currentLeft = current.left.height;
				}

				if (current.right == null) {
					currentright = 0;
				} else {
					currentright = current.right.height;
				}
				current.balance = currentLeft - currentright;
				if (current.balance >= 2) {
					if (current.left.balance < 0) {
						rotateLeftRight(current, current.left);
					} else {
						rotateRight(current, current.left);
					}
				} else if (current.balance <= -2) {
					if (current.right.balance > 0) {
						rotateRightLeft(current, current.right);
					} else {
						rotateLeft(current, current.right);
					}
				}
				current.balance = 0;
			}

			if (current.left != null && current.left.balance <= -2) {//negative is left rotation
				AVLNode<T> temp = current.left;
				if (current.left.right.balance > 0) {//check for extra rotation
					rotateRightLeft(current, current.left.right);
				} else {
					rotateLeft(current, current.left.right);
				}

				temp.balance = 0;
			}
			if (current.left != null && current.left.balance >= 2) {
				AVLNode<T> temp = current.left;
				if (current.left.left.balance < 0) {
					rotateLeftRight(current, current.left.left);
				} else {
					rotateRight(current, current.left.left);
				}
				temp.balance = 0;
			}
			if (current.right != null && current.right.balance >= 2) {
				AVLNode<T> temp = current.right;
				if (current.right.left.balance < 0) {
					rotateLeftRight(current, current.right.left);
				} else {
					rotateRight(current, current.right.left);
				}
				temp.balance = 0;
			}
			if (current.right != null && current.right.balance <= -2) {
				AVLNode<T> temp = current.right;
				if (current.right.right != null && current.right.right.balance > 0) {
					rotateRightLeft(current, current.right.right);
				} else {
					rotateLeft(current, current.right.right);
				}

				temp.balance = 0;
			}
			{
				int currentLeft;
				int currentright;
				if (current.left == null) {
					currentLeft = 0;
				} else {
					currentLeft = current.left.height;
				}

				if (current.right == null) {
					currentright = 0;
				} else {
					currentright = current.right.height;
				}
				current.balance = currentLeft - currentright;
			}
			return current.height;
		}

		//recursive method that traverses down the tree
		//returns a node for the value provided
		//if null is provided return null which assmes an empty tree
		//if the current node has the value we are traversing to, return that node
		//otherwise traverse in the respective left or right direction
		//if that direction is null the tree does not contain the value
		//return current node in that case
		//BigO{logn)
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
		/// return whether the tree contains a value BigO(1)
		/// </summary>
		/// <param name="value">the value to check for</param>
		/// <returns>true if the value is found in the tree, false if not</returns>
		public bool Contains(T value) {
			AVLNode<T> node = Traverse(Root, value);
			if (node == null) { return false; }
			if (node.value.CompareTo(value) == 0) return true;
			return false;
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
		/*
		 *ToArray()
		 *	create queue
		 *	enqueue the root value
		 *	recursily iterate to enqueue all nodes in ToArray(node, queue)
		 *	create array
		 *	loop through until queue is empty
		 *		dequeue value
		 *		append value to array
		 *	return array
		 *	
		 *	ToArray(node, queue)
		 *		if node is null return queue
		 *		try to enqueue nodes left value
		 *		try to enqueue nodes right value
		 *		to array(node.left, queue)
		 *		to array(node.right, queue)
		 *		return queue
		 */
		/// <summary>
		/// generate the tree into a array BigO(n)
		/// </summary>
		/// <returns>a sorted array representing the values in the tree</returns>
		public T[] ToArray() {
			Queue<T> queue = new Queue<T>();
			queue.Enqueue(Root.value);
			queue = ToArray(Root, queue);
			T[] array = new T[0];
			while (queue.Count > 0) {
				T value = queue.Dequeue();
				array = array.Append(value).ToArray();
			}
			return array;
		}

		private Queue<T> ToArray(AVLNode<T> current, Queue<T> queue) {
			if (current == null) return queue;
            if (current.left != null) {
				queue.Enqueue(current.left.value);
            }
			if (current.right != null) {
				queue.Enqueue(current.right.value);
			}
			queue = ToArray(current.left, queue);
			queue = ToArray(current.right, queue);
			return queue;
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


		//bigO(1)
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
			pivot.height += 1;
			topNode.height -= 1;
			if (pivot.left != null) {
				pivot.left.height += 1;
			}
			//		Top node right equal to null
			topNode.left = pivot.right;
			//Pivot left equal to top node
			pivot.right = topNode;
		}

		//bigO(1)
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
			pivot.height += 1;
			topNode.height -= 1;
			if (pivot.right != null) {
				pivot.right.height += 1;
			}
			//		Top node right equal to null
			topNode.right = pivot.left;
			//Pivot left equal to top node
			pivot.left = topNode;
		}

		// big-o: o(1)
		// takes in a parent (parent of unbalanced tree) node and a pivot node (middle value of subtree)
		// rotates a parent child and a pivot child depending on values using right rotation
		// rotates the parent and pivot using left rotation
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

		// big-o: o(1)
		// takes in a parent node (parent is the overall //parent of the unbalanced subtree) and a //pivot node (middle node of the subtree)
		// rotates the bottom half using left rotation
		// then right rotates to finish up
		// would return finished, rotated tree
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
		public int height = 1;
		public int balance = 0;
	}
}
