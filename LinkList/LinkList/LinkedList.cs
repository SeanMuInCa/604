using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
namespace LinkedList
{
    public class LinkedList<T> : A_List<T> where T : IComparable<T>
    {
        private Node head;

        public override IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(this);
        }
        /*public override void Insert(int index, T data)
        {
            if (head == null)
            {
                throw new InvalidOperationException("LinkedList is empty.");
            }

            if (index < 0 || index >= Count)
            {
                throw new ArgumentOutOfRangeException("Invalid index." + index);
            }
            Node newNode = new Node(data);
            if (index == 0)
            {
                newNode.next = head;
                head = newNode;
                return;
            }
            Node curNode = head;
            for (int i = 0; i < index - 1; i++)
            {
                if (curNode == null)
                {
                    throw new InvalidOperationException("Index out of range.");
                }
                curNode = curNode.next;
            }
            newNode.next = curNode.next;
            curNode.next = newNode;
        }*/

        /*public override void Insert(int index, T data)
        {
            if (index < 0 || index >= Count)
            {
                throw new IndexOutOfRangeException("Invalid index " + index);
            }
            if (index == 0)
            {
                Node newNode = new Node(data);
                newNode.next = head;
                head = newNode;
            }
            else
            {
                int i = 0;
                Node cur = head;
                Node pre = null;
                while (i < index)
                {
                    pre = cur;
                    cur = cur.next;
                    i++;
                }
                Node newNode = new Node(data);
                pre.next = newNode;
                newNode.next = cur;
            }
        }*/
        //version rec
        public override void Insert(int index, T data)
        {
            if (index < 0 || index >= Count)
            {
                throw new IndexOutOfRangeException("Invalid index " + index);
            }
            head = recInsert(index, head, data);
        }
        private Node recInsert(int index, Node current, T data)
        {
            if (index == 0)
            {
                Node newNode = new Node(data);
                newNode.next = current;
                current = newNode;
            }
            else
            {
                current.next = recInsert(--index, current.next, data);
            }
            return current;
        }

        /* public override void Add(T data)
         {
             Node newNode = new Node(data);
             if (head == null) head = newNode;
             else
             {
                 Node lastNode = head;
                 while (lastNode.next != null)
                 {
                     lastNode = lastNode.next;
                 }
                 lastNode.next = newNode;
             }
         }*/


        /* // * version 1
          public override void Add(T data)
         {
             head = recAdd(head, data);
         }

         private Node recAdd(Node cur, T data)
         {
             if (cur == null) cur = new Node(data);
             else cur.next = recAdd(cur.next, data);
             return cur;
         }*/

        /* //version 2
         public override void Add(T data)
         {
             if (head == null)
                 head = new Node(data);
             else recAdd(head, data);
         }
         private void recAdd(Node cur, T data)
         {
             if (cur == null)
                 cur = new Node(data);
             recAdd(cur.next, data);
         }*/

        //version 3
        public override void Add(T data)
        {
            recAdd(ref head, data);
        }
        private void recAdd(ref Node cur, T data)
        {
            if (cur == null)
            {
                cur = new Node(data);
            }
            else
            {
                recAdd(ref cur.next, data);
            }
        }

        /*public override bool Remove(T data)
        {
            if (head == null) return false;
            bool flag = false;
            Node curNode = head;
            if (head.data.Equals(data))
            {
                head = head.next;
                flag = true;
            }
            while (curNode.next != null)
            {
                if (curNode.next.data.Equals(data)) 
                {
                    curNode.next = curNode.next.next;
                    flag = true;
                    break;
                }
                curNode = curNode.next;
            }
            return flag;
        }*/

        //rec version
        public override bool Remove(T data)
        {
            return recRemove(ref head, data);
        }
        private bool recRemove(ref Node current, T data)
        {
            bool found = false;

            if (current != null)
            {
                if (current.data.CompareTo(data) == 0)
                {
                    found = true;
                    current = current.next;
                }
                else
                {
                    found = recRemove(ref current.next, data);
                }
            }
            
            return found;
        }


        public override T ReplaceAt(int index, T data)
        {
            if (head == null)
            {
                throw new InvalidOperationException("LinkedList is empty.");
            }

            if (index < 0 || index >= Count)
            {
                throw new ArgumentOutOfRangeException( "Invalid index." + index);
            }
            Node newNode = new Node(data);
            if (index == 0)
            {
                Node temp = head;
                head = newNode;
                return temp.data;
            }
            Node curNode = head;
            for (int i = 0; i < index - 1; i++)
            {
                if (curNode == null)
                {
                    throw new InvalidOperationException("Index out of range.");
                }
                curNode = curNode.next;
            }
            Node tempNode = curNode.next;
            curNode.next = newNode;
            return tempNode.data;
        }
        public override void Clear()
        {
            if (head == null) return;
            Node curNode = head;
            for (int i = 0; i < Count; i++)
            {
                Node nextNode = curNode.next;
                curNode.data = default(T);
                curNode.next = null; // 断开连接
                curNode = nextNode;
            }
            head = null;
        }
        public override T RemoveAt(int index)
        {
            if (head == null)
            {
                throw new InvalidOperationException("LinkedList is empty.");
            }

            if (index < 0 || index >= Count)
            {
                throw new ArgumentOutOfRangeException("Invalid index." + index);
            }
            if (index == 0)
            {
                Node removedNode = head;
                head = head.next;
                return removedNode.data;
            }
            Node curNode = head;
            for (int i = 0; i < index - 1; i++)
            {
                if (curNode == null)
                {
                    throw new InvalidOperationException("Index out of range.");
                }
                curNode = curNode.next;
            }
            Node removeNode = curNode.next;
            curNode.next = curNode.next.next;
            return removeNode.data;
        }

        private class Enumerator : IEnumerator<T>
        {
            //A reference to the Linked List
            private LinkedList<T> parent;
            //A refernce to the current Node that we are visiting
            private Node lastVisited;
            //The next scount that we potentially visit
            private Node scout;
            public Enumerator(LinkedList<T> parent)
            {
                this.parent = parent;
                Reset();
            }
            public T Current
            {
                get
                {
                    return lastVisited.data;
                }
            }
            object IEnumerator.Current
            {
                get
                {
                    return lastVisited.data;
                }
            }
            public void Dispose()
            {
                parent = null;
                scout = null;
                lastVisited = null;
            }
            public bool MoveNext()
            {
                bool result = false;
                if (scout != null)
                {
                    result = true;
                    lastVisited = scout;
                    scout = scout.next;
                }
                return result;
            }
            
            public void Reset()
            {
                lastVisited = null;
                scout = parent.head;
            }
        }
        private class Node
        {
            public T data;
            public Node next;
            public Node(T data) : this(data, null) { }
            
            public Node(T data, Node next)
            {
                this.data = data;
                this.next = next;
            }
        }
    }
}