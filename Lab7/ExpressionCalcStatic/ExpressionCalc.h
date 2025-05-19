#pragma once

class ExpressionCalc
{
private:
    double a, b, c, d;
    double result;

public:
    double GetResult();
    ExpressionCalc(double a, double b, double c, double d);
    double CalculateExpression();
};