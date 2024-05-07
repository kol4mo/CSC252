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
    public class DoubleLinkedList<T> where T : IComparable<T> {
        public DoubleNode<T> head;
        public DoubleNode<T> tail;
        private int count = 0;
        public int Count { get { return count; } }

        //addes a DoubleNode with the give value to the end of the list
        public void Add(T value) {// O(1)
            DoubleNode<T> node = new DoubleNode<T>(value);
            if (head == null) {
                head = node;
                tail = node;
                count++;
                return;
            }
            tail.next = node;
            node.prev = tail;
            tail = node;
            count++;
        }

        //inserts a value for a DoubleNode after the given DoubleNode
        public void Insert(T value, int index) {
            DoubleNode<T> newNode = new DoubleNode<T>(value);
            if (index < 0 || index >= Count) {
                throw new IndexOutOfRangeException();
            }
            if (index == 0) {
                newNode.next = head;
                head = newNode;
                count++;
                return;
            }
            DoubleNode<T> currentNode = head;
            for (int i = 0; i < index - 1; i++) {
                currentNode = currentNode.next;
            }
            newNode.next = currentNode.next;
            currentNode.next = newNode;
            newNode.prev = currentNode;
            newNode.next.prev = newNode;
            count++;
        }

        public T Get(int index) {
            if (index < 0 || index >= Count) {
                throw new IndexOutOfRangeException();
            }
            DoubleNode<T> currentNode = head;
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
            } else {
                head.prev = null;
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

            DoubleNode<T> currentNode = head;
            for (int i = 0; i < index - 1; i++) {
                currentNode = currentNode.next;
            }
            T value = currentNode.next.value;
            currentNode.next = currentNode.next.next;
            if (currentNode.next == null) {
                tail.next = currentNode;
            } else {
                currentNode.next.prev = currentNode;
            }
            count--;
            return value;
        }

        public T RemoveLast() {
            if (tail == null) {
                throw new Exception("No Items in list to remove");
            }
            T value = tail.value;
            tail = tail.prev;
            if (tail == null) {
                head = null;
            } else {
                tail.next = null;
            }
            count--;
            return value;
        }


        public override string ToString() {
            if (head == null) {
                return string.Empty;
            }

            DoubleNode<T> currentNode = head;
            string values = head.value.ToString() + ", ";
            while (currentNode.next.next != null) {
                currentNode = currentNode.next;
                values += currentNode.value.ToString() + ", ";
            }
            values += currentNode.next.value;
            return values;
        }

        public void Clear() {
            while (head != null) {
                Remove();
            }
        }

        //Search
        public int Search(T value) {
            DoubleNode<T> currentNode = head;
            for (int i = 0; i < Count - 1; i++) {
                if (currentNode.value.CompareTo(value) == 0) { return i; }
                currentNode = currentNode.next;
                
            }
            return -1;
        }

    }

    public class DoubleNode<T> where T : IComparable<T> {
        public T value;
        public DoubleNode<T> next = null;
        public DoubleNode<T> prev = null;

        public DoubleNode(T value) {
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
