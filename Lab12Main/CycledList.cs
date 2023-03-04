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
    public class Node<T>
    {
        public Node<T>? prev;
        public Node<T>? next;
        public T? data;

        public Node()
        {
            prev = null;
            next = null;
            data = default(T);
        }
    }

    public class CycledList<T>: IEnumerable<T>, ICollection<T> where T: ICloneable<T>, new()
    {
        public Node<T>? start = null;
        public int Count
        {
            get;
            private set;
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
                Add(default(T));
            }
        }

        public CycledList(CycledList<T> anotherList)
        {
            foreach (T element in anotherList)
            {
                Add((T)element.Clone());
            }
            IsReadOnly = this.IsReadOnly;
        }

        public void Add(T? element)
        {   if (IsReadOnly)
            {
                return;
            }
            if (element != null)
            {
                if (start == null)
                {
                    Count = 1;
                    start = new Node<T>();
                    start.next = start;
                    start.prev = start;
                }
                else
                {
                    ++Count;
                    var newNode = new Node<T>();
                    newNode.next = start;
                    newNode.prev = start.prev;
                    start.prev.next = newNode;
                    start.prev = newNode;
                    start = newNode;
                }
                if (element is T temp)
                {
                    start.data = element.Clone();
                }
                else
                {
                    start.data = (T)element.Clone();
                }
            }
            else
            {
                if (start != null)
                {
                    start.data = default(T);
                }
                else
                {
                    start = new Node<T>();
                }
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("[");
            foreach (T t in this)
            {
                sb.Append(t.ToString());
            }
            if (Count == 0)
            {
                sb.Append("-");
            }
            sb.Append("]");
            return sb.ToString();
        }

        public void Print()
        {
            Console.WriteLine(ToString());
        }

        public IEnumerator<T> GetEnumerator()
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
            if (IsReadOnly)
            {
                return;
            }
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

        public bool Contains(T toFind)
        {
            if (start == null)
            {
                return false;
            }
            foreach(T element in this)
            {
                if (element.Equals(toFind))
                {
                    return true;
                }
            }
            return false;
        }

        public void CopyTo(T[] arr, int start)
        {
            if (Count + start <= arr.GetLength(0))
            {
                int i = start;
                foreach(T element in this)
                {
                    arr[i] = element.Clone();
                    ++i;
                }
            }
        }

        public bool Remove(T t)
        {
            if (IsReadOnly)
            {
                return false;
            }
            if (start != null)
            {
                int iterations = 0;
                while (Contains(t))
                {
                    var curNode = start;
                    int i = 0;
                    while (!curNode.data.Equals(t))
                    {
                        curNode = curNode.next;
                        ++i;
                    }
                    if (i == 0)
                    {
                        start = start.next;
                    }
                    curNode.data = default(T);
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
        
        public void Copy(ref CycledList<T> list)
        {
            list = new CycledList<T>(this);    
        }

        public void ShallowCopy(ref CycledList<T> list)
        {
            list.start = this.start;
            list.Count = this.Count;
            list.IsReadOnly = this.IsReadOnly;
        }        
    }
}