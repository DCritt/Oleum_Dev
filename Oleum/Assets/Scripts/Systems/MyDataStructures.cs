using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering.Universal;

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

public class PriorityNode<T> : Node<T>
{

    public int priority;

    public PriorityNode(T data, int priority, Node<T> next, Node<T> prev) : base(data, next, prev)
    {

        this.priority = priority;

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

    public void RemoveLast()
    {

        if (size == 0)
        {

            Debug.Log("queue empty");
            return;

        }

        size -= 1;

        if (size == 0)
        {

            head = null;
            tail = null;
            return;

        }

        tail = tail.prev;
        tail.next = null;

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

        Node<T> newNode = new Node<T>(data, null, tail);
        tail.next = newNode;
        tail = newNode;

    }
    public T Peek()
    {

        if (size == 0)
        {

            Debug.Log("stack empty");

        }

        return tail.data;

    }
    public T Pop()
    {

        if (size == 0)
        {

            Debug.Log("stack empty");

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
            curr = curr.prev;

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

        Node<T> curr = queue.tail;

        for (int i = 0; i < queue.size; i++)
        {

            Enqueue(curr.data);
            curr = curr.prev;

        }

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

            Debug.Log("queue empty");

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
            curr = curr.prev;

        }

    }

}

public class MyPriorityQueue<T> : LinkedListStructure<T>
{

    MyList<PriorityNode<T>> priorityList = new MyList<PriorityNode<T>>();

    public MyPriorityQueue() : base()
    {



    }

    public MyPriorityQueue(int maxSize) : base(maxSize)
    {



    }

    public MyPriorityQueue(MyPriorityQueue<T> priorityQueue)
    {

        Node<T> curr = priorityQueue.tail;

        for (int i = 0; i < priorityQueue.size; i++)
        {

            Enqueue(curr.data, (curr as PriorityNode<T>).priority);
            curr = curr.prev;

        }

    }

    public void Enqueue(T data, int priority)
    {

        if (maxSize != -1 && size == maxSize)
        {

            Debug.Log("can't exceed maxSize");
            return;

        }

        size += 1;

        if (head == null)
        {

            head = new PriorityNode<T>(data, priority, null, null);
            tail = head;
            priorityList.InsertFirst(head as PriorityNode<T>);
            return;

        }

        if ((head as PriorityNode<T>).priority >= priority)
        {

            head = new PriorityNode<T>(data, priority, head, null);
            head.next.prev = head;
            
            if ((head.next as PriorityNode<T>).priority > priority)
            {

                priorityList.InsertFirst(head as PriorityNode<T>);

            }
            else
            {

                priorityList[0] = head as PriorityNode<T>;

            }

            return;

        }

        for (int i = 0; i < priorityList.size; i++)
        {

            PriorityNode<T> temp = priorityList[i];

            if (priority == temp.priority)
            {

                temp.prev = new PriorityNode<T>(data, priority, temp, temp.prev);
                temp.prev.prev.next = temp.prev;
                priorityList[i] = temp.prev as PriorityNode<T>;
                return;

            }
            else if (priority < temp.priority)
            {

                temp.prev = new PriorityNode<T>(data, priority, temp, temp.prev);
                temp.prev.prev.next = temp.prev;
                if (i == 0)
                {

                    head = temp.prev;
                    priorityList.InsertFirst(temp.prev as PriorityNode<T>);

                }
                else
                {

                    priorityList.InsertAt(temp.prev as PriorityNode<T>, i - 1);

                }

                return;

            }

        }

        tail.next = new PriorityNode<T>(data, priority, null, tail);
        tail = tail.next;
        priorityList.InsertLast(tail as PriorityNode<T>);
        
    }
    public T Peek()
    {

        if (size == 0)
        {

            Debug.Log("priority queue empty");

        }

        return tail.data;

    }
    public T Dequeue()
    {

        if (size == 0)
        {

            Debug.Log("priority queue empty");

        }

        T data = tail.data;
        size -= 1;

        if ((tail as PriorityNode<T>) == priorityList[priorityList.size - 1])
        {

            priorityList.RemoveLast();

        }

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
            curr = curr.prev;

        }

    }

}

public class MyPriorityStack<T> : LinkedListStructure<T>
{

    MyList<PriorityNode<T>> priorityList = new MyList<PriorityNode<T>>();

    public MyPriorityStack() : base()
    {



    }
    public MyPriorityStack(int maxSize) : base(maxSize)
    {



    }
    public MyPriorityStack(MyPriorityStack<T> priorityStack)
    {



    }


    public void Push(T data, int priority)
    {

        if (maxSize != -1 && size == maxSize)
        {

            Debug.Log("can't exceed maxSize");
            return;

        }

        size += 1;

        if (head == null)
        {

            head = new PriorityNode<T>(data, priority, null, null);
            tail = head;
            priorityList.InsertFirst(head as PriorityNode<T>);
            return;

        }

        if ((head as PriorityNode<T>).priority > priority)
        {

            head = new PriorityNode<T>(data, priority, head, null);
            head.next.prev = head;
            priorityList.InsertFirst(head as PriorityNode<T>);
            return;

        }
        else if ((head as PriorityNode<T>).priority == priority)
        {

            priorityList[0].next = new PriorityNode<T>(data, priority, priorityList[0].next, priorityList[0]);
            priorityList[0] = priorityList[0].next as PriorityNode<T>;

            if (priorityList[0].next != null)
            {

                priorityList[0].next.prev = priorityList[0];

            }
            else
            {

                tail = priorityList[0];

            }

            return;

        }

        for (int i = 0; i < priorityList.size; i++)
        {

            PriorityNode<T> temp = priorityList[i];

            if (priority == temp.priority)
            {

                temp.next = new PriorityNode<T>(data, priority, temp.next, temp);

                if(temp.next.next != null)
                {

                    temp.next.next.prev = temp.next;

                }
                else
                {

                    tail = temp.next;

                }

                priorityList[i] = temp.next as PriorityNode<T>;

                return;

            }
            else if (priority > temp.priority && i == (priorityList.size - 1))
            {

                temp.next = new PriorityNode<T>(data, priority, temp.next, temp);

                if (temp.next.next != null)
                {

                    temp.next.next.prev = temp.next;

                }
                else
                {

                    tail = temp.next;

                }
                priorityList.InsertAt(temp.next as PriorityNode<T>, i);
                return;

            }

        }

    }
    public T Peek()
    {

        if (size == 0)
        {

            Debug.Log("priority stack empty");

        }

        return tail.data;

    }
    public T Pop()
    {

        if (size == 0)
        {

            Debug.Log("priority queue empty");

        }

        T data = tail.data;
        size -= 1;

        if (size == 0)
        {

            priorityList.Clear();
            data = tail.data;
            head = null;
            tail = null;
            return data;

        }

        if ((tail?.prev as PriorityNode<T>)?.priority != (tail as PriorityNode<T>)?.priority)
        {

            priorityList.RemoveLast();

        }
        else
        {

            priorityList[priorityList.size - 1] = priorityList[priorityList.size - 1].prev as PriorityNode<T>;

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
            curr = curr.prev;

        }

    }

}
