using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{
    internal class Matrix
    {
        private char[,] matrix;
        private int digitCount;

        public Matrix(char[,] inputMatrix)
        {
            matrix = inputMatrix;
            digitCount = Count();
        }

        public string this[int column]
        {
            get
            {
                string result = "";          
                for (int row = 0; row < matrix.GetLength(0); row++)
                {
                    result += matrix[row, column];
                }
                return result;
            }
        }

        public int DigitCount
        {
            get { return digitCount; }
        }

        private int Count()
        {
            int count = 0;
            foreach (char c in matrix)
            {
                if (char.IsDigit(c))
                {
                    count++;
                }
            }
            return count;
        }
    }
}
