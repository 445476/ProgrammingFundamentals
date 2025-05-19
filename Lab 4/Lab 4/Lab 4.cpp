#include <iostream>
#include <vector>

#include "../ShapesLibStatic/square.h";


int main()
{
	Square mySquare({ { 1,1 }, { 2,1 }, { 2,2 }, { 1,2 } });
    cout << "Point one " << mySquare.getPoint(0).x << ", " << mySquare.getPoint(0).y << endl;
	cout << "Point two " << mySquare.getPoint(1).x << ", " << mySquare.getPoint(1).y << endl;
	cout << "Point three " << mySquare.getPoint(2).x << ", " << mySquare.getPoint(2).y << endl;
	cout << "Point four " << mySquare.getPoint(3).x << ", " << mySquare.getPoint(3).y << endl;

	cout << "Side one: " << mySquare.getSide(3) << endl;
	cout << "Perimeter: " << mySquare.perimeter() << endl;
	cout << "Area: " << mySquare.area() << endl;

	Square mySquare2(mySquare);
	Square baseSquare;
}