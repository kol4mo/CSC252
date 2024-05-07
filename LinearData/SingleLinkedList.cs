using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LinearData
{
    public class SingleLinkedList<T> where T : IComparable<T> {
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
            Node<T> currentNode = head;
            for (int i = 0; i < index - 1; i++) {
                currentNode = currentNode.next;
            }
            newNode.next = currentNode.next;
            currentNode.next = newNode;
            count++;
        }

        public T Get(int index) {
            if (index < 0 || index >= Count) {
                throw new IndexOutOfRangeException();
            }
            Node<T> currentNode = head;
            for (int i = 0; i < index; i++) {
                currentNode = currentNode.next;
            }
            return currentNode.value;
        }

        //removes the head
        public T Remove() {
            if (head == null) {
                throw new Exception("No Items in list to remove");
            }
            T value = head.value;
            head = head.next;
            if (head == null) {
                tail = null;
            }
            count--;
            return value;
        }

        public T RemoveAt(int index) {
            if (index < 0 || index >= Count) {
                throw new IndexOutOfRangeException();
            }
            if (index == 0) {
                return Remove();
            }

            Node<T> currentNode = head;
            for (int i = 0; i < index - 1; i++) {
                currentNode = currentNode.next;
            }
            T value = currentNode.next.value;
            currentNode.next = currentNode.next.next;

            if (currentNode.next == null) {
                tail.next = currentNode;
            }
            count--;
            return value;
        }

        public T RemoveLast() {
            if (tail == null) {
                throw new Exception("No Items in list to remove");
            }
            Node<T> currentNode = head;
            for (int i = 0; i < Count - 2; i++) {
                currentNode = currentNode.next;
            }
            T valueRemoved = currentNode.next.value;
            currentNode.next = null;
            tail = currentNode;
            count--;
            return valueRemoved;
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

        public override string ToString() {
            if (head == null) {
                return string.Empty;
            }

            Node<T> currentNode = head;
            string values = head.value.ToString() + ", ";
            while (currentNode.next.next != null) {
                currentNode = currentNode.next;
                values += currentNode.value.ToString() + ", ";
            }
            values += currentNode.next.value;
            return values;
        }

        public void Clear() {
            head = null;
            tail = null;
            count = 0;
        }

        //Search
        public int Search(T value) {
            Node<T> currentNode = head;
            for (int i = 0; i < Count - 1; i++) {
                if (currentNode.value.CompareTo(value) == 0) { return i; }
                currentNode = currentNode.next;
                
            }
            return -1;
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
