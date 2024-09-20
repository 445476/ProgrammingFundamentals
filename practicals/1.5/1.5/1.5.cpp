#include <iostream>
#include <cstdlib>
using namespace std;

void initRandomizer()
{
    srand(time(NULL));
}

int getP()
{
    int p;
    cout << "Enter number P from 0 to 10 that you want to find: " << endl;
    cin >> p;

    //limiting numbers to a specific range, in this instance to range from 0 to 10 for easier testing
    if (p > 10 || p < 0)
    {
        cout << "P is out of range" << endl;
        getP();
    }
    return p;
}

int main()
{
   {
       //Given array A[n] and variable P find index of first appearance of P in array.
 
       cout << "Task 1" << endl;
 
       int n;
 
       cout << "Enter number of iterations: " << endl;
       cin >> n;

       int p = getP();
 
       int* arr = new int[n];
 
       //generate array
       for (int i = 0; i < n; i++)
       {
           arr[i] = rand() % 10;
       }
 
       //cout the array
       for (int i = 0; i < n; i++) {
           cout << arr[i] << " ";
       }
       cout << endl;
 
       //find p in array
       for (int i = 0; i<n; i++)
       {
           if (arr[i] == p)
           {
               cout << "Index of P = " << i+1 <<endl;
               break;
           }
           else if(i == n-1)
           {
               cout << "There is no P in array." << endl;
               break;
           }
       }
       delete[] arr;
       arr = nullptr;
       cout << " " << endl;
   }
   
   {
       //Given array A[n] find lowest positive number in it.
 
       cout << "Task 2" << endl;
       
       int n;
 
       cout << "Enter number of iterations: " << endl;
       cin >> n;
 
       int* arr = new int[n];
       int arrMin = arr[0];
 
       //generate array
       for (int i = 0; i < n; i++)
       {
           arr[i] = (rand() % 200) - 100; //substracting 100 to allow random generation of negative numbers
       }
 
       //cout the array
       for (int i = 0; i < n; i++) 
       {
           cout << arr[i] << " ";
       }
       cout << endl;
 
       //finding min positive number
       for (int i = 0; i < n; i++)
       {
           //check if arrmin is positive
           if(arrMin < 0)
           {
               arrMin = arr[i];
           }
           
           //change min number
           if (arr[i] > 0 && arrMin > arr[i])
           {
               arrMin = arr[i];
           }
       }
 
       cout << "Lowest positive number: "<< arrMin << endl;
       cout << " " << endl;
 
       delete[] arr;
       arr = nullptr;
   }

    {
        //Given array A[n] find lowest and biggest number and switch places between them.
        cout << "Task 3" << endl;

        initRandomizer();

        int n;

        cout << "Enter number of iterations: " << endl;
        cin >> n;

        int* arr = new int[n];
        int minIndex = 0;
        int maxIndex = 0;

        //generate array
        for (int i = 0; i < n; i++)
        {
            arr[i] = (rand() % 200) - 100; //substracting 100 to allow random generation of negative numbers
        }
        
        int arrMin = arr[0];
        int arrMax = arr[0];

        //cout the array
        for (int i = 0; i < n; i++)
        {
            cout << arr[i] << " ";
        }
        cout << endl;

        //finding min and max number
        for (int i = 1; i < n; i++)
        {

            //change min number
            if (arrMin > arr[i])
            {
                arrMin = arr[i];
                minIndex = i;
            }
            
            if (arrMax < arr[i])
            {
                arrMax = arr[i];
                maxIndex = i;
            }
        }
            //swapping numbers
            arr[minIndex] = arrMax;
            arr[maxIndex] = arrMin;
            
            //cout the array again
            for (int i = 0; i < n; i++)
            {
                cout << arr[i] << " ";
            }
            cout << endl;
            
            delete[] arr;
            arr = nullptr;
    }
}