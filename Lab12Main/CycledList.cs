using Lab12Main;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Lab12Main
{
    public class Node<T>
    {
        public Node<T>? Prev;
        public Node<T>? Next;
        public T? data;

        public Node()
        {
            Prev = null;
            Next = null;
            data = default(T);
        }
    }

    public class CycledList<T> : IEnumerable<T>, ICollection<T> where T : ICloneable<T>, new()
    {
        public Node<T>? Start = null;
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

        public virtual T? this[int index]
        {
            get
            {
                if (Start != null)
                {
                    var curNode = Start;
                    for (int i = 0; i < index; ++i)
                    {
                        if (curNode.Next == null || curNode.Prev == null)
                        {
                            return default(T);
                        }
                        curNode = curNode.Next;
                    }
                    if (curNode == null)
                    {
                        return default(T);
                    }
                    return curNode.data;
                }
                return default(T);
            }
            set
            {
                if (Start != null)
                {
                    var curNode = Start;
                    for (int i = 0; i < index; ++i)
                    {
                        if (curNode != null)
                        {
                            if (curNode.Next != null || curNode.Prev != null)
                            {
                                curNode = curNode.Next;
                            }
                        }
                    }
                    if (curNode != null)
                    {
                        curNode.data = value;
                    }
                }
            }
        }

        public CycledList()
        {
            Start = null;
            Count = 0;
        }

        public CycledList(int size)
        {
            Start = null;
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

        public virtual void Add(T? element)
        {
            if (IsReadOnly)
            {
                return;
            }
            if (element != null)
            {
                if (Start == null)
                {
                    Count = 1;
                    Start = new Node<T>();
                    Start.Next = Start;
                    Start.Prev = Start;
                }
                else
                {
                    ++Count;
                    var newNode = new Node<T>();
                    newNode.Next = Start;
                    newNode.Prev = Start.Prev;
                    Start.Prev.Next = newNode;
                    Start.Prev = newNode;
                    Start = newNode;
                }
                if (element is T temp)
                {
                    Start.data = element.Clone();
                }
                else
                {
                    Start.data = (T)element.Clone();
                }
            }
            else
            {
                if (Start != null)
                {
                    Start.data = default(T);
                }
                else
                {
                    Start = new Node<T>();
                }
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("[\n");
            foreach (T t in this)
            {
                sb.Append(t.ToString() + "\n");
            }
            if (Count == 0)
            {
                sb.Append("-\n");
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
            if (Start != null)
            {
                var curNode = Start;
                for (int i = 0; i < Count; ++i)
                {
                    yield return curNode.data;
                    curNode = curNode.Next;
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
            if (Start != null)
            {
                for (int i = 0; i < Count - 1; ++i)
                {
                    Start.Next.Prev = Start.Prev;
                    Start.Prev.Next = Start.Next;
                    Start = Start.Next;
                }
                Count = 0;
                Start.Next = null;
                Start.Prev = null;
                Start = null;
            }
        }

        public bool Contains(T toFind)
        {
            if (Start == null)
            {
                return false;
            }
            foreach (T element in this)
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
                foreach (T element in this)
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
            if (Start != null)
            {
                int iterations = 0;
                while (Contains(t))
                {
                    var curNode = Start;
                    int i = 0;
                    while (!curNode.data.Equals(t))
                    {
                        curNode = curNode.Next;
                        ++i;
                    }
                    if (i == 0)
                    {
                        Start = Start.Next;
                    }
                    curNode.data = default(T);
                    curNode.Next.Prev = curNode.Prev;
                    curNode.Prev.Next = curNode.Next;
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

        public virtual bool Remove(int index)
        {
            if (IsReadOnly)
            {
                return false;
            }
            if (Start == null)
            {
                return false;
            }
            var curNode = Start;
            for (int i = 0; i < index; ++i)
            {
                curNode = curNode.Next;
            }
            if (curNode == Start)
            {
                Start = curNode.Next;
            }
            curNode.Next.Prev = curNode.Prev;
            curNode.Prev.Next = curNode.Next;
            --Count;
            return true;
        }

        public void Copy(ref CycledList<T> list)
        {
            list = new CycledList<T>(this);
        }

        public void ShallowCopy(ref CycledList<T> list)
        {
            list.Start = this.Start;
            list.Count = this.Count;
            list.IsReadOnly = this.IsReadOnly;
        }
    }
}