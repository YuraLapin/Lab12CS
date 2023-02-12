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

       public void Add(Transport t)
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

        IEnumerator<Transport> IEnumerable<Transport>.GetEnumerator()
        {
            return (IEnumerator<Transport>)GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        public ListEnum GetEnumerator()
        {
            var arr = new Transport[Count];
            if (start != null)
            {
                var curNode = start;
                for (int i = 0; i < Count; ++i)
                {
                    arr[i] = curNode.data;
                    curNode = curNode.next;
                }
            }            
            return new ListEnum(arr);
        }

        public void Clear()
        {
            if (start != null)
            {
                var curNode = start;
                for (int i = 0; i < Count - 1; ++i)
                {
                    curNode.data = null;
                    curNode = curNode.next;
                    curNode.prev = null;
                }
                curNode.data = null;
                Count = 0;
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
                if (Contains(t))
                {
                    var curNode = start;
                    int i = 0;
                    if (t is Express express)
                    {
                        while (!(curNode.data is Express) || !curNode.data.Equals(express))
                        {
                            curNode = curNode.next;
                            ++i;
                        }
                    }
                    else if (t is Train train)
                    {
                        while (!(curNode.data is Train) || !curNode.data.Equals(train))
                        {
                            curNode = curNode.next;
                            ++i;
                        }
                    }
                    else
                    {
                        while (!curNode.data.Equals(t))
                        {
                            curNode = curNode.next;
                            ++i;
                        }

                    }
                    if (i == 0)
                    {
                        start = start.next;
                    }
                    curNode.data = null;
                    curNode.next.prev = curNode.prev;
                    curNode.prev.next = curNode.next;
                    --Count;
                    return true;
                }
            }
            return false;
        } 
        
        public void Copy(ref CycledList list)
        {
            list.Clear();
            foreach(Transport t in this)
            {
                list.Add(new Transport(t));
            }
            list.IsReadOnly = this.IsReadOnly;
        }

        public void ShallowCopy(ref CycledList list)
        {
            list.start = this.start;
            list.Count = this.Count;
            list.IsReadOnly = this.IsReadOnly;
        }

        public class ListEnum: IEnumerator
        {
            public Transport[] arr;
            int position = -1;

            public ListEnum(Transport[] list)
            {
                arr = list;
            }

            public bool MoveNext()
            {
                position++;
                return (position < arr.Length);
            }

            public void Reset()
            {
                position = -1;
            }

            object IEnumerator.Current
            {
                get
                {
                    return Current;
                }
            }

            public Transport Current
            {
                get
                {
                    return arr[position];
                }
            }
        }
    }
}