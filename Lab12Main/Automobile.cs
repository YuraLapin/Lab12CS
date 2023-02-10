using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab12Main
{
    public class Automobile: Transport
    {
        public int wheels;

        public Automobile(): base()
        {
            this.wheels = 0;
        }

        public Automobile(in string name, in int maxSpeed, in int horsepowers): base(name, maxSpeed)
        {
            this.wheels = horsepowers;
        }

        new public void RandomInit()
        {
            int maxWheels = 9;
            wheels = Program.rand.Next(maxWheels);
        }

        public override string ToString()
        {
            return name.ToString() + ": power - " + power.ToString() + ", wheels - " + wheels.ToString();
        }

        public string ConvertToStringNonVirual()
        {
            return name.ToString() + ": power - " + power.ToString() + ", wheels - " + wheels.ToString();
        }
    }
}