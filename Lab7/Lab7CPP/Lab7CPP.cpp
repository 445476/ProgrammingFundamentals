#include "../ExpressionCalcStatic/pch.h"
#include <iostream>
#include "../ExpressionCalcStatic/ExpressionCalc.h";
#include "../ExpressionCalcStatic/ManualSQRT.h";

using namespace std;

int main()
{
    ExpressionCalc one = ExpressionCalc(2, 3, 5, 6);
    ExpressionCalc two = ExpressionCalc(1, 2, 1, 45); // Triggers exception

    cout << one.GetResult();
}


