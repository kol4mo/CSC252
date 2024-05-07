using Microsoft.VisualStudio.TestTools.UnitTesting;
using LinearData;
using System;
using System.Collections;

namespace Testing {
    [TestClass]
    public class LinearUnitTest {
        #region SingleLinkedList
        [TestMethod]
        public void TestSingleAdd_EmptyList() {
            SingleLinkedList<int> list = new SingleLinkedList<int>();
            list.Add(42);

            Assert.IsTrue(list.head != null, "append failed, tail 42 expected value to be 42 but tail was null");
            Assert.IsTrue(list.head.value == 42, "append failed, tail was not equal to 42 was " + list.head.value);
            Assert.IsTrue(list.Count == 1, "append failed, Count was not Accurate");
        }

        [TestMethod]
        public void TestSingleAdd_NonEmptyList() {
            int expectedValue = 404;
            SingleLinkedList<int> list = new SingleLinkedList<int>();
            list.Add(42);
            list.Add(expectedValue);

            //Assert.IsTrue(list.head != null, "append failed, tail 42 expected value to be 42 but tail was null");
            Assert.IsTrue(list.head.next != null, "append failed, expected head have next but was null");

            Assert.IsTrue(list.head.next.value == expectedValue, "Append failed, tail was not equal to " + expectedValue + " was " + list.head.next.value);

        }

        [TestMethod]
        public void TestSingleInsert_HappyPath() {
            SingleLinkedList<int> list = new SingleLinkedList<int>();
            list.Add(42);
            list.Add(42);
            list.Insert(404, 1);

            Assert.IsTrue(list.head.next.value == 404, "Insert failed, expected 404 at index 1");
        }

        [TestMethod]
        public void TestSingleInsert_SingleList() {

            SingleLinkedList<int> list = new SingleLinkedList<int>();
            list.Add(42);
            list.Insert(404, 0);

            Assert.IsTrue(list.head.value == 404, "Insert failed, expected 404 at index 1");
        }

        [TestMethod]
        public void TestSingleGet_HappyPath() {
            int input = 404;
            SingleLinkedList<int> list = new SingleLinkedList<int>();
            list.Add(42);
            list.Add(input);
            int output = list.Get(1);

            Assert.IsTrue(output == input, "Get Failed, expected " + input + " at index 1, recieved " + output);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void TestSingleGet_EmptyList() {
            SingleLinkedList<int> list = new SingleLinkedList<int>();
            int output = list.Get(0);
        }

        [TestMethod]
        public void TestSingleRemove_HappyPath() {
            int input = 42;
            SingleLinkedList<int> list = new SingleLinkedList<int>();
            list.Add(input);
            int output = list.Remove();

            Assert.IsTrue(input == output, "Remove failed, expected return value " + input + ", returned " + output);
            Assert.IsTrue(list.head == null, "Remove failed, expected head to be empty but was not");
            Assert.IsTrue(list.tail == null, "Remove failed, expected tail to be empty but was not");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestSingleRemove_EmptyList() {
            SingleLinkedList<int> list = new SingleLinkedList<int>();
            list.Remove();

        }

        //RemoveAt
        [TestMethod]
        public void TestSingleRemoveAt_HappyPath() {
            int input = 42;
            SingleLinkedList<int> list = new SingleLinkedList<int>();
            list.Add(input);
            list.Add(404);
            list.Add(input);
            int output = list.RemoveAt(1);

            Assert.IsTrue(output == 404, "RemoveAt failed, expected return value of " + 404 + " but returned " + output);
            Assert.IsTrue(list.head.value == input, "RemoveAt failed, expected value of " + input + ", got " + list.head.value);
            Assert.IsTrue(list.head.next.value == input, "RemoveAt failed, expected value of " + input + ", got " + list.head.value);
            Assert.IsTrue(list.head.next.next == null, "RemoveAt failed, expected only 2 node, but currently has more");

        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void TestSingleRemoveAt_EmptyList() {
            SingleLinkedList<int> list = new SingleLinkedList<int>();
            list.RemoveAt(1);
        }

        //RemoveLast
        [TestMethod]
        public void TestSingleRemoveLast_HappyPath() {
            int input = 42;
            SingleLinkedList<int> list = new SingleLinkedList<int>();
            list.Add(404);
            list.Add(input);
            int output = list.RemoveLast();

            Assert.IsTrue(input == output, "RemoveAt failed, expected return value of " + input + ", but got a return value of " + output);
            Assert.IsTrue(list.head.next == null, "RemoveLast failed, expected only one node");
            Assert.IsTrue(list.head == list.tail, "RemoveLast failed, expected head and tail to be the same");
            Assert.IsTrue(list.head.value != input, "RemoveLast failed, expected head to not be the input value");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestSingleRemoveLast_EmptyList() {
            SingleLinkedList<int> list = new SingleLinkedList<int>();
            list.RemoveLast();
        }

        //ToString
        [TestMethod]
        public void TestSingleToString_HappyPath() {
            SingleLinkedList<int> list = new SingleLinkedList<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);
            list.Add(5);

            string output = list.ToString();
            string expectedOutput = "1, 2, 3, 4, 5";

            Assert.IsTrue(output == expectedOutput, "ToString failed, expected '" + expectedOutput + "' but returned '" + output + "'");
        }

        [TestMethod]
        public void TestSingleToString_Empty() {
            SingleLinkedList<int> list = new SingleLinkedList<int>();
            string output = list.ToString();
            string expectedOutput = "";
            Assert.IsTrue(output == expectedOutput, "ToString failed, expected '" + expectedOutput + "' but returned '" + output + "'");
        }

        //Clear
        [TestMethod]
        public void TestSingleClear_HappyPath() {
            SingleLinkedList<int> list = new SingleLinkedList<int>();
            list.Add(404);
            list.Add(404);

            list.Clear();

            Assert.IsTrue(list.head == null, "Clear failed, head was expected to be null but was not");
            Assert.IsTrue(list.tail == null, "Clear failed, tail was expected to be null but was not");
            Assert.IsTrue(list.Count == 0, "Clear failed, Count was expected to be 0 but was " + list.Count);
        }

        [TestMethod]
        public void TestSingleClear_EmptyList() {
            SingleLinkedList<int> list = new SingleLinkedList<int>();
            list.Clear();

            Assert.IsTrue(list.head == null, "Clear failed, head was expected to be null but was not");
            Assert.IsTrue(list.tail == null, "Clear failed, tail was expected to be null but was not");
            Assert.IsTrue(list.Count == 0, "Clear failed, Count was expected to be 0 but was " + list.Count);
        }

        //Search
        [TestMethod]
        public void TestSingleSearch_HappyPath() {
            SingleLinkedList<int> list = new SingleLinkedList<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);
            list.Add(5);
            int output = list.Search(4);
            int expectedOuput = 3;

            Assert.IsTrue(output == expectedOuput, "SearchFailed, expected output to return " + expectedOuput + ", but returned " + output);
        }

        [TestMethod]
        public void TestSingleSearch_ValueNotInList() {
            SingleLinkedList<int> list = new SingleLinkedList<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);
            int output = list.Search(5);
            int expectedOuput = -1;

            Assert.IsTrue(output == expectedOuput, "SearchFailed, expected output to return " + expectedOuput + ", but returned " + output);
        }

        #endregion

        #region DoubleLinkedList
        [TestMethod]
        public void TestDoubleAdd_EmptyList() {
            DoubleLinkedList<int> list = new DoubleLinkedList<int>();
            list.Add(42);

            Assert.IsTrue(list.head != null, "append failed, tail 42 expected value to be 42 but tail was null");
            Assert.IsTrue(list.head.value == 42, "append failed, tail was not equal to 42 was " + list.head.value);
            Assert.IsTrue(list.Count == 1, "append failed, Count was not Accurate");
        }

        [TestMethod]
        public void TestDoubleAdd_NonEmptyList() {
            int expectedValue = 404;
            DoubleLinkedList<int> list = new DoubleLinkedList<int>();
            list.Add(42);
            list.Add(expectedValue);

            //Assert.IsTrue(list.head != null, "append failed, tail 42 expected value to be 42 but tail was null");
            Assert.IsTrue(list.head.next != null, "append failed, expected head have next but was null");

            Assert.IsTrue(list.head.next.value == expectedValue, "Append failed, tail was not equal to " + expectedValue + " was " + list.head.next.value);

        }

        [TestMethod]
        public void TestDoubleInsert_HappyPath() {
            DoubleLinkedList<int> list = new DoubleLinkedList<int>();
            list.Add(42);
            list.Add(42);
            list.Insert(404, 1);

            Assert.IsTrue(list.head.next.value == 404, "Insert failed, expected 404 at index 1");
        }

        [TestMethod]
        public void TestDoubleInsert_DoubleList() {

            DoubleLinkedList<int> list = new DoubleLinkedList<int>();
            list.Add(42);
            list.Insert(404, 0);

            Assert.IsTrue(list.head.value == 404, "Insert failed, expected 404 at index 1");
        }

        [TestMethod]
        public void TestDoubleGet_HappyPath() {
            int input = 404;
            DoubleLinkedList<int> list = new DoubleLinkedList<int>();
            list.Add(42);
            list.Add(input);
            int output = list.Get(1);

            Assert.IsTrue(output == input, "Get Failed, expected " + input + " at index 1, recieved " + output);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void TestDoubleGet_EmptyList() {
            DoubleLinkedList<int> list = new DoubleLinkedList<int>();
            int output = list.Get(0);
        }

        [TestMethod]
        public void TestDoubleRemove_HappyPath() {
            int input = 42;
            DoubleLinkedList<int> list = new DoubleLinkedList<int>();
            list.Add(input);
            int output = list.Remove();

            Assert.IsTrue(input == output, "Remove failed, expected return value " + input + ", returned " + output);
            Assert.IsTrue(list.head == null, "Remove failed, expected head to be empty but was not");
            Assert.IsTrue(list.tail == null, "Remove failed, expected tail to be empty but was not");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestDoubleRemove_EmptyList() {
            DoubleLinkedList<int> list = new DoubleLinkedList<int>();
            list.Remove();

        }

        //RemoveAt
        [TestMethod]
        public void TestDoubleRemoveAt_HappyPath() {
            int input = 42;
            DoubleLinkedList<int> list = new DoubleLinkedList<int>();
            list.Add(input);
            list.Add(404);
            list.Add(input);
            int output = list.RemoveAt(1);

            Assert.IsTrue(output == 404, "RemoveAt failed, expected return value of " + 404 + " but returned " + output);
            Assert.IsTrue(list.head.value == input, "RemoveAt failed, expected value of " + input + ", got " + list.head.value);
            Assert.IsTrue(list.head.next.value == input, "RemoveAt failed, expected value of " + input + ", got " + list.head.value);
            Assert.IsTrue(list.head.next.next == null, "RemoveAt failed, expected only 2 node, but currently has more");

        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void TestDoubleRemoveAt_EmptyList() {
            DoubleLinkedList<int> list = new DoubleLinkedList<int>();
            list.RemoveAt(1);
        }

        //RemoveLast
        [TestMethod]
        public void TestDoubleRemoveLast_HappyPath() {
            int input = 42;
            DoubleLinkedList<int> list = new DoubleLinkedList<int>();
            list.Add(404);
            list.Add(input);
            int output = list.RemoveLast();

            Assert.IsTrue(input == output, "RemoveAt failed, expected return value of " + input + ", but got a return value of " + output);
            Assert.IsTrue(list.head.next == null, "RemoveLast failed, expected only one node");
            Assert.IsTrue(list.head == list.tail, "RemoveLast failed, expected head and tail to be the same");
            Assert.IsTrue(list.head.value != input, "RemoveLast failed, expected head to not be the input value");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestDoubleRemoveLast_EmptyList() {
            DoubleLinkedList<int> list = new DoubleLinkedList<int>();
            list.RemoveLast();
        }

        //ToString
        [TestMethod]
        public void TestDoubleToString_HappyPath() {
            DoubleLinkedList<int> list = new DoubleLinkedList<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);
            list.Add(5);

            string output = list.ToString();
            string expectedOutput = "1, 2, 3, 4, 5";

            Assert.IsTrue(output == expectedOutput, "ToString failed, expected '" + expectedOutput + "' but returned '" + output + "'");
        }

        [TestMethod]
        public void TestDoubleToString_Empty() {
            DoubleLinkedList<int> list = new DoubleLinkedList<int>();
            string output = list.ToString();
            string expectedOutput = "";
            Assert.IsTrue(output == expectedOutput, "ToString failed, expected '" + expectedOutput + "' but returned '" + output + "'");
        }

        //Clear
        [TestMethod]
        public void TestDoubleClear_HappyPath() {
            DoubleLinkedList<int> list = new DoubleLinkedList<int>();
            list.Add(404);
            list.Add(404);

            list.Clear();

            Assert.IsTrue(list.head == null, "Clear failed, head was expected to be null but was not");
            Assert.IsTrue(list.tail == null, "Clear failed, tail was expected to be null but was not");
            Assert.IsTrue(list.Count == 0, "Clear failed, Count was expected to be 0 but was " + list.Count);
        }

        [TestMethod]
        public void TestDoubleClear_EmptyList() {
            DoubleLinkedList<int> list = new DoubleLinkedList<int>();
            list.Clear();

            Assert.IsTrue(list.head == null, "Clear failed, head was expected to be null but was not");
            Assert.IsTrue(list.tail == null, "Clear failed, tail was expected to be null but was not");
            Assert.IsTrue(list.Count == 0, "Clear failed, Count was expected to be 0 but was " + list.Count);
        }

        //Search
        [TestMethod]
        public void TestDoubleSearch_HappyPath() {
            DoubleLinkedList<int> list = new DoubleLinkedList<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);
            list.Add(5);
            int output = list.Search(4);
            int expectedOuput = 3;

            Assert.IsTrue(output == expectedOuput, "SearchFailed, expected output to return " + expectedOuput + ", but returned " + output);
        }

        [TestMethod]
        public void TestDoubleSearch_ValueNotInList() {
            DoubleLinkedList<int> list = new DoubleLinkedList<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);
            int output = list.Search(5);
            int expectedOuput = -1;

            Assert.IsTrue(output == expectedOuput, "SearchFailed, expected output to return " + expectedOuput + ", but returned " + output);
        }

        #endregion

        #region Queue
        [TestMethod]
        public void Enqueue_happyPath() {
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(1);
            queue.Enqueue(2);

            Assert.IsTrue(queue.Count == 2, "Enqueue failed, count did not equal expected count");
            Assert.IsTrue(queue.head.value == 1, "Enqueue failed, head value expected to be 1 but was " + queue.head.value);
            Assert.IsTrue(queue.tail.value == 2, "Enqueue failed, tail value expected to be 2 but was " + queue.tail.value);
        }

        [TestMethod]
        public void Enqueue_SingleValue() {
            int input = 1;
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(input);

            Assert.IsTrue(queue.head.value == input);
            Assert.IsTrue(queue.tail.value == input);
        }

        [TestMethod]
        public void Dequeue_happyPath() {
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(1);
            queue.Enqueue(2);
            int output1 = queue.Dequeue();
            int output2 = queue.Dequeue();

            Assert.IsTrue(queue.Count == 0, "Dequeue failed, count did not equal expected count");
            Assert.IsTrue(queue.head == null, "Dequeue failed, head was not null");
            Assert.IsTrue(queue.tail == null, "Dequeue failed, tail was not null");
            Assert.IsTrue(output1 == 1, "Dequeue failed, did not receive 1 from first output");
            Assert.IsTrue(output2 == 2, "Dequeue failed, did not receive 2 from second output");
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void Dequeue_Empty() {
            Queue<int> queue = new Queue<int>();
            queue.Dequeue();
        }

        [TestMethod]
        public void QueuePeek_HappyPath() {
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(1);
            queue.Enqueue(2);
            int output = queue.Peek();

            Assert.IsTrue(output == 1, "Peek failed, not expected output");
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void QueuePeek_Empty() {
            Queue<int> queue = new Queue<int>();
            queue.Peek();
        }

        [TestMethod]
        public void QueueContains_HappyPath() {
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(1);
            queue.Enqueue(2);
            bool output = queue.Contains(2);
            bool output2 = queue.Contains(3);

            Assert.IsTrue(output == true, "Contains failed, could not find 2 in queue");
            Assert.IsTrue(output2 == false, "Contains failed, found 3 in queue when not their");
        }

        [TestMethod]
        public void QueueContains_Empty() {
            Queue<int> queue = new Queue<int>();
            bool output = queue.Contains(2);

            Assert.IsTrue(output == false, "Contains failed");
        }

        [TestMethod]
        public void QueueGet_HappyPath() {
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(1);
            queue.Enqueue(2);
            int output1 = queue.Get(0);
            int output2 = queue.Get(1);

            Assert.IsTrue(output1 == 1, "Get failed, did not recieve 1 for first output");
            Assert.IsTrue(output2 == 2, "Get failed, did not recieve 2 for second output");
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void QueueGet_OutOfRange() {
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(1);
            queue.Enqueue(2);
            int output1 = queue.Get(3);
        }
        #endregion

        #region Stack
        [TestMethod]
        public void Push_happyPath() {
            Stack<int> Stack = new Stack<int>();
            Stack.Push(1);
            Stack.Push(2);

            Assert.IsTrue(Stack.Count == 2, "Push failed, count did not equal expected count");
            Assert.IsTrue(Stack.head.value == 1, "Push failed, head value expected to be 1 but was " + Stack.head.value);
            Assert.IsTrue(Stack.tail.value == 2, "Push failed, tail value expected to be 2 but was " + Stack.tail.value);
        }

        [TestMethod]
        public void Push_SingleValue() {
            int input = 1;
            Stack<int> Stack = new Stack<int>();
            Stack.Push(input);

            Assert.IsTrue(Stack.head.value == input);
            Assert.IsTrue(Stack.tail.value == input);
        }

        [TestMethod]
        public void Pop_happyPath() {
            Stack<int> Stack = new Stack<int>();
            Stack.Push(1);
            Stack.Push(2);
            int output1 = Stack.Pop();
            int output2 = Stack.Pop();

            Assert.IsTrue(Stack.Count == 0, "Pop failed, count did not equal expected count");
            Assert.IsTrue(Stack.head == null, "Pop failed, head was not null");
            Assert.IsTrue(Stack.tail == null, "Pop failed, tail was not null");
            Assert.IsTrue(output1 == 2, "Pop failed, did not receive 2 from first output");
            Assert.IsTrue(output2 == 1, "Pop failed, did not receive 1 from second output");
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void Pop_Empty() {
            Stack<int> Stack = new Stack<int>();
            Stack.Pop();
        }

        [TestMethod]
        public void StackPeek_HappyPath() {
            Stack<int> Stack = new Stack<int>();
            Stack.Push(1);
            Stack.Push(2);
            int output = Stack.Peek();

            Assert.IsTrue(output == 2, "Peek failed, not expected output");
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void StackPeek_Empty() {
            Stack<int> Stack = new Stack<int>();
            Stack.Peek();
        }

        [TestMethod]
        public void StackContains_HappyPath() {
            Stack<int> Stack = new Stack<int>();
            Stack.Push(1);
            Stack.Push(2);
            bool output = Stack.Contains(2);
            bool output2 = Stack.Contains(3);

            Assert.IsTrue(output == true, "Contains failed, could not find 2 in Stack");
            Assert.IsTrue(output2 == false, "Contains failed, found 3 in Stack when not their");
        }

        [TestMethod]
        public void StackContains_Empty() {
            Stack<int> Stack = new Stack<int>();
            bool output = Stack.Contains(2);

            Assert.IsTrue(output == false, "Contains failed");
        }

        [TestMethod]
        public void StackGet_HappyPath() {
            Stack<int> Stack = new Stack<int>();
            Stack.Push(1);
            Stack.Push(2);
            int output1 = Stack.Get(0);
            int output2 = Stack.Get(1);

            Assert.IsTrue(output1 == 2, "Get failed, did not recieve 2 for first output");
            Assert.IsTrue(output2 == 1, "Get failed, did not recieve 1 for second output");
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void StackGet_OutOfRange() {
            Stack<int> Stack = new Stack<int>();
            Stack.Push(1);
            Stack.Push(2);
            int output1 = Stack.Get(3);
        }
        #endregion
    }
}