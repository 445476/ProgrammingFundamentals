/*
Black   \033[0;30m
Red     \033[0;31m
Green   \033[0;32m
Yellow  \033[0;33m
Blue    \033[0;34m
Magenta \033[0;35m
Cyan    \033[0;36m
White   \033[0;37m
Reset   \033[0m

Clearing console printf("\033[H\033[J");

256 Colors
("\033[38;5;%dm) where %d is number from 0 to 256
*/


// README right now project generates christmas tree and animates it. It is not writing it to txt and toys placement is kinda bad
// when i finished this project i got the gist of what i should have done, but i don`t have time to rewrite the project now, maybe i`ll revise it later

#include <iostream>
#include <windows.h>
#include <ctime>
using namespace std;

const int STEP = 2;
const int STEPROW = 3;
const int MAX = 30;


void print_color_table() //gpt-generated function to show all available colors
{
    for (int i = 0; i < 256; i++) {
        printf("\033[38;5;%dm %3d \033[0m", i, i);  // Print color code and its value
        if ((i + 1) % 16 == 0) {
            printf("\n");  // Newline after every 16 colors
        }
    }
}

//populates array with numbers
void generateTree(int** arr, int& levels)
{
    int mid = (levels * STEP + STEPROW) / 2;
    int midl = mid;
    int midr = mid;

    for (int i = 0; i < levels + STEP; i++)
    {

        for (int j = 0; j < levels * STEP + STEPROW; j++)
        {
            if (j == midl)
            {
                for (int k = 0; k <= (midr - midl); k++, j++)
                {
                    arr[i][j] = (rand() % MAX) + 1; //0 = space, 1 - chance-4 = *, else it`s a toy
                }
                j--;
            }
            else
            {
                arr[i][j] = 0;
            }
        }
        midl--;
        midr++;
    }
}

//translates numbers from array to chars(why you didn't use chars at the start you stoopid
void coutTriangle(int** arr, int& levels, int& block)
{

    for (int i = 0; i < block + STEP; i++)
    {

        for (int j = 0; j < levels * STEP + STEPROW; j++)
        {
            int n;
            if (arr[i][j] == 0)
            {
                printf(" ");
            }
            else if (arr[i][j] > 0 && arr[i][j] <= MAX-4)
            {
                printf("\033[0;32m" "*" "\033[0m");
            }
            else if (arr[i][j] == MAX-3)
            {
                n = rand() % 7 + 1;
                if (n == 2) //toy cant be green
                {
                    n += 1;
                }
                printf("\033[0;3%dm" "@" "\033[0m", n);
            }
            else if (arr[i][j] == MAX-2)
            {
                n = rand() % 7 + 1;
                if (n == 2)
                {
                    n += 1;
                }
                printf("\033[0;3%dm" "#" "\033[0m", n);
            }
            else if (arr[i][j] == MAX-1)
            {
                n = rand() % 7 + 1;
                if (n == 2)
                {
                    n += 1;
                }
                printf("\033[0;3%dm" "$" "\033[0m", n);
            }
            else
            {
                n = rand() % 7 + 1;
                if (n == 2)
                {
                    n += 1;
                }
                printf("\033[0;3%dm" "&" "\033[0m", n);
            }
        }
        cout << endl;
    }
    block++;
}

//couts the array in specific pattern
void coutTree(int** arr, int& levels)
{
    int block = 1;

    for (int i = 0; i < levels; i++)
    {
        coutTriangle(arr, levels, block);
    }

    block = 1;

    for (int i = 0; i < 2; i++)
    {
        for (int j = 0; j < levels * STEP + STEPROW; j++)
        {
            if (arr[0][j] != 0)
            {
                printf("\033[0;33m" "*" "\033[0m");
            }
            else
            {
                printf(" ");
            }

        }
        printf("\n");
    }

}

//animation
void anim(int** arr, int& levels)
{
    while (1) //infinite loop for avoiding recursion
    {
        coutTree(arr, levels);

        Sleep(1000);

        system("cls");
    }
}


int main()
{
    srand(time(NULL));
    
    int levels;
    cin >> levels;

    system("cls");
    
    //this array is the reason why it`s not so easy to rewrite, basically the tree is just one block that repeats to certain point then it repeats again
    // i could`ve just make one big array...
    int** arr = new int* [2 + levels];

    for (int i = 0; i < levels + 2; i++)
    {
        arr[i] = new int[levels *2 + 3];
    }
    
    generateTree(arr, levels);

    anim(arr, levels);
}