// 2.4 Kozlovets Illia 123
//
#include <iostream>
#include <stdlib.h>
#include <iomanip>
using namespace std;

void coutArray1D(int* arr, int& coll)
{
    for (int i = 0; i < coll; i++)
    {
        cout << setw(3) << arr[i] << " ";

    }
    cout << endl;
}


void coutArray2D(int** arr, int& rows, int& coll)
{
    for (int i = 0; i < rows; i++)
    {
        coutArray1D(arr[i], coll);
    }
    cout << endl;
}

int findMin1D(int* arr, int& coll)
{
    int min = arr[0];
    for (int i = 0; i < coll; i++)
    {
        if (arr[i] <= min)
        {
            min = arr[i];
        }
    }
    cout << "Min number is " << min << endl;
    return min;
}

void populateArray(int* arr, int& coll)
{
    for (int i = 0; i < coll; i++)
    {
        arr[i] = rand() % 21 - 10;
    }
}


int main()
{
    srand(time(0));
    

    {//task 1,2 find mult of all negative numbers in matrix, find mult of matrix trace
        cout << "Enter rows and colls of array" << endl;
        int rows;
        int coll;
        cin >> rows >> coll;

        int** arr = new int* [rows];

        for (int i = 0; i < rows; i++)
        {
            // creating arrays for columns inside rows
            arr[i] = new int[coll];
        }

        //populate arrays with numbers

        for (int i = 0; i < rows; i++)
        {
            populateArray(arr[i], coll);
        }

        coutArray2D(arr, rows, coll);

        int mult = 1,
            multTrace = 1;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < coll; j++)
            {
                if (i == j)
                {
                    multTrace *= arr[i][j];
                }
                
                if (arr[i][j] < 0)
                {
                    mult *= arr[i][j];
                }
            }
        }
        cout << "Mult of negatives: " << mult << endl;
        cout << "Mult of trace: " << multTrace << endl;

        delete arr;
        arr = nullptr;
    }
    
}
