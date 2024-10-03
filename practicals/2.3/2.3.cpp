// 2.3 Illia Kozlovets 123
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

//legacy code i'm too lazy to delete
void replaceMin1D(int* arr, int& coll, int& min)
{
    int mult = 1;
    for (int i = 0; i < coll; i++)
    {
        mult *= arr[i];
    }
    for (int i = 0; i < coll; i++)
    {
        if (arr[i] == min)
        {
            arr[i] = mult;
        }
    }
}

void populateArray(int* arr, int& coll)
{
    for (int i = 0; i < coll; i++)
    {
        arr[i] = rand() % 21 - 10;
    }
}

//fill array with 0
void spamZeroArray(int* arr, int& coll)
{
    for (int i = 0; i < coll; i++)
    {
        arr[i] = 0;
    }
}

void swap(int& a, int& b)
{
    int temp = a;
    a = b;
    b = temp;
}

//ascending bubblesort
void bubbleSort(int arr[], int n)
{
    for (int i = 0; i < n - 1; i++) {
        for (int j = 0; j < n - i - 1; j++)
        {
            if (arr[j] > arr[j + 1])
            {
                swap(arr[j], arr[j + 1]);
            }
        }
    }
}

void insertionSort(int arr[], int n)
{
    for (int i = 1; i < n; i++) {
        int element = arr[i];
        int j = i - 1;
        while (j >= 0 && (arr[j] < element)) {
            arr[j + 1] = arr[j];
            j--;
        }
        arr[j + 1] = element;
    }
}

int main()
{
    srand(time(0));
    
    {// task 1 populate arrB using neg numbers from arrA, sort it in descending order with insertion sort
        
        cout << "Lenghth of array?" << endl;
        int n;
        cin >> n;
        
        int* arr = new int[n];
        populateArray(arr, n);
        coutArray1D(arr, n);

        int* arrB;
        int n2 = 0;
        
        //trying to limit size of arrB to amount of negative numbers in arr A
        for (int i = 0; i < 2; i++)
        {
            if (i == 0)
            {
                for (int j = 0; j < n; j++)
                {
                    if (arr[j] < 0)
                    {
                        n2++;
                    }
                }
            }
            //check if arrB can be created
            else if (n2 == 0)
            {
                cout << "No negative numbers, cant populate array B" << endl;
                break;
            }
            //populating arrB and sorting it
            else
            {
                arrB = new int[n2];
                int k = 0;
                for (int j = 0; j < n; j++)
                {
                    if (arr[j] < 0)
                    {
                        arrB[k] = arr[j];
                        k++;
                    }
                }
                cout << "ArrB: " << endl;
                coutArray1D(arrB, n2);
                
                insertionSort(arrB, n2);
                
                cout << "Sorted ArrB: " << endl;
                coutArray1D(arrB, n2);
                delete arrB;
                arrB = nullptr;
            }
        }
        delete arr;
        arr = nullptr;
    }

    {//task 2 sort all arrays in 2d array by ascending using bubble
        cout << endl << endl << "Enter rows and colls of array" << endl;
        int rows;
        int coll;
        cin >> rows >> coll;

        int** arr = new int*[rows];
        
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

        //sorting array
        for (int i = 0; i < rows; i++)
        {
            bubbleSort(arr[i], coll);
        }

        cout << "Sorted array" << endl;
        coutArray2D(arr, rows, coll);

    }
}
