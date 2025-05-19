#pragma once
#include "Strings.h"
#include <string>

using namespace std;

class Chars : public Strings
{
private:
    string data;

public:
    Chars(string str);

    int Length() override;

    int SymbCount() override;
};