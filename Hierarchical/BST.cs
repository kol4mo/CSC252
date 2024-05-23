using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Hierarchical {
    public class BST<T> where T : IComparable<T> {
        public Node<T> Root { get; set; }
        public int Count { get; set; }

        //will not add duplicate nodes BigO(1)
        public void Add(T value) {
            Node<T> newNode = new Node<T>(value);
            if (Root == null) {
                Root = newNode;
                Count++;
                return;
            }

            Node<T> currentNode = Traverse(Root, value);
            int valueCompared = currentNode.value.CompareTo(value);
            if (valueCompared == 0) {
                
            } else if (currentNode.value.CompareTo(value) < 0) {
                currentNode.right = newNode;
                Count++;
            } else {
                currentNode.left = newNode;
                Count++;
            }
        }

        //recursive method that traverses down the tree
        //returns a node for the value provided
        //if null is provided return null which assmes an empty tree
        //if the current node has the value we are traversing to, return that node
        //otherwise traverse in the respective left or right direction
        //if that direction is null the tree does not contain the value
        //return current node in that case
        //BigO{logn)
        private Node<T> Traverse(Node<T> node, T value) {
            if (node == null) {
                return null;
            }
            Node<T> nextNode;
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
        private Node<T> TraverseParent(Node<T> node, T value) {
            if (node == null) {
                return null;
            }
            Node<T> nextNode;
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
            Node<T> node = Traverse(Root, value);
            if (node == null) { return false; }
            if (node.value.CompareTo(value) == 0) return true;
            return false;
        }

        /*
         *Remove(value) {
         *  traverse to the node to be removed, call traverse
         *  traverse to the parent of the rightmost leaf of the left subtree
         *  lead = node.left
         *  while node.right.right != null {
         *      node = node.right
         *  }
         *  if Node.right == null { // left subtree 
         *  node to remove value = node.value
         *  node to remove. left = node.left
         *  } else {
         *  node to remove value = node.right.value
         *  node.right = node.right.left
         *  }
         *  parent of rightmostleaf. right  = rightmostleaf.left
         *}
         *
         *Remove(value)
         *  starting at the root
         *      is the root of the node were removing
         *          if yes remove the root
         *      otherwise traverse the tree
         *      is my child(left or right the node to remove
         *          is my childs left null?
         *              if yes then make my child my child's right
         *          otherwise find the right most node of my childs left subtree
         *              my childs right subtree becomes the right subtree of this node
         *              my child is my childs left
         */

        /// <summary>
        /// removes a node from a tree bigO((logn)^2)
        /// </summary>
        /// <param name="value">the value of the node to attempt to remove</param>
        public void Remove(T value) {
            if (Root == null) { return; }
            if (Root.value.CompareTo(value) == 0) {
                Count--;
                Node<T> temp = Root.left;
                Root = Root.right;
                Node<T> temp2 = Root;
                while (temp2 != null && temp2.left != null) {//log n
                    temp2 = temp2.left;
                }
                if (temp2 == null) {
                    temp2 = temp;
                } else {
                    temp2.left = temp;
                }
                return;
            }
            Node<T> node = TraverseParent(Root, value); //logn
            if(node == null) { return; }
            if (node.right.value.CompareTo(value) == 0) {
                Count--;
                Node<T> temp = node.right.left;
                node.right = node.right.right;
                Node<T> temp2 = node.right;
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
                Node<T> temp = node.left.left;
                node.left = node.left.right;
                Node<T> temp2 = node.left;
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

        /// <summary>
        /// find the height of the tree BigO(n)
        /// </summary>
        /// <returns>an integer value repesenting the height of the tree</returns>
        public int Height() {
            if (Root == null) return 0;

            return HeightR(Root, 1);
        }

        public int HeightR(Node<T> node, int height) {
            if (node.left != null) {
                int leftHeight = HeightR(node.left, height + 1);
                if (leftHeight > height) {
                    height = leftHeight;
                }
            }
            if (node.right != null)
            {
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
            array = ToArray(Root, array);
            return array;
        }

        private T[] ToArray(Node<T> node, T[] array) {
            if (node == null) return array;
            array = ToArray(node.left, array);
            array = array.Append(node.value).ToArray();
            array = ToArray(node.right, array);
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

        private string InOrder(Node<T> node, string final) {
            if (node == null    ) return final;
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

        private string PreOrder(Node<T> node, string final) {
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

        private string PostOrder(Node<T> node, string final) {
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
    }

}