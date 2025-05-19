using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ExpressionCalcDLL
{
    public class ExpressionCalc
    {
        private double a,b,c,d;
        private double result;

        public double GetResult()
        { return result; } 
        public ExpressionCalc(double a, double b, double c, double d)
        {
            this.a = a;
            this.b = b;
            this.c = c;
            this.d = d;

            try
            {
                result = this.CalculateExpression();
            }
            catch (ArgumentException exception)
            {
                Console.WriteLine("Error: " + exception.Message);
            }
        }

        public double CalculateExpression()
        {
            double numerator = a * b / 4 - 1;
            double innerExpression = 41 - d;
            ManualSQRT sqrt = new ManualSQRT(innerExpression);
            double denominator = sqrt.GetNumber() - b*a + c;

            if (denominator == 0)
            {
                throw new ArgumentException("Denominator cannot be zero");
            }
            else
            {
                return numerator / denominator;
            }
        }
    }
}
