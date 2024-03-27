using System;

namespace _4_1
{
    class Program
    {
        static void Main(string[] args)
        {
            ArraySimple<int> intArray = new ArraySimple<int>();
            intArray.Print();
            intArray.Add(19);
            intArray.Print();
            intArray.Remove(2);
            intArray.Print();
            intArray.Sort();
            intArray.Print();
        }
    }
}
