#include <iostream>
#include <stdlib.h>
using namespace std;




void coutArray1D(int* arr, int coll)
{
    for (int i = 0; i < coll; i++)
    {
        cout << arr[i] << " ";

    }
    cout << endl;
}


void coutArray2D(int** arr, int rows, int coll)
{
    for (int i = 0; i < rows; i++)
    {
        coutArray1D(arr[i], coll);
    }
    cout << endl;
}

int findMin1D(int* arr, int coll, int min)
{
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

//for task 1
void replaceMin1D(int* arr, int coll, int min)
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

void populateArray(int* arr, int coll)
{
    for (int i = 0; i < coll; i++)
    {
        arr[i] = rand() % 21 - 10;
    }
}

//i got too lazy to make other methods

int main()
{
    srand(time(0));

      {
          //task 1 find min in every arr and replace it 
          cout << "Task 1" << endl;
    
          cout << "Enter number of arrays and their size" << endl;
          int coll,
              rows;
    
          //it is advisable to add verification of input but I won't do that here(and in previous practice)
          cin >> rows >> coll;
          cout << endl;
    
          //creating arrays for rows
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
    
          //finding min and replasing it
    
          int min;
          for (int i = 0; i < rows; i++)
          {
              int min = arr[i][0];
              replaceMin1D(arr[i], coll, findMin1D(arr[i], coll, min));
          }
          cout << endl;
    
          coutArray2D(arr, rows, coll);
    
          delete[] arr;
          arr = nullptr;
      }
    
      {
          //task 2 find index of B[m] entering in A[n], replace it with mult of subsequence B[m]
          cout << "Task 2" << endl;
    
          cout << "Enter size of arrays" << endl;
          int coll;
    
          cin >> coll;
          cout << endl;
    
          int* a = new int[coll];
          int* b = new int[coll];
          int* c = new int[coll]; //temp array for storing subsequence
    
          populateArray(a, coll);
          populateArray(b, coll);
    
          //populating c with 1`s so it won't hinder mult
          for (int i = 0; i < coll; i++)
          {
              c[i] = 1;
          }
    
          cout << "Array A: " << endl;
          coutArray1D(a, coll);
    
          cout << "Array B: " << endl;
          coutArray1D(b, coll);
    
    
          int mult = 1;
    
          for (int i = 0; i < coll; i++)
          {
    
              //finding subsequence
              {
                  for (int j = 0; j < coll; j++)
                  {
                      if (a[i] == b[j])
                      {
                          c[i] = a[i];
                      }
                  }
              }
    
              //multipluying it
              {
                  mult *= c[i];
              }
    
              //changing array a
              {
                  for (int j = 0; j < coll; j++)
                  {
                      if (a[i] == b[j])
                      {
                          a[i] = mult;
                      }
                  }
              }
          }
    
          cout << endl;
    
          cout << "Changed array A: " << endl;
          coutArray1D(a, coll);
    
          cout << "Array B: " << endl;
          coutArray1D(b, coll);
    
          cout << "Control array C: " << endl;
          coutArray1D(c, coll);
    
          cout << endl;
    
          delete[] a;
          delete[] b;
          delete[] c;
          a = nullptr;
          b = nullptr;
          c = nullptr;
      }

    {
        //task 3 find max in column and change it to mult of collumn
        cout << "Task 3" << endl;

        cout << "Enter number of arrays and their size" << endl;
        int coll,
            rows;

        cin >> rows >> coll;
        cout << endl;

        int** arr = new int* [rows];

        for (int i = 0; i < rows; i++)
        {
            arr[i] = new int[coll];
        }

        for (int i = 0; i < rows; i++)
        {
            populateArray(arr[i], coll);
        }

        coutArray2D(arr, rows, coll);

        int max;
        int mult;

        //yummy spaghetti  ||
        //                 \/
        for (int i = 0; i < coll; i++)
        {
            max = arr[0][i];
            mult = 1;

            //find max
            for (int j = 0; j < rows; j++)
            {
                if (max < arr[j][i])
                {
                    max = arr[j][i];
                }
            }

            //find mult
            for (int j = 0; j < rows; j++)
            {
                mult *= arr[j][i];
            }

            //change the collumn
            for (int j = 0; j < rows; j++)
            {
                if (max == arr[j][i])
                {
                    arr[j][i] = mult;
                }
            }
        }
        cout << "New array" << endl;
        coutArray2D(arr, rows, coll);
    }
}

