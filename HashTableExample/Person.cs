using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTableExample
{
    internal class Person : IComparable<Person>
    {
        int sin = 0;
        string firstName = "";
        string lastName = "";
        int count = 1;

        public Person(int sin, string firstName, string lastName) 
        { 
            this.sin = sin;
            this.firstName = firstName;
            this.lastName = lastName;
        }

        public override int GetHashCode()
        {
            return sin.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return this.Equals((Person)obj);
        }

        public override string ToString()
        {
            return sin.ToString() + ": " + firstName + " " + lastName;
        }

        public int CompareTo(Person other)
        {
            return this.sin.CompareTo(other.sin);
        }
    }
}
