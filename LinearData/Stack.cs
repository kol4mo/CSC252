using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearData {
    public class Stack<T> where T : IComparable<T> {
        public SNode<T> head; // first added
        public SNode<T> tail;// most recent added, next to be removed
        private int count = 0;
        public int Count { get { return count; } }
        /// <summary>
        /// Find and return the value at the specified index
        /// Throws null reference if their is no head
        /// throws index out of bounds if index is out side the range of the queue
        /// BigO(n)
        /// </summary>
        /// <param name="index">the given index</param>
        /// <returns>the value at specified index</returns>
        public T Get(int index) {
            if (tail == null) {
                throw new NullReferenceException();
            }
            if (index >= count || index < 0) {
                throw new IndexOutOfRangeException();
            }
            SNode<T> node = tail;
            for (int i = 0; i < index; i++) {
                node = node.prev;
            }
            return node.value;
        }

        /* Psuedo Code
         *Contains(T value)
         *  Traverse through each value from head to tail
         *      if value equals the given value
         *          return true
         *  return false
         */
        /// <summary>
        /// Contains returns true if the given value is in the queue
        /// BigO(n)
        /// </summary>
        /// <param name="value">The value to be searched for</param>
        /// <returns>Whether or not the queue contains the given value</returns>
        public bool Contains(T value) {
            if (head != null) {
                for (SNode<T> node = head; node != null; node = node.next) {
                    if (node.value.CompareTo(value) == 0) {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// peeks at the next value to be removed without removing the value
        /// Throws null reference if their is no head
        /// BigO(1)
        /// </summary>
        /// <returns>the value of the bottom of the queue</returns>
        public T Peek() {
            if (head != null) {
                return tail.value;
            }
            throw new NullReferenceException();
        }

        /// <summary>
        /// the next item in the queue is removed
        /// null reference error if head is null
        /// BigO(1)
        /// </summary>
        /// <returns>returns the value of the item dequeued</returns>
        public T Pop() {
            if (head == null) {
                throw new NullReferenceException();
            }

            T value = tail.value;
            count--;
            if (tail == head) {
                tail = null;
                head = null;
            } else {
                tail.prev.next = null;
                tail = tail.prev;
            }
            return value;
        }

        /// <summary>
        /// Enqueues a new item at the end
        /// BigO(1)
        /// </summary>
        /// <param name="value">is the value of the new item added</param>
        public void Push(T value) {
            SNode<T> node = new SNode<T>(value);
            count++;
            if (head == null) {
                head = node;
                tail = node;
                return;
            }

            tail.next = node;
            node.prev = tail;
            tail = node;
        }

    }
    public class SNode<T> where T : IComparable<T> {
        public T value;
        public SNode<T> next = null;//above
        public SNode<T> prev = null;//below, closer to head

        public SNode(T value) {
            this.value = value;
        }
    }
}
