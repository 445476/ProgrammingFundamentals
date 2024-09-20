/*
* Kozlovets Illia 123
* Task 1
*/
#include <iostream>;

void main()
{
        //Task
        float fA = 5.8,
            fB = 39.1;

        int nC = 70,
            nD = 42;

        bool bRes1 = !(!(int)(fA = fB) ^ (- (nC < nD))); // wtihout converting to int doesnt work
    

    
        int nA = 85,
            nB = 85;

        float fC = 6.4,
            fD = 9.3;

        bool bRes2 = !((nA = nB) ^ (-(fC < fD)));
    
   
     //Task 2
      const int A = 4;
        int B = -65,
           D = 13,
            E = 2;
        int C;
        int* pC;
        pC = &C;
        *pC = 23;

        bool rez = ((A ^ ((-B)) - *pC) >= (D + (E << sizeof(long))));
 
}