using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Delegates
{
    public class Program
    {
        public static void DisplayInt(int x)
        {
            Console.WriteLine(x);
        }
        public static void DisplaySquare(int x)
        {
            Console.WriteLine(x * x);
        }
        //Can we use a method where the signature does not match.
        public static void DisplayAsDouble(double x)
        {
            Console.WriteLine(x);
        }
        static void Main(string[] args)
        {
            Collection c = new Collection();
            c.iterate(DisplayInt, Collection.Direction.BACKWARD);
            //c.iterate(DisplayAsDouble); //Signature must match the delegate.
        }
    }
}