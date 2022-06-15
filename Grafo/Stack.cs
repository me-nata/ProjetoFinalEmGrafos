using System.Collections.Generic;

public class Stack<T>{
    List<T> stack;

    public Stack() {
        stack = new List<T>();
    }

    public void push(T item) {
        stack.Add(item);
    }

    public void pop() {
        stack.RemoveAt(stack.Count-1);
    }

    public T get() {
        return stack[stack.Count-1];
    }

    public bool isNotEmpty() {
        return !(stack.Count==0);
    }
}