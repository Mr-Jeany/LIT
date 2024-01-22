using System;

namespace BaseArrays
{
    class ArrayStep : ArrayBase
    {
        public ArraySimple[] selfArray;
        private int size;
        private int maxLength = 5;
        private int min = 0;
        private int max = 10;
        public decimal average;

        public ArrayStep(int size, int min, int max, int maxLength, bool userGenerated = false)
        {
            this.size = size;
            this.min = min;
            this.max = max;
            this.maxLength = maxLength;
            Regenerate(userGenerated);
        }

        public override void Regenerate(bool userGenerated = false)
        {
            if (userGenerated)
            {
                ArraySimple[] arr = new ArraySimple[size];

                for (int i = 0; i < size; i++)
                {
                    Console.Write("Size of line: ");
                    int size = int.Parse(Console.ReadLine());
                    arr[i] = new ArraySimple(size, 0, 9, true);
                }

                selfArray = arr;
            }
            else
            {
                ArraySimple[] arr = new ArraySimple[size];
                Random rand = new Random();

                for (int i = 0; i < size; i++)
                {
                    arr[i] = new ArraySimple(rand.Next(1, maxLength - 1), 0, 9);
                }

                selfArray = arr;
            }

            CountAverage();
        }

        public override void Print()
        {
            foreach (var el in selfArray)
            {
                el.Print();
            }
        }

        public void Clear()
        {
            for (int i = 0; i < selfArray.Length; i++)
            {
                for (int j = 0; j < selfArray[i].selfArray.Length; j++)
                {
                    if (selfArray[i].selfArray[j] % 2 == 0)
                    {
                        selfArray[i].selfArray[j] = i * j;
                    }
                }
            }
        }

        public override void CountAverage()
        {
            decimal summ = 0;
            int counter = 0;
            foreach (ArraySimple el in selfArray)
            {
                foreach (int item in el.selfArray)
                {
                    counter++;
                    summ += item;
                }
            }

            average = summ / counter;
        }
    }
}