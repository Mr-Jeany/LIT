using System;

namespace BaseArrays
{
    sealed class ArrayTwoDim : ArrayBase
    {
        public int[,] selfArray;
        public int height;
        public int width;
        private int min = 0;
        private int max = 9;
        public decimal average;
        private int determinant;
        public int Determinant
        {
            get
            {
                return determinant;
            }
        }

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

        public override void Usered()
        {
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

        public override void CountAverage()
        {

            decimal summ = 0;
            int counter = 0;
            foreach (int el in selfArray)
            {
                counter++;
                summ += el;
            }

            average = summ / counter;
        }
    }
}
