namespace ExpressionCalcDLL
{
    public class ManualSQRT
    {
        private double number;

        public double GetNumber()
            { return number; }

        public ManualSQRT(double value)
        {
            if (value < 0)
            {
                throw new ArgumentException($"Inacceptable square value: {value}");
            }

            if (value == 0)
            {
                number = 0;
            }
            else
            {
                number = Math.Pow(value, 0.5);
            }
        }
    }
}
