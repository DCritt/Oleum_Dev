using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;

public class Node<T>
{

    public T data;
    public Node<T> next;
    public Node<T> prev;

    public Node(T data, Node<T> next, Node<T> prev)
    {

        this.data = data;
        this.next = next;
        this.prev = prev;

    }

}

public class LinkedListStructure<T>
{

    public int size;
    public int maxSize;

    public Node<T> head { get; protected set; }
    public Node<T> tail { get; protected set; }

    public LinkedListStructure()
    {

        head = null;
        tail = null;
        maxSize = -1;
        size = 0;

    }
    public LinkedListStructure(int maxSize)
    {

        head = null;
        tail = null;
        this.maxSize = maxSize;
        size = 0;

    }

    public T this[int index]
    {

        get { return Get(index); }
        set { Set(index, value); }

    }

    public T Get(int index)
    {

        if (index >= size)
        {

            Debug.Log("index out of range");

        }

        Node<T> curr = head;

        for (int i = 0; i < index; i++)
        {

            curr = curr.next;

        }

        return curr.data;

    }
    private Node<T> GetNode(int index)
    {

        if (index >= size)
        {

            Debug.Log("index out of range");
            return null;

        }

        Node<T> curr = head;

        for (int i = 0; i < index; i++)
        {

            curr = curr.next;

        }

        return curr;

    }
    public void Set(int index, T data)
    {

        if (index >= size)
        {

            Debug.Log("index out of range");
            return;

        }

        Node<T> curr = head;

        for (int i = 0; i < index; i++)
        {

            curr = curr.next;

        }

        curr.data = data;

    }
    public void Swap(int index1, int index2)
    {

        if (index1 >= size || index2 >= size || size < 2)
        {

            Debug.Log("index out of range");
            return;

        }

        if (index1 == index2)
        {

            return;

        }

        Node<T> node1 = GetNode(index1);
        Node<T> node2 = GetNode(index2);

        if (index1 == 0)
        {

            head = node2;

        }
        else if (index2 == 0)
        {

            head = node1;

        }

        Node<T> prev1 = node1.prev;
        Node<T> next1 = node1.next;
        node1.prev = node2.prev;
        node2.prev = prev1;
        node1.next = node2.next;
        node2.next = next1;

        if (node1.prev != null)
        {

            node1.prev.next = node1;

        }
        if (node2.prev != null)
        {

            node2.prev.next = node2;

        }

    }
    public int Find(T data)
    {

        Node<T> curr = head as Node<T>;

        for (int i = 0; i < size; i++)
        {

            if (curr.data.Equals(data))
            {

                return i;

            }
            curr = curr.next;

        }

        Debug.Log("value could not be found");
        return -1;


    }
    public bool Contains(T data)
    {

        Node<T> curr = head as Node<T>;

        for (int i = 0; i < size; i++)
        {

            if (curr.data.Equals(data))
            {

                return true;

            }
            curr = curr.next;

        }

        return false;

    }
    public void RemoveAt(int index)
    {

        if (index >= size)
        {

            Debug.Log("index out of range");

        }

        Node<T> curr = head;

        for (int i = 0; i < index; i++)
        {

            curr = curr.next;

        }

        if (index != 0)
        {

            curr.prev.next = curr.next;

        }
        if (index != (size - 1))
        {

            curr.next.prev = curr.prev;

        }

        size -= 1;

        curr.prev = null;
        curr.next = null;

    }

    public void Clear()
    {

        Node<T> curr = head;

        while (curr.next != null)
        {

            curr.prev = null;
            curr = curr.next;
            curr.prev.next = null;

        }

        head = null;
        tail = null;
        size = 0;

    }

    public virtual void Print()
    {



    }

}

public class MyList<T> : LinkedListStructure<T>
{

    public MyList() : base()
    {

        

    }
    public MyList(int maxSize) : base(maxSize)
    {

    }
    public MyList(MyList<T> list)
    {

        Node<T> curr = list.head;

        for (int i = 0; i < list.size; i++)
        {

            InsertLast(curr.data);
            curr = curr.next;

        }

    }

    public void InsertFirst(T data)
    {

        if (maxSize != -1 && size == maxSize)
        {

            Debug.Log("can't exceed maxSize");
            return;

        }

        size += 1;

        if (head == null)
        {

            head = new Node<T>(data, null, null);
            tail = head;
            return;

        }

        Node<T> newNode = new Node<T>(data, head, null);
        head.prev = newNode;
        head = newNode;

    }
    public void InsertLast(T data)
    {

        if (maxSize != -1 && size == maxSize)
        {

            Debug.Log("can't exceed maxSize");
            return;

        }

        size += 1;

        if (head == null)
        {

            head = new Node<T>(data, null, null);
            tail = head;
            return;

        }

        Node<T> newNode = new Node<T>(data, null, tail);
        tail.next = newNode;
        tail = newNode;

    }
    public void InsertAt(T data, int index)
    {

        if (maxSize != -1 && size == maxSize)
        {

            Debug.Log("can't exceed maxSize");
            return;

        }

        if (size == 0)
        {

            size += 1;
            head = new Node<T>(data, null, null);
            tail = head;
            return;

        }

        if (index >= size)
        {

            Debug.Log("index out of range");
            return;

        }

        if (index == (size - 1))
        {

            InsertLast(data);
            return;

        }

        size += 1;

        Node<T> curr = head;

        for (int i = 0; i < index; i++)
        {

            curr = curr.next;

        }

        curr.next = new Node<T>(data, curr.next, curr);
        curr.next.next.prev = curr.next;

    }

    public override void Print()
    {

        base.Print();

        Node<T> curr = head;

        for (int i = 0; i < size; i++)
        {

            Debug.Log("index" + i + " = " + curr.data);
            curr = curr.next;

        }

    }
    

}

public class MyStack<T> : LinkedListStructure<T>
{

    public MyStack() : base()
    {



    }
    public MyStack(int maxSize) : base(maxSize)
    {



    }
    public MyStack(MyStack<T> stack)
    {

        Node<T> curr = stack.tail;

        for (int i = 0; i < stack.size; i++)
        {

            Push(curr.data);
            curr = curr.prev;

        }

    }

    public void Push(T data)
    {

        if (maxSize != -1 && size == maxSize)
        {

            Debug.Log("can't exceed maxSize");
            return;

        }

        size += 1;

        if (head == null)
        {

            head = new Node<T>(data, null, null);
            tail = head;
            return;

        }

        Node<T> newNode = new Node<T>(data, head, null);
        head.prev = newNode;
        head = newNode;

    }
    public T Peek()
    {

        if (size == 0)
        {

            Debug.Log("stack empty");

        }

        return head.data;

    }
    public T Pop()
    {

        if (size == 0)
        {

            Debug.Log("stack empty");

        }

        T data = head.data;
        size -= 1;

        if (size == 0)
        {

            data = head.data;
            head = null;
            tail = null;
            return data;

        }

        head = head.next;
        head.prev = null;
        return data;

    }

    public override void Print()
    {

        base.Print();

        Node<T> curr = head;

        for (int i = 0; i < size; i++)
        {

            Debug.Log("index" + i + " = " + curr.data);
            curr = curr.next;

        }

    }

}

public class MyQueue<T> : LinkedListStructure<T>
{

    public MyQueue() : base()
    {

        

    }
    public MyQueue(int maxSize) : base(maxSize)
    {



    }
    public MyQueue(MyQueue<T> queue)
    {



    }

    public void Enqueue(T data)
    {

        if (maxSize != -1 && size == maxSize)
        {

            Debug.Log("can't exceed maxSize");
            return;

        }

        size += 1;

        if (head == null)
        {

            head = new Node<T>(data, null, null);
            tail = head;
            return;

        }

        Node<T> newNode = new Node<T>(data, head, null);
        head.prev = newNode;
        head = newNode;

    }
    public T Peek()
    {

        if (size == 0)
        {

            Debug.Log("stack empty");

        }

        return tail.data;

    }
    public T Dequeue()
    {

        if (size == 0)
        {

            Debug.Log("queue empty");

        }

        T data = tail.data;
        size -= 1;

        if (size == 0)
        {

            data = tail.data;
            head = null;
            tail = null;
            return data;

        }

        tail = tail.prev;
        tail.next = null;
        return data;

    }
    
    public override void Print()
    {

        base.Print();

        Node<T> curr = tail;

        for (int i = 0; i < size; i++)
        {

            Debug.Log("index" + i + " = " + curr.data);
            curr = tail.prev;

        }

    }

}
