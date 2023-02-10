using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Lab12Main
{
    public class Transport: IComparable
    {
        public string name;
        public int power;

        public Transport()
        {
            name = "";
            power = 0;
        }

        public Transport(in string name, in int maxSpeed)
        {
            this.name = name;
            this.power = maxSpeed;
        }

        public Transport(in Transport t)
        {
            this.name = t.name;
            this.power = t.power;
        }        
        
        public override string ToString()
        {
            return name.ToString() + ": power - " + power.ToString();
        }        

        public void Print()
        {
            Console.WriteLine(this.ToString());
        }               

        public int CompareTo(object? obj)
        {
            if (obj != null)
            {
                int res = 0;
                if (obj is Transport c)
                {
                    res = string.Compare(this.name, c.name);
                }
                return res;
            }
            return 0;
        } 
        
        public override bool Equals(object? obj)
        {
            if (obj != null)
            {
                if (obj is Transport t)
                {
                    return Equals(this.name, t.name) && Equals(this.power, t.power);
                }
            }
            return false;
        }
    }
}