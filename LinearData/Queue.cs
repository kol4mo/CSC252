using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearData {
    public class Queue<T> where T : IComparable<T> {
        public QNode<T> head; // the next to be removed, first in I considered this the bottom of the queue
        public QNode<T> tail;// most recent add
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
            if (head == null) {
                throw new NullReferenceException();
            }
            if (index >= count || index < 0) {
                throw new IndexOutOfRangeException();
            }
            QNode<T> node = head;
            for (int i = 0; i < index; i++) {
                node = node.next;
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
                for (QNode<T> node = head; node != null; node = node.next) {
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
                return head.value;
            }
            throw new NullReferenceException();
        }

        /// <summary>
        /// the next item in the queue is removed
        /// null reference error if head is null
        /// BigO(1)
        /// </summary>
        /// <returns>returns the value of the item dequeued</returns>
        public T Dequeue() {
            if (head == null) {
                throw new NullReferenceException();
            }
            T value = head.value;
            count--;
            //remove head
            if (head == tail) {
                tail = null;
                head = null;
            } else {
                head.next.prev = null;
                head = head.next;
            }
            return value;
        }

        /// <summary>
        /// Enqueues a new item at the end
        /// BigO(1)
        /// </summary>
        /// <param name="value">is the value of the new item added</param>
        public void Enqueue(T value) {
            QNode<T> node = new QNode<T>(value);
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
    public class QNode<T> where T : IComparable<T> {
        public T value;
        public QNode<T> next = null;//above
        public QNode<T> prev = null;//below, closer to head

        public QNode(T value) {
            this.value = value;
        }
    }
}
