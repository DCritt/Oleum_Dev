using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Node<T>
{

    public T data;
    public Node<T> next;

    public Node(T data, Node<T> next)
    {

        this.data = data;
        this.next = next;

    }

}

public class MyList<T>
{

    public int size;
    public int maxSize;

    private Node<T> head;

    public MyList()
    {

        maxSize = -1;
        size = 0;
        head = null;

    }
    public MyList(int maxSize)
    {

        this.maxSize = maxSize;
        size = 0;
        head = null;

    }

    public T this[int index]
    {

        get { return Get(index); }
        set { Set(index, value); }

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

            head = new Node<T>(data, null);
            return;

        }

        Node<T> newNode = new Node<T>(data, head);
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

            head = new Node<T>(data, null);
            return;

        }

        Node<T> curr = head;

        while (curr.next != null)
        {

            curr = curr.next;

        }

        curr.next = new Node<T>(data, null);

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
            head = new Node<T>(data, null);
            return;

        }

        if (index >= size)
        {

            Debug.Log("index out of range");
            return;

        }

        size += 1;

        Node<T> curr = head;
        Node<T> nextNode = curr.next;

        for (int i = 0; i < index; i++)
        {

            curr = nextNode;
            nextNode = curr.next;

        }

        curr.next = new Node<T>(data, nextNode);

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
        Node<T> next1 = node1.next;
        Node<T> next2 = node2.next;

        Debug.Log("node1 = " + node1.data + " node2 = " + node2.data);

        if (index1 == 0)
        {

            Node<T> pred = GetNode(index2 - 1);
            head = node2;
            head.next = next1;
            pred.next = node1;
            node1.next = next2;
            return;

        }

        if (index2 == 0)
        {

            Node<T> pred = GetNode(index1 - 1);
            head = node1;
            head.next = next2;
            pred.next = node2;
            node1.next = next1;
            return;

        }

        Node<T> pred1 = GetNode(index1 - 1);
        Node<T> pred2 = GetNode(index2 - 1);
        pred1.next = node2;
        node2.next = next1;
        pred2.next = node1;
        node1.next = next2;


    }

}
