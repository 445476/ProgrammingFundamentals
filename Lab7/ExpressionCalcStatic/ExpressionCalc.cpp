#include "ExpressionCalc.h"
#include "ManualSQRT.h"
#include <format>
#include <iostream>

using namespace std;

    double ExpressionCalc::GetResult()
    {
        return result;
    }    
    double ExpressionCalc::CalculateExpression()
    {
        double numerator = a * b / 4 - 1;
        double innerExpression = 41 - d;
        ManualSQRT sqrt(innerExpression);
        double denominator = sqrt.GetNumber() - b * a + c;

        if (denominator == 0)
        {
            throw runtime_error("Denominator cannot be zero");
        }
        else
        {
            return numerator / denominator;
        }
    }
    ExpressionCalc::ExpressionCalc(double a1, double b1, double c1, double d1)
    {
        a = a1;
        b = b1;
        c = c1;
        d = d1;

        try
        {
            result = CalculateExpression();
        }
        catch (const runtime_error& e)
        {
            cout << ("Error: ") << e.what() << endl;
        }
    }