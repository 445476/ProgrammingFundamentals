#include <iostream>
#include "Triangle.h"

int main()
{
    Triangle triangleOne;
    Triangle triangleTwo(0, 0, 1, 1, 1, 2);
    Triangle triangleThree(triangleTwo);

    triangleTwo = triangleTwo * 2;
    triangleTwo = triangleTwo + triangleThree;
    triangleOne = triangleTwo;

    triangleOne.listAll();

   // triangleTwo = triangleTwo + triangleThree + triangleOne;
    //triangleTwo.listAll();
}

