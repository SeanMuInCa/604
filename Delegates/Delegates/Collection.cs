using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Delegates
{
    public class Collection
    {
        private int[] iArray = { 5, 10, 20 };
        public enum Direction
        {
            FORWARD,
            BACKWARD
        }
        //Create a delegate type
        public delegate void DoSomethingToData(int x);
        //A method to iterate through the data
        //Use the delegate to pass a method into iterate
        public void iterate(DoSomethingToData ds, Direction dir)
        {
            //for (int j = 0; j < iArray.Length; j++)
            //{
            //    //Console.WriteLine(iArray[j]);
            //    //Call the instance of the delegate on the current data item.
            //    ds(iArray[j]);
            //}
            switch (dir)
            {
                case Direction.FORWARD:
                    for (int j = 0; j < iArray.Length; j++)
                    {
                        ds(iArray[j]);
                    }
                    break;
                case Direction.BACKWARD:
                    for (int j = iArray.Length - 1; j >= 0; j--)
                    {
                        ds(iArray[j]);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}







