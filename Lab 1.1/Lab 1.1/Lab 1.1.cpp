#include <iostream>
#include "String.h"


int main()
{
    String myString("12345 6789");
    myString.getString();
    myString.measureString();

    String copy(myString);
    String moveable(move(myString));


   // std::cout << (moveable.sortString());
}

