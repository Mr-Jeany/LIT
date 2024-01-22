using System;
using System.Linq;

namespace BaseArrays
{
    class Program
    {
        static void Main(string[] args)
        {
            ArraySimple arra1 = new ArraySimple(10, 0, 150, false);
            arra1.Print();
            arra1.BelowHundred();
            arra1.Print();

            ArrayTwoDim arra2 = new ArrayTwoDim(3, 3, 0, 9);
            arra2.Print();
            Console.WriteLine(arra2.average);

            ArrayStep arra3 = new ArrayStep(3, 0, 9, 7);
            arra3.Print();
            arra3.Clear();
            arra3.Print();
            Console.WriteLine(arra3.average);
        }
    }
}
