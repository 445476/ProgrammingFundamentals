// Illia Kozlovets 123 lab 2.1
//

#include <iostream>

int main()
{
    { //task 1
        int arr[11];

        for (int i = 0; i < 11; i++)

        {

            if (i % 2 == 0)

            {

                arr[i] = i - 7;

            }

            else

            {

                arr[i] = 7 + 0;

            }
        }
            int size = 11;

            for (int i = 0; i < size - 1; i++)

            {

                for (int j = 0; j < size - i - 1; j++)

                {

                    if (arr[j] > arr[j + 1])

                    {

                        int temp = arr[j];

                        arr[j] = arr[j + 1];

                        arr[j + 1] = temp;

                    }

                }

            }
        }
    {//task 2
        int arr1[10],
            arr2[10],
            arr3[10];
        
        for (int i = 0; i < 10; i++)
        {
            arr1[i] = i * i + 76;
        }
        for (int i = 0; i < 10; i++)
        {
            arr2[i] = 85-i;
        }

        int k = 0; 

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                if (arr1[i] == arr2[j])

                {
                    arr3[k] = arr1[i];
                        k++;
                    break;
                }

            }

        }
        int sum = 0;
        for (int i = 0; i < k; i++)
        {
            sum += arr3[i];
        }
    }
    {//task 3
        int arr[5][4] = { { -10, 9,-8,7},
        {5,-5,3,-2, },
        {-1,2,-3,4,  },
        {6,-7,8,-9, } };

        int arr1[4];

        for (int i = 0; i < 4; i++)
        {
            int sum = 0;
            for (int j = 0; j < 5; j++)
            {
                if (arr[j][i] > 0)
                {
                    sum += arr[j][i];
                }
            }
            arr1[i] = sum;
        }

        int fArr[5][5] = { {0.1,-0.2, 0.3,-0.4,0.5},
        { 0.1,-0.2, 0.3,-0.4, 0.5 },
        { 0.1,-0.2, 0.3,-0.4, 0.5 },
        { 0.1,-0.2, 0.3,-0.4, 0.5 },
        { 0.1,-0.2, 0.3,-0.4, 0.5 } };
        int fArr1[5];

        for (int i = 0, j = 4; i < 5; i++, j--)
        {
            if (fArr[i][j] < 0)
            {
                fArr1[i] = fArr[i][j];
            }
        }
    }
}


