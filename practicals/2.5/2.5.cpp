// 2.5 Illia Kozlovets 123
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
*/

#include <iostream>
#include <ctime>
using namespace std;

string green = "\033[0;32m";
string blue = "\033[0;34m";
string reset = "\033[0m";

struct Car {
    std::string brand;
    std::string model;
    int year;
    float rental_price_per_day;
    bool is_available;
};

void clearScr()
{
    fprintf(stdout, "\033[H\033[J");
}

void input_cars(Car cars[], int &n, bool manual)
{
    if (manual)
    {
        for (int i = 0; i < n; i++)
        {
            cout << "Car " << i + 1;

            cout << "Enter brand: ";
            cin >> cars[i].brand;
            
            cout << "Enter model: ";
            cin >> cars[i].model;

            cout << "Enter year: ";
            cin >> cars[i].year;

            cout << "Enter price per day: ";
            cin >> cars[i].rental_price_per_day;

            cars[i].is_available = 1;
            cout << endl;
        }
    }
    else
    {
        string brands[5] = {"Boyota", "Lmaocar", "XInpin", "Test1", "Bobacar"};
        string models[5] = {"Tasty", "NotTasty", "McdonaldsCar", "Test2", "Mega"};

        for (int i = 0; i < n; i++)
        {
            cars[i].brand = brands[rand() % 5];
            cars[i].model = models[rand() % 5];
            cars[i].year = rand()%44 + 1980;
            cars[i].rental_price_per_day = rand() %300 + 50;
            cars[i].is_available = 1;
        }
    }
    clearScr();
}

void display_available_cars(const Car cars[], int& n)
{
    for (int i = 0; i < n; i++)
    {
        if (cars[i].is_available)
        {
            cout << "Car " << i+1 << ": "
                << green << " Brand: " << blue << cars[i].brand
                << green + " Model: " << blue << cars[i].model
                << green + " Year: " << blue << cars[i].year
                << green + " Price per day: " << blue << cars[i].rental_price_per_day << "$"
                << reset << endl;
        }
    }                                                  
}                                    

void rent_car(Car cars[], int& n)
{
    string wantedBrand;
    string wantedModel;
    int check = 0;

    cout << "Enter brand and model of the car you want to rent" << endl;
    cin >> wantedBrand >> wantedModel;

    for (int i = 0; i < n; i++)
    {
        if (cars[i].is_available && cars[i].brand == wantedBrand && cars[i].model == wantedModel)
        {
            cars[i].is_available = 0;
            cout << endl << "You succesfully rented a car!" << endl;
            break;
        }
    }
}

void return_car(Car cars[], int n)
{
    string wantedBrand;
    string wantedModel;

    cout << "Enter brand and model of the car you want to return" << endl;
    cin >> wantedBrand >> wantedModel;

    for (int i = 0; i < n; i++)
    {
        if (!cars[i].is_available && cars[i].brand == wantedBrand && cars[i].model == wantedModel)
        {
            cars[i].is_available = 1;
            cout << endl << "You succesfully returned a car!" << endl;
            break;
        }
    }
}

void find_most_expensive_available_car(const Car cars[], int n)
{
    int max = 0;
    int car;

    for (int i = 0; i < n; i++)
    {
        if (cars[i].is_available && cars[i].rental_price_per_day > max)
        {
            max = cars[i].rental_price_per_day;
            car = i;
        }
    }

    cout << "Most expensive car is: "
        << green << " Brand: " << blue << cars[car].brand
        << green + " Model: " << blue << cars[car].model
        << green + " Year: " << blue << cars[car].year
        << green + " Price per day: " << blue << cars[car].rental_price_per_day << "$"
        << reset << endl;
}

int main()
{
    srand(time(NULL));
    
    bool manual;
    int n;
    bool end = 0;


    cout << "Do you want to enter all cars by yourself?" << "\n" << "1: enter cars manually" << "\n" << "0: use pre-generated array" << endl;
    cin >> manual;
    if (manual)
    {
        cout << "How many cars do you want to add?" << endl;
        cin >> n;
    }
    else
    {
        n = 5;
    }

    Car* cars = new Car[n];

    input_cars(cars, n, manual);

    while (!end)
    {
        int choice;
        
        display_available_cars(cars, n);

        cout << "What do you want to do?" << "\n1: Rent a car \n2: Return a rented car \n3: Find most expensive available car" << endl;
        cin >> choice;

        switch (choice)
        {
        case 1:
            rent_car(cars,n);
            break;
        case 2:
            return_car(cars, n);
            break;
        case 3:
            find_most_expensive_available_car(cars, n);
            break;
        }

        cout << endl << "Do you want to continue working with program?" << endl << "1: Yes \n0: No\n";
        cin >> choice;
        if (choice == 0)
        {
            end = 1; // or just break
        }
        clearScr();
    }
}


