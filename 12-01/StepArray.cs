using System;

namespace Array
{
    class StepArray
    {
        public SimpleArray[] selfArray;
        private int size;
        private int _maxLength = 5;
        private int _min = 0;
        private int _max = 10;
        private decimal average;
        public decimal Average
        {
            get
            {
                return average;
            }
        }

        public StepArray(int size, bool userGenerated = false)
        {
            this.size = size;
            RegenerateStepArray(userGenerated);
        }

        public void RegenerateStepArray(bool userGenerated)
        {
            if (userGenerated)
            {
                SimpleArray[] arr = new SimpleArray[size];

                for (int i = 0; i < size; i++)
                {
                    Console.Write("Size of line: ");
                    int size = int.Parse(Console.ReadLine());
                    arr[i] = new SimpleArray(size, true);
                }

                selfArray = arr;
            }
            else
            {
                SimpleArray[] arr = new SimpleArray[size];
                Random rand = new Random();

                for (int i = 0; i < size; i++)
                {
                    arr[i] = new SimpleArray(rand.Next(1, _maxLength - 1));
                }

                selfArray = arr;
            }

            decimal summ = 0;
            int counter = 0;
            foreach (SimpleArray el in selfArray)
            {
                foreach (int item in el.selfArray)
                {
                    counter++;
                    summ += item;
                }
            }

            average = summ / counter;
        }

        public void Print()
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
    }
}
