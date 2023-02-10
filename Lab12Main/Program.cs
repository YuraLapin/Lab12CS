using System.Linq.Expressions;

namespace Lab12Main
{
    public class Program
    {
        public static Random rand = new Random();       

        public static int Main()
        {
            var list = new CycledList();

            list.Add(new Transport("transport 1", 1));
            list.Add(new Transport("train 2", 2));
            list.Add(new Transport("express 3", 3));

            list.Print();

            return 0;
        }
    }
}