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
        }
    }

    abstract class ArrayBase
    {
        public decimal average;
        public int min = 0;
        public int max = 10;

        public abstract void Regenerate(bool userGenerated = false);
        public abstract void Print();
        public abstract void CountAverage();
    }

    class ArraySimple : ArrayBase
    {
        public int[] selfArray;
        public int size;
        public ArraySimple(int size, int min, int max, bool userGenerated = false)
        {
            this.size = size;
            this.min = min;
            this.max = max;
            Regenerate(userGenerated);
        }
        
        public override void Regenerate(bool userGenerated = false)
        {
            if (!userGenerated)
            {
                selfArray = new int[size];
                Random rand = new Random();
                for (int i = 0; i < selfArray.Length; i++)
                {
                    selfArray[i] = rand.Next(min, max);
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

                size = selfArray.Length;
            }
            
            CountAverage();
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
            Console.Write("{ ");
            foreach (var el in selfArray)
            {
                Console.Write(el + " ");
            }
            Console.WriteLine("}");
        }
    }

    class ArrayTwoDim : ArrayBase
    {
        public int[,] selfArray;
        private int determinant;

        public int Determinant
        {
            get
            {
                return determinant;
            }
        }
        public int height;
        public int width;

        public ArrayTwoDim(int height, int width, int min, int max, bool userGenerated = false)
        {
            this.height = height;
            this.width = width;
            this.min = min;
            this.max = max;
            Regenerate(userGenerated);
        }

        public override void Regenerate(bool userGenerated = false)
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
                        selfArray[i, n] = rand.Next(min, max + 1);
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

            determinant = GetDeterminant(selfArray);
            
            CountAverage();
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

        public override void Print()
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

    class ArrayStep : ArrayBase
    {
        public int[][] selfArray;
        private int size;
        public int maxLength;
        private decimal average;
        public decimal Average
        {
            get
            {
                return average;
            }
        }

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
                    arr[i] = new int[rand.Next(1, maxLength)];
                    Console.WriteLine("[" + arr[i].Length + "]");
                    for (int j = 0; j < arr[i].Length; j++)
                    {
                        arr[i][j] = rand.Next(min, max);
                    }
                }

                selfArray = arr;
            }
            
            CountAverage();
        }

        public override void CountAverage()
        {
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

        public override void Print()
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
