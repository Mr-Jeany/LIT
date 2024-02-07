using System;
using System.Linq;

namespace _3_4
{
    sealed class ArraySimple : ArrayBase
    {
        public int[] selfArray;
        public int size;
        public decimal average;
        public ArraySimple(int size, int min, int max, bool userGenerated = false)
        {
            this.size = size;
            this.min = min;
            this.max = max;
            Regenerate(userGenerated);
        }
        
        public override void Regenerate(bool userGenerated = false)
        {
            if (userGenerated)
            {
                Usered();
            }
            else
            {
                Randomed();
            }
            
            CountAverage();
        }

        public override void Randomed()
        {
            selfArray = new int[size];
            Random rand = new Random();
            for (int i = 0; i < selfArray.Length; i++)
            {
                selfArray[i] = rand.Next(min, max);
            }
        }

        public override void Usered()
        {
            string[] selfArrayString = Console.ReadLine().Split();
            selfArray = new int[selfArrayString.Length];
            for (int i = 0; i < selfArrayString.Length; i++)
            {
                selfArray[i] = int.Parse(selfArrayString[i]);
            }

            size = selfArray.Length;
        }

        public override void CountAverage()
        {
            
            decimal summ = 0;
            foreach (int el in selfArray)
            {
                summ += el;
            }

            average = summ / selfArray.Length;
        }

        public void BelowHundred()
        {
            int newArraySize = selfArray.Count(el => (el <= 100 && el > 0) || (el >= -100 && el < 0));
            if (newArraySize != selfArray.Length)
            {
                int[] tempArray = new int[newArraySize];
                int n = 0;
                for (int i = 0; i < selfArray.Length; i++)
                {
                    if (selfArray[i] <= 100 && selfArray[i] >= 0 || selfArray[i] >= -100 && selfArray[i] < 0)
                    {
                        tempArray[n] = selfArray[i];
                        n++;
                    }
                }
                selfArray = tempArray;
            }
        }
        
        public void Clear()
        {
            int counter = selfArray.Length;
            int empty = Int32.MinValue;
            for (int i = selfArray.Length - 1; i > 0; i--)
            {
                if (selfArray[i] != empty && selfArray.Count(s => s == selfArray[i]) >= 2)
                {
                    selfArray[i] = empty;
                    counter--;
                }
            }

            int[] newArray = new int[counter];
            counter = 0;
            for (int i = 0; counter < newArray.Length; i++)
            {
                if (selfArray[i] != empty)
                {
                    newArray[counter] = selfArray[i];
                    counter++;
                }
            }

            selfArray = newArray;
        }
        
        public override void Print()
        {
            foreach (var el in selfArray)
            {
                Console.Write(el + " ");
            }
            Console.WriteLine();
        }
    }
}