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


            return nextNode == null ? node : Traverse(nextNode, value);
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

        public bool Contains(T value) {
            Node<T> node = Traverse(Root, value);
            if (node == null) { return false; }
            if (node.value.CompareTo(value) == 0) return true;
            return false;
        }

        public void Remove(T value) {
            Node<T> node = TraverseParent(Root, value);
            if(node == null) { return; }
            if (node.right.value.CompareTo(value) == 0) {
                Count--;
                Node<T> temp = node.right.left;
                node.right = node.right.right;
                Node<T> temp2 = node.right;
                while (temp2 != null && temp2.left != null) {
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

        public T[] ToArray() {
            T[] array = new T[0];
            array = ToArray(Root, array);
            return array;
        }

        public T[] ToArray(Node<T> node, T[] array) {
            if (node == null) return array;
            array = ToArray(node.left, array);
            array = array.Append(node.value).ToArray();
            array = ToArray(node.right, array);
            return array;
        }
    }

}