#include "ManualSQRT.h"
#include "math.h"
#include <format>
#include <iostream>
#include <string>

using namespace std;

ManualSQRT::ManualSQRT(double value)
{
    if (value < 0)
    {

        string error = "Inacceptable square value:" + to_string(value);
        throw runtime_error(error);
    }

    if (value == 0)
    {
        number = 0;
    }
    else
    {
        number = pow(value, 0.5);
    }
}

double ManualSQRT :: GetNumber()
{
    return number;
}