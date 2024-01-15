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

    class SimpleArray
    {
        public int[] selfArray;
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
                for (int i = 0; i < selfArray.Length - 1; i++)
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
            Console.Write("{ ");
            foreach (var el in selfArray)
            {
                Console.Write(el + " ");
            }
            Console.WriteLine("}");
        }
    }

    class TwoDimArray
    {
        public int[,] selfArray;
        public int height;
        public int width;
        private int _min = 0;
        private int _max = 9;
        private decimal average;
        public decimal Average
        {
            get
            {
                return average;
            }
        }
        private int determinant;
        public int Determinant
        {
            get
            {
                return determinant;
            }
        }

        public TwoDimArray(int width, int height, bool userGenerated = false)
        {

            RegenerateTwoDimArray(width, height , userGenerated);
        }

        public void RegenerateTwoDimArray(int width, int height, bool userGenerated = false)
        {
            if (!userGenerated)
            {
                this.width = width;
                this.height = height;
                selfArray = new int[height, width];
                Random rand = new Random();
                for (int i = 0; i < height; i++)
                {
                    for (int n = 0; n < width; n++)
                    {
                        selfArray[i, n] = rand.Next(_min, _max + 1);
                    }
                }
            } 
            else
            {
                this.width = width;
                this.height = height;
                selfArray = new int[height, width];
                for (int i = 0; i < height; i++)
                {
                    string[] input = Console.ReadLine().Split();
                    for (int n = 0; n < width; n++)
                    {
                        selfArray[i, n] = int.Parse(input[n]);
                    }
                }
            }
            

            decimal summ = 0;
            foreach (int el in selfArray)
            {
                summ += el;
            }

            average = summ / selfArray.Length;

            determinant = GetDeterminant(selfArray);
        }

        private int GetDeterminant(int[,] matr)
        {
            int size = matr.GetLength(0);
            int determ = 0;

            if (size == 2)
            {
                determ += matr[0, 0] * matr[1, 1] - matr[0, 1] * matr[1, 0];

            }
            else if (size == 1)
            {
                return matr[0, 0];
            }
            else
            {
                for (int i = 0; i < size; i++)
                {
                    int det = GetDeterminant(CutMatrix(matr, i, 0));
                    int sign = i % 2 == 0 ? 1 : -1;
                    determ += (sign) * matr[0, i] * det;

                }
            }

            return determ;
        }

        static int[,] CutMatrix(int[,] firstMatrix, int x, int y)
        {
            int size = firstMatrix.GetLength(0);
            int[,] matrix = new int[size - 1, size - 1];
            int counterX = 0;
            int counterY = 0;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (counterX == size - 1)
                    {
                        counterX = 0;
                        counterY++;
                    }
                    if (i != x && j != y)
                    {
                        matrix[counterX, counterY] = firstMatrix[j, i];
                        counterX++;
                    }
                }
            }

            return matrix;
        }

        public void Print()
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Console.Write(selfArray[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
    }

    class StepArray
    {
        public int[][] selfArray;
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
                int[][] arr = new int[size][];

                for (int i = 0; i < size; i++)
                {
                    string[] input = Console.ReadLine().Split(" ");
                    arr[i] = new int[input.Length];
                    for (int j = 0; j < input.Length; j++)
                    {
                        arr[i][j] = int.Parse(input[j]);
                    }
                }

                selfArray = arr;
            }
            else
            {
                int[][] arr = new int[size][];
                Random rand = new Random();

                for (int i = 0; i < size; i++)
                {
                    arr[i] = new int[rand.Next(1, _maxLength)];
                    Console.WriteLine("[" + arr[i].Length + "]");
                    for (int j = 0; j < arr[i].Length; j++)
                    {
                        arr[i][j] = rand.Next(_min, _max);
                    }
                }

                selfArray = arr;
            }

            decimal summ = 0;
            int counter = 0;
            foreach (int[] el in selfArray)
            {
                foreach (int item in el)
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
                
                foreach (var val in el)
                {
                    Console.Write(val + " ");
                }

                Console.WriteLine();
            }
        }

        public void Clear()
        {
            for (int i = 0; i < selfArray.Length; i++)
            {
                for (int j = 0; j < selfArray[i].Length; j++)
                {
                    if (selfArray[i][j] % 2 == 0)
                    {
                        selfArray[i][j] = i * j;
                    }
                }
            }
        }


    }
}