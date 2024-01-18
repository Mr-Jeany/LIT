using System;
using System.Linq;

namespace Array
{
    public class SimpleArray
    {
        public int[] selfArray;
        private int _min = 0;
        private int _max = 10;
        private decimal average;

        public decimal Average
        {
            get { return average; }
        }

        public SimpleArray(int size, bool userGenerated = false)
        {
            RegenerateArray(size, userGenerated);
        }

        public void RegenerateArray(int size, bool userGenerated = false)
        {
            if (!userGenerated)
            {
                selfArray = new int[size];
                Random rand = new Random();
                for (int i = 0; i < selfArray.Length; i++)
                {
                    selfArray[i] = rand.Next(_min, _max + 1);
                }
            }
            else
            {
                string[] selfArrayString = Console.ReadLine().Split();
                selfArray = new int[selfArrayString.Length];
                for (int i = 0; i < selfArrayString.Length; i++)
                {
                    selfArray[i] = int.Parse(selfArrayString[i]);
                }
            }

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

        public void Print()
        {
            foreach (var el in selfArray)
            {
                Console.Write(el + " ");
            }
            Console.WriteLine();
        }
    }
}