using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Enqueue three items with different priorities where the highest priority
    // is at the BACK of the queue: Low (1), Mid (3), High (5). Dequeue three times.
    // Expected Result: High, Mid, Low
    // Defect(s) Found: Two defects. (1) The first Dequeue returned Mid instead of High because the
    // search loop stopped at _queue.Count - 1 and never checked the last item in the queue.
    // (2) After fixing the loop, the second Dequeue returned High again because the item was
    // never removed from the queue. Fixed by looping to _queue.Count and calling RemoveAt.
    public void TestPriorityQueue_1()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Low", 1);
        priorityQueue.Enqueue("Mid", 3);
        priorityQueue.Enqueue("High", 5);

        Assert.AreEqual("High", priorityQueue.Dequeue());
        Assert.AreEqual("Mid", priorityQueue.Dequeue());
        Assert.AreEqual("Low", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Enqueue items where several share the highest priority:
    // A (2), B (5), C (5), D (1), E (5). Dequeue five times.
    // Expected Result: B, C, E, A, D (ties are removed in FIFO order, closest to the front first)
    // Defect(s) Found: The first Dequeue returned C instead of B. The comparison used >= so a later
    // item with the same priority replaced the earlier one, breaking FIFO order for ties.
    // Fixed by using > so the item closest to the front is kept.
    public void TestPriorityQueue_2()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 2);
        priorityQueue.Enqueue("B", 5);
        priorityQueue.Enqueue("C", 5);
        priorityQueue.Enqueue("D", 1);
        priorityQueue.Enqueue("E", 5);

        string[] expectedResult = ["B", "C", "E", "A", "D"];

        foreach (var expected in expectedResult)
        {
            Assert.AreEqual(expected, priorityQueue.Dequeue());
        }
    }

    [TestMethod]
    // Scenario: Try to Dequeue from an empty queue.
    // Expected Result: InvalidOperationException with the message "The queue is empty."
    // Defect(s) Found: None. The exception and message were already correct.
    public void TestPriorityQueue_Empty()
    {
        var priorityQueue = new PriorityQueue();

        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Exception should have been thrown.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
        catch (AssertFailedException)
        {
            throw;
        }
        catch (Exception e)
        {
            Assert.Fail(
                 string.Format("Unexpected exception of type {0} caught: {1}",
                                e.GetType(), e.Message)
            );
        }
    }

    [TestMethod]
    // Scenario: Enqueue First (4), Second (9), Third (2) and check the queue contents.
    // Then Dequeue once and check the queue contents again.
    // Expected Result: Items are added to the back in the order they were enqueued:
    // [First (Pri:4), Second (Pri:9), Third (Pri:2)]. Dequeue returns Second and the queue
    // becomes [First (Pri:4), Third (Pri:2)].
    // Defect(s) Found: Enqueue was correct (items go to the back regardless of priority).
    // Dequeue returned Second, but the queue still contained all three items afterwards because
    // the item was never removed. Fixed by calling _queue.RemoveAt(highPriorityIndex).
    public void TestPriorityQueue_EnqueueToBackAndRemove()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("First", 4);
        priorityQueue.Enqueue("Second", 9);
        priorityQueue.Enqueue("Third", 2);

        Assert.AreEqual("[First (Pri:4), Second (Pri:9), Third (Pri:2)]", priorityQueue.ToString());

        Assert.AreEqual("Second", priorityQueue.Dequeue());
        Assert.AreEqual("[First (Pri:4), Third (Pri:2)]", priorityQueue.ToString());
    }

    [TestMethod]
    // Scenario: Enqueue One (3), Two (3), dequeue once, then enqueue Three (7) and Four (3).
    // Dequeue until the queue is empty, then Dequeue one more time.
    // Expected Result: One, Three, Two, Four, then InvalidOperationException "The queue is empty."
    // Defect(s) Found: The first Dequeue returned One only by chance (the loop did not run), and the
    // item was not removed, so later results were wrong and the queue never became empty, which
    // meant the exception was never thrown. Passes after the loop, comparison and removal fixes.
    public void TestPriorityQueue_MixedEnqueueDequeue()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("One", 3);
        priorityQueue.Enqueue("Two", 3);
        Assert.AreEqual("One", priorityQueue.Dequeue());

        priorityQueue.Enqueue("Three", 7);
        priorityQueue.Enqueue("Four", 3);
        Assert.AreEqual("Three", priorityQueue.Dequeue());
        Assert.AreEqual("Two", priorityQueue.Dequeue());
        Assert.AreEqual("Four", priorityQueue.Dequeue());

        Assert.ThrowsException<InvalidOperationException>(() => priorityQueue.Dequeue());
    }
}
