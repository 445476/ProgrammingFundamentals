using Homework;
using System.Numerics;

internal class Program
{
    private static void Main(string[] args)
    {
        Matrix matrix = new Matrix(new char[,] {
            { '1', 'a', 'a' },
            { '3', 'b', 'b' },
            { 'c', '4', 'c' }
        });

        Console.WriteLine(matrix[1]);
        Console.WriteLine(matrix.DigitCount);
    }
}