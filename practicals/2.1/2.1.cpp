// Kozlovets Illia 123 - 1
#include <iostream>
#include <stdlib.h>
#include <vector>
using namespace std;

int main()
{
    srand(time(0));

    cout << "Enter number of arrays and their size" << endl; //you can make it random(including random sizes for arrays) but for educational purposes they are static
    int coll,
        rows;
    cin >> rows >> coll;

    //creating arrays for rows
    int** arr = new int*[rows];

    for (int i = 0; i < rows; i++) {

        // creating arrays for columns inside rows
        arr[i] = new int[coll];
    }

    //populate arrays with numbers

    for (int i = 0; i < rows; i++)
    {
        for (int j = 0; j < coll; j++)
        {
            arr[i][j] = rand() % 21 - 10;
        }
    }

    //cout the array 
    for (int i = 0; i < rows; i++)
    {
        for (int j = 0; j < coll; j++)
        {
            cout << arr[i][j] << " ";
        }
        cout << endl;
    }

    {
        //task 1. Find indexes of lowest numbers in array
        cout << endl;
        cout << "Task 1" << endl;

        int min = arr[0][0];
        
        // finding min
        for (int i = 0; i < rows; i++)
        {
           for (int j = 0; j < coll; j++)
           {
               if (arr[i][ j] <= min)
               {
                   min = arr[i][j];
               }
           }
        }
        cout << "Lowest number: " << min << endl;

        //finding indexes
        cout << "Indexes of lowest numbers are: " << endl;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < coll; j++)
            {
                if (min == arr[i][j])
                {
                    cout << "A[" << i << "," << j << "] ";
                }
            }
        }
        cout << endl;
    }

    {
        // Task 2 find max negative in array
        cout << endl;
        cout << "Task 2" << endl;
        int maxNeg = ~1;
        // finding and couting max negative
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < coll; j++)
            {
                if (arr[i][j] > maxNeg && arr[i][j] < 0)
                {
                    maxNeg = arr[i][j];
                }
            }
        }
        cout << "Biggest negative number: " << maxNeg << endl;
    }

    {
        // Task 2 find mmin positive in array
        cout << endl;
        cout << "Task 3" << endl;
        int minPos = (unsigned int)arr[0][0];
        // finding and couting max negative
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < coll; j++)
            {
                if (arr[i][j] < minPos && arr[i][j] > 0)
                {
                    minPos = arr[i][j];
                }
            }
        }
        cout << "Smallest positive number: " << minPos << endl;
    }
}

