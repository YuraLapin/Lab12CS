using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab12Main
{
    public class Express: Train
    {
        public List<string> stationsToSkip;

        public Express(): base()
        {
            stationsToSkip = new List<string>();
        }

        public Express(in string name, in int maxSpeed, in int cars, in List<string> stationsToSkip): base(name, maxSpeed, cars)
        {
            this.stationsToSkip = stationsToSkip;
        } 
        
        public Express(in Express e): base(e)
        {
            this.stationsToSkip = new List<string>(e.stationsToSkip);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(name.ToString() + ": power - " + power.ToString() + ", cars - " + cars.ToString() + ", list of stations to skip: [ ");
            if (stationsToSkip.Count > 0)
            {
                foreach (string s in stationsToSkip)
                {
                    sb.Append(s + ", ");
                }
                sb.Append("]");
            }
            else
            {
                sb.Append("- ]");
            }
            return GetType() + " " + sb.ToString();
        }        

        public override bool Equals(object? obj)
        {
            if (obj != null)
            {
                if (obj is Express e)
                {
                    //return Equals(this.name, e.name) && Equals(this.power, e.power) && Equals(this.cars, e.cars) && Equals(this.stationsToSkip, e.stationsToSkip);
                    return Equals(this.ToString(), e.ToString());
                }
            }
            return false;
        }
    }
}