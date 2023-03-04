using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab12Main
{
    public class Train : Transport
    {
        public int cars;

        public Train() : base()
        {
            cars = 0;
        }

        public Train(string name, int maxSpeed, int cars) : base(name, maxSpeed)
        {
            this.cars = cars;
        }

        public Train(Train t): base (t)
        {
            cars = t.cars;
        }        

        public override string ToString()
        {
            return GetType() + " " + name.ToString() + ": power - " + power.ToString() + ", cars - " + cars.ToString();
        }        

        //public override bool Equals(object? obj)
        //{
        //    if (obj != null)
        //    {
        //        if (obj is Train t)
        //        {
        //            //return Equals(this.name, t.name) && Equals(this.power, t.power) && Equals(this.cars, t.cars);
        //            return Equals(this.ToString(), t.ToString());
        //        }
        //    }
        //    return false;
        //}

        public override Train Clone()
        {
            return new Train(this);
        }
    }
}