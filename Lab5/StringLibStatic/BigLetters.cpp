#include "BigLetters.h"

BigLetters::BigLetters(string str)
{
    data = str;
}

int BigLetters::Length()
{
    return (sizeof('B') * SymbCount());
}

int BigLetters::SymbCount()
{
    int count = 0;
    for (char c : data)
    {
        if (c == 'B') count++;
    }
    return count;
}