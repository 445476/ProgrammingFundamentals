using ExpressionCalcDLL;

internal class Program
{
    private static void Main(string[] args)
    {
        ExpressionCalc[] calculators = new ExpressionCalc[]
              {
            new ExpressionCalc(2, 3, 5, 6),
            new ExpressionCalc(1, 2, 1, 45) // Triggers exception
              };

        Console.WriteLine("Result: " + calculators[0].GetResult());
    }
}