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
            intArray.Print();
            intArray.Remove(2);
            intArray.Print();
            intArray.Reverse();
            intArray.Print();
            intArray.Sort();
            intArray.Print();
            Console.WriteLine(intArray.Count());
            Console.WriteLine(intArray.CountWithCondition((x) => x > 6));
            Console.WriteLine(intArray.CheckIfOneIsTrue((x) => x == 5));
            Console.WriteLine(intArray.CheckIfAllAreTrue((x) => x < 3));
            Console.WriteLine(intArray.Contains(10));
            intArray.MakeForEachElement((x) => Console.Write(x*x + " "));

            ArraySimple<string> stringArray = new ArraySimple<string>();
            stringArray.Print();
            stringArray.Add("name");
            stringArray.Print();
            stringArray.Remove(2);
            stringArray.Print();
            stringArray.Reverse();
            stringArray.Print();
            stringArray.Sort();
            stringArray.Print();
            Console.WriteLine(stringArray.Count());
            Console.WriteLine(stringArray.CountWithCondition((x) => x.Length > 3));
            Console.WriteLine(stringArray.CheckIfOneIsTrue((x) => x.Length == 4));
            Console.WriteLine(stringArray.CheckIfAllAreTrue((x) => x.Length >= 2));
            Console.WriteLine(stringArray.Contains("hello"));
            stringArray.MakeForEachElement((x) => Console.Write(x + " "));
        }
    }
}
