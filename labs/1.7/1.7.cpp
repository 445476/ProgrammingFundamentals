#include <iostream>

static int sA = 12; //global variable
static int sB;

int main()
{
    sB = 10;
    {
        int nA = 5;
        static int sExample = 3;
        nA = sA * sB;
    }
    // sExample not available out of block

    sA += 10;
    sB++;
    {
        // nA not available
        int nB = 4;
        {
            nB = 2;
            int nC = 7;
        }
        // nC not available
    }

    float fltk = 20; //dynamic memory distribution in stack
    int nL = 0;
    for (int i = 0; i<5; i++)
    {
        static int nF = 0;
        nF++;
        int nS = 0;
        nS++;
        nL++;
    }

    //operator that lets enter the code multiple times
    for (int i = 0; i < 5; i = i + 1)
    {

        static int iA = 0;

        int iB = 0;

        iA = iA + 1;

        iB = iB + 1;

    }

    int* pI; //initiating typed pointer
    pI = new int;
    *pI = 25;
    delete pI; // returning memory

    int* pW; //initiating typed pointer
    pW = new int;
    pW = pI; //making garbage
    *pW = 25;
    delete pW;
    }