using System;

namespace HashTableExample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*Quadratic<int, string> quad = new Quadratic<int, string>();
            TestAdd(quad);
            Console.WriteLine(quad.ToString());
            TestExpand(quad);
            Console.WriteLine(quad.ToString());
            TestRemove(quad);
            Console.WriteLine(quad.ToString());
            TestGet(quad);

            Console.WriteLine("---------------------------------------");
            DoubleHash<int, string> dh = new DoubleHash<int, string>();
            TestAdd(dh);
            Console.WriteLine(dh.ToString());
            TestExpand(dh);
            Console.WriteLine(dh.ToString());
            TestRemove(dh);
            Console.WriteLine(dh.ToString());
            TestGet(dh);*/

            
            Quadratic<Person, Person> quadPerson = new Quadratic<Person, Person>();
            DoubleHash<Person, Person> dhPerson = new DoubleHash<Person, Person>();
            Linear<Person, Person> linearPerson = new Linear<Person, Person>();
            TestHT(quadPerson);
            Console.WriteLine("---------------------------------------");
            TestHT(dhPerson);
            Console.WriteLine("---------------------------------------");
            TestHT(linearPerson);
            Console.WriteLine("---------------------------------------");

        }


        public static void TestAdd(A_HashTable<int, string> table)
        {
            try
            {
                //table.Add(11, "rob");
                table.Add(22, "bryce");
                table.Add(13, "mu");

                Console.WriteLine("number of collisions: " + table.NumCollision);
            }
            catch (Exception ex) 
            { 
                Console.WriteLine(ex.Message);
            }
        }

        static void TestExpand(A_HashTable<int, string> table)
        {
            try
            {
                table.Add(32, "howard");
                table.Add(18, "mona");
                table.Add(26, "sofia");
            }
            catch(Exception ex) 
            { 
                Console.WriteLine(ex.Message);
            }
        }

        static void TestRemove(A_HashTable<int, string> table)
        {
            try
            {
                table.Remove(32);
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }

        }

        static void TestGet(A_HashTable<int, string> table)
        {
            try
            { 
                Console.WriteLine("value of key 26: " + table.Get(26));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        static void LoadFromDataFile(A_HashTable<Person, Person> table)
        {
            StreamReader sr = new StreamReader(File.Open("People.txt", FileMode.Open));
            string input = "";
            try
            {
                while ((input = sr.ReadLine()) != null) 
                {
                    try
                    {
                        char[] array = { ' ' };
                        string[] array2 = input.Split(array);

                        int sin = Int32.Parse(array2[0]);
                        string lastName = array2[1];
                        string firstName = array2[2];

                        Person p = new Person(sin, firstName, lastName);
                        table.Add(p, p);
                    }
                    catch (Exception e)
                    { 
                        Console.WriteLine($"{e.Message}");
                    }
                }
            }
            catch (Exception e)
            { 
                Console.WriteLine (e.Message);
            }finally 
            { 
                sr.Close(); 
            }
            
        }

        static void TestHT(A_HashTable<Person, Person> table)
        {
            LoadFromDataFile(table);
            Console.WriteLine("hash table type: " + table.GetType().Name);
            Console.WriteLine("# of people added:  " + table.Count);
            Console.WriteLine("final table size:  " + table.HTSize);
            Console.WriteLine("# of collisions:  " + table.NumCollision + "\n");
        }

    }
}