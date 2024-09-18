// Kozlovets Illia 123

#include <iostream>

int main()
{
    {
        const char CONST1 = '1';

        char chVar1;

        chVar1 = 'p';

        char chVar2 = '№';

        const char CONST2 = 0x7a;

        char chVar3;//9

        chVar3 = 0x3b;//1

        char chVar4 = 0x9;//z
    }

    {
        //Опису змінних типів int, float, unsigned short.

        int nA;

        float fltB;

        unsigned short wC;

        //Ініціювання змінних, що описанні в п.1 даного завдання

        nA = 381;

        fltB = 954.67;

        wC = 6429;

        //Опису змінних типів double, int, char.

        double dblD;

        int nE;

        char chF;

        //Ініціювання змінних 
        // за допомогою неявного приведення типів

        dblD = nA;

        nE = fltB;

        chF = wC;

        //за допомогою явного приведення

        dblD = (double)nA;

        nE = (int)fltB;

        chF = (char)wC;

        //з обходом суворої типізації

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
