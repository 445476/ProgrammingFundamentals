#pragma once
#include "Strings.h"
#include <string>

using namespace std;

class BigLetters : public Strings
{
private:
    string data;

public:
    BigLetters(string str);

    int Length() override;

    int SymbCount() override;
};