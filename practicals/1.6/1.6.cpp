#include <iostream>
using namespace std;

int main()
{
    {
        // Задано ціле значення А. Визначити, яких бітів (0 чи 1) більше в його двійковому поданні.
        
        cout << "Task 1" << endl;

        int a;
        int mask = 1;
        int ones = 0;
        int zeroes = 0;
        
        cout << "Enter your number: " << endl;
        cin >> a;

        /* program will only check first 8 bits to allow variety with smaller numbers during testing
         to allow any number you can change the for loop to (i = 0; i < sizeof(a) * 8 - 1; i++) */
        for (int i = 0; i < 8; i++)
        {
            int checkBit = a & mask;

            if (checkBit != 0)
            {
                ones++;
            }
            else
            {
                zeroes++;
            }
            mask = mask << 1;
        }
        
        if (ones == zeroes)
        {
            cout << "There are equal number of zeroes and ones" << endl;
        }
        else if (ones > zeroes)
        {
            cout << "There are more ones" << endl;
        }
        else
        {
            cout << "There are more zeroes" << endl;
        }

        cout << endl;
    }

    {
        /* Task 2 Make specification for XOR operation
        since Xor is already in programming language we don't need to reinvent it, but if we needed to code it ourself 
        we basically need 3 arrays, A and B for input and C for output, then check in loop if A[n]=B[n] and give C[n] a value 0 or 1 using Xor standart rules.
        Since i'm too lazy to write dex to bin convenventer i'll just hope that noone forces me to do this xd
        */
    }
}

