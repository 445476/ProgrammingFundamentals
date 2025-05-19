#include "Chars.h"

Chars::Chars(string str)
{
    data = str;
}

int Chars::Length() 
{
    return (sizeof('*') * SymbCount());
}

int Chars::SymbCount()
{
    int count = 0;
    for(char c : data)
    {
        if (c == '*') count++;
    }
    return count;
}