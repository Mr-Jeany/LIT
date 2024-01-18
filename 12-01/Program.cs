using System;
using System.Linq;

namespace Arrays
{
    class Program
    {
        static void Main(string[] args)
        {
            SimpleArray arr = new SimpleArray(10);
            arr.Print();
            arr.RegenerateArray(10);
            arr.Print();
            Console.WriteLine(arr.Average);

            SimpleArray arr2 = new SimpleArray(10);
            arr2.Print();
            arr2.BelowHundred();
            arr2.Print();

            SimpleArray arr3 = new SimpleArray(10, true);
            arr3.Print();
            arr3.Clear();
            arr3.Print();

            TwoDimArray tda = new TwoDimArray(3, 3);
            tda.Print();
            Console.WriteLine(tda.Determinant);
            tda.RegenerateTwoDimArray(3, 3, true);
            tda.Print();
            Console.WriteLine(tda.Average);

            StepArray sa = new StepArray(4, true);
            sa.Print();
        }
    }
}
