using System;

namespace _4_1
{
    class Program
    {
        static void Main(string[] args)
        {
            ArraySimple<int> intArray = new ArraySimple<int>();
            intArray.Print();
            intArray.Add(5);
            intArray.Remove(2);
            intArray.Sort();
            intArray.Count();
            intArray.CountWithCondition((x) => x > 6);
            intArray.CheckIfOneIsTrue((x) => x == 5);
            intArray.CheckIfAllAreTrue((x) => x < 3);
            intArray.Contains(10);
            intArray.MakeForEachElement((x) => Console.Write(x*x + " "));
        }
    }
}