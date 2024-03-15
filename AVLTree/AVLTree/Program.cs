using BST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVLTree
{
    internal class Program
    {
        static void DoSomething(int x)
        { 
            Console.Write(x + " ");
        }
        static void TestRandomAVL()
        {
            long start;
            long end;
            AVLTree<int> balancedTree = new AVLTree<int>();

            start = Environment.TickCount;
            Random random = new Random((int) start);//unique seed to make the number random

            int max = 50000;
            int largest = max * 10;

            List<int> removedList = new List<int>();

            int add;

            for (int i = 0; i < max; i++) 
            { 
                add = random.Next(i, largest);
                balancedTree.Add(add);
                if (i % 10 == 0)
                { 
                    removedList.Add(i);
                }
            }

            if (balancedTree.Count <= 50)
            {
                Console.WriteLine(balancedTree.ToString());
            }

            end = Environment.TickCount;

            Console.WriteLine("Time to add: " + (end - start).ToString() + " ms!" );
            Console.WriteLine("Theoretical Minimum height: " + Math.Truncate(Math.Log(max, 2)));
            Console.WriteLine("Actual Height: " + balancedTree.Height());
        }
        static void Main(string[] args)
        {
            TestRandomAVL();
            /*BST<int> mapleTree = new BST<int>();
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

            IEnumerator<int> myEnumerator = mapleTree.GetEnumerator();
            while (myEnumerator.MoveNext())
            {
                Console.WriteLine(myEnumerator.Current);
            }

            Console.WriteLine("the height: " + mapleTree.Height());

            mapleTree.Iterate(DoSomething, TRAVERSALORDER.POST_ORDER);*/
        }
    }
}
