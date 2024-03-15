using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BST
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BST<int> mapleTree = new BST<int>();
            mapleTree.Add(10);
            mapleTree.Add(5);
            mapleTree.Add(15);
            mapleTree.Add(1);
            mapleTree.Add(8);
            mapleTree.Add(11);
            mapleTree.Add(20);
            mapleTree.Add(13);
            mapleTree.Add(14);
            mapleTree.Add(12);

            mapleTree.Remove(10);
            Console.WriteLine("height is " + mapleTree.Height());
            IEnumerator<int> myEnumerator = mapleTree.GetEnumerator();
            while (myEnumerator.MoveNext()) 
            { 
               Console.WriteLine(myEnumerator.Current);
            }
        }
    }
}
