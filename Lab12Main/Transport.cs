using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Lab12Main
{
    public class Transport: ICloneable<Transport>
    {
        public string name;
        public int power;

        public Transport()
        {
            name = "";
            power = 0;
        }

        public Transport(string name, int maxSpeed)
        {
            this.name = name;
            this.power = maxSpeed;
        }

        public Transport(Transport t)
        {
            this.name = t.name;
            this.power = t.power;
        }        
        
        public override string ToString()
        {
            return GetType() + " " + name.ToString() + ": power - " + power.ToString();
        }        

        public void Print()
        {
            Console.WriteLine(this.ToString());
        }

        public override bool Equals(object? obj)
        {
            if (obj != null)
            {
                if (obj is Transport t)
                {
                    //return Equals(this.name, t.name) && Equals(this.power, t.power);
                    return string.Equals(this.name, t.name) && (this.power == t.power);
                }
            }
            return false;
        }

        public virtual Transport Clone()
        {
            return new Transport(this);
        }
    }
}