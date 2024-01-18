using System;

namespace Array
{
    public class TwoDimArray
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
}