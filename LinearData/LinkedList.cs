using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LinearData
{
    public class LinkedList<T> where T : IComparable<T> {
        public Node<T> head;
        public Node<T> tail;
        private int count = 0;
        public int Count { get { return count; } }

        //addes a node with the give value to the end of the list
        public void Add(T value) {// O(1)
            Node<T> node = new Node<T>(value);
            if (head == null) {
                head = node;
                tail = node;
                count++;
                return;
            }
            tail.next = node;
            tail = node;
            count++;
        }

        //inserts a value for a node after the given node
        public void Insert(T value, int index) {
            Node<T> newNode = new Node<T>(value);
            if (index < 0 || index >= Count) {
                throw new IndexOutOfRangeException();
            }
            if (index == 0) {
                newNode.next = head;
                head = newNode;
                count++;
                return;
            }
            int current = 0;
            Node<T> currentNode = head;
            while (currentNode != null && current != index - 1) {
                currentNode = currentNode.next;
                current++;
            }
            newNode.next = currentNode.next;
            currentNode.next = newNode;
            count++;
        }

        //removes a all nodes with value
        public void RemoveAll(T value) { //O(n)
            Node<T> currentNode = head;
            while (currentNode != null && currentNode.value.CompareTo(value) == 0) {
                head = head.next;
                currentNode = head;
                count--;
            }
            if (currentNode == null) {
                return;
            }
            while (currentNode != null) {
                while (currentNode.next != null && currentNode.next.value.CompareTo(value) == 0) {
                    currentNode.next = currentNode.next.next;
                    count--;
                }
                if (currentNode.next == null) {
                    tail = currentNode;
                }
                currentNode = currentNode.next;
            }
        }

        //Search
        public Node<T> Search(T value) {
            return null;
        }

    }

    public class Node<T> where T : IComparable<T> {
        public T value;
        public Node<T> next = null;

        public Node(T value) {
            this.value = value;
        }
        //search(searchValue)
            //if value = searchValue
                //return this
            //if next
                //return next.search(searchValue)
            //return null
    }
}
