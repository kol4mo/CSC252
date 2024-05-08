using System;
using System.Collections.Generic;
using System.Linq;
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
            } else if (valueCompared < 0) {
                nextNode = node.right;
            } else {
                nextNode = node.left;
            }


            return nextNode == null ? node : Traverse(nextNode, value);
        }

        //clears the tree and resets the root BigO(1)
        public void Clear() {
            Root = null;
            Count = 0;
        }
    }

}