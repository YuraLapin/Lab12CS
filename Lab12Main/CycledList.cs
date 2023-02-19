using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab12Main
{
    public class Node
    {
        public Node? prev;
        public Node? next;
        public Transport? data;

        public Node()
        {
            prev = null;
            next = null;
            data = null;
        }
    }

    public class CycledList: IEnumerable<Transport>, ICollection<Transport>
    {
        public Node? start = null;
        public int Count
        {
            get;
            set;
        }
        public bool IsReadOnly
        {
            get;
            set;
        }

        public CycledList()
        {
            start = null;
            Count = 0;
        }

        public CycledList(int size)
        {
            start = null;
            Count = 0;
            for (int i = 0; i < size; ++i)
            {
                Add(null);
            }
        }

        public CycledList(CycledList cl)
        {
           foreach (Transport t in cl)
           {
                Add(new Transport(t));
           }
           IsReadOnly = this.IsReadOnly;
        }

        public void Add(Transport? t)
        {       
            if (t != null)
            {
                if (start == null)
                {
                    Count = 1;
                    start = new Node();
                    start.next = start;
                    start.prev = start;
                }
                else
                {
                    ++Count;
                    var newNode = new Node();
                    newNode.next = start;
                    newNode.prev = start.prev;
                    start.prev.next = newNode;
                    start.prev = newNode;
                    start = newNode;
                }
                if (t is Express express)
                {
                    start.data = new Express(express);
                }
                else if (t is Train train)
                {
                    start.data = new Train(train);
                }
                else
                {
                    start.data = new Transport(t);
                }
            }
            else
            {
                if (start != null)
                {
                    start.data = null;
                }
                else
                {
                    start = new Node();
                }
            }
        }

        public void Print()
        {
            Console.WriteLine("[");            
            foreach(Transport t in this)
            {
                t.Print();
            }
            if (Count == 0)
            {
                Console.WriteLine("-");
            }
            Console.WriteLine("]");
        }

        public IEnumerator<Transport> GetEnumerator()
        {
            if (start != null)
            {
                var curNode = start;
                for (int i = 0; i < Count; ++i)
                {
                    yield return curNode.data;
                    curNode = curNode.next;
                }                
            }
            yield break;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }     

        public void Clear()
        {
            if (start != null)
            {
                for (int i = 0; i < Count - 1; ++i)
                {
                    start.next.prev = start.prev;
                    start.prev.next = start.next;
                    start = start.next;
                }
                Count = 0;
                start.next = null;
                start.prev = null;
                start = null;
            }            
        }

        public bool Contains(Transport toFind)
        {
            foreach(Transport t in this)
            {
                if (t.Equals(toFind))
                {
                    return true;
                }
            }
            return false;
        }

        public void CopyTo(Transport[] arr, int start)
        {
            if (Count + start <= arr.GetLength(0))
            {
                int i = start;
                foreach(Transport t in this)
                {
                    arr[i] = new Transport(t);
                    ++i;
                }
            }
        }

        public bool Remove(Transport t)
        {
            if (start != null)
            {
                int iterations = 0;
                while (Contains(t))
                {
                    var curNode = start;
                    int i = 0;
                    while (!((Transport)curNode.data).Equals(t))
                    {
                        curNode = curNode.next;
                        ++i;
                    }
                    if (i == 0)
                    {
                        start = start.next;
                    }
                    curNode.data = null;
                    curNode.next.prev = curNode.prev;
                    curNode.prev.next = curNode.next;
                    --Count;
                    iterations++;
                }
                if (iterations > 0)
                {
                    return true;
                }
            }
            return false;
        } 
        
        public void Copy(ref CycledList list)
        {
            list = new CycledList(this);    
        }

        public void ShallowCopy(ref CycledList list)
        {
            list.start = this.start;
            list.Count = this.Count;
            list.IsReadOnly = this.IsReadOnly;
        }        
    }
}