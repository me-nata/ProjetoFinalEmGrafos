using System.Collections.Generic;

public class Queue<T>{
    List<T> queue;

    public Queue() {
        queue = new List<T>();
    }

    public void push(T item) {
        queue.Add(item);
    }

    public void pop() {
        queue.RemoveAt(0);
    }

    public T first() {
        return queue[0];
    }

    public T last() {
        return queue[queue.Count-1];
    }

    public bool isNotEmpty() {
        return (queue.Count!=0);
    }
}