// Kozlovets Illia 123

#include <iostream>

int main()
{
    {
        const char CONST1 = '1';

        char chVar1;

        chVar1 = 'p';

        char chVar2 = '¹';

        const char CONST2 = 0x7a;

        char chVar3;//9

        chVar3 = 0x3b;//1

        char chVar4 = 0x9;//z
    }

    {
        //Desc of vars: int, float, unsigned short

        int nA;

        float fltB;

        unsigned short wC;

        //Init of above vars

        nA = 381;

        fltB = 954.67;

        wC = 6429;

        //Desc of vars double int char.

        double dblD;

        int nE;

        char chF;

        //Init of vars
        // using coercion

        dblD = nA;

        nE = fltB;

        chF = wC;

        //using casting

        dblD = (double)nA;

        nE = (int)fltB;

        chF = (char)wC;

        //bypassing strong typisation

        double* pdblD;

        void* pV;

        pV = &nA;

        pdblD = (double*)pV;

        dblD = *pdblD;

        int* pnE;

        pV = &fltB;

        pnE = (int*)pV;

        nE = *pnE;

        char* pchF;

        pV = &wC;

        pchF = (char*)pV;

        chF = *pchF;
    }
}
