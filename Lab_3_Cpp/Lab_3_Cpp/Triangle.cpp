#include "Triangle.h"

#include <iostream>

#include <cmath>

using namespace std;

Triangle::Triangle()
{
}

Triangle::Triangle(int x1, int y1, int x2, int y2, int x3, int y3)
{
	pointOne.x = x1;
	pointOne.y = y1;

	pointTwo.x = x2;
	pointTwo.y = y2;

	pointThree.x = x3;
	pointThree.y = y3;

	calculateSides();
}

Triangle::Triangle(Triangle& copyable)
{
	pointOne = copyable.pointOne;
	pointTwo = copyable.pointTwo;
	pointThree = copyable.pointThree;

	sideOne = copyable.sideOne;
	sideTwo = copyable.sideTwo;
	sideThree = copyable.sideThree;

}

float Triangle::findSide(point& first, point& second)
{
	return(sqrt(pow(second.x - first.x, 2) + pow(second.y - first.y, 2)));
}

void Triangle::calculateSides()
{
	sideOne = findSide(pointOne, pointTwo);
	sideTwo = findSide(pointTwo, pointThree);
	sideThree = findSide(pointThree, pointOne);
}

float Triangle::perimeter()
{
	return(sideOne + sideTwo + sideThree);
}

float Triangle::area()
{
	float p = perimeter()/2;
	return(sqrt(p*(p - sideOne)*(p - sideTwo)*(p - sideThree)));
}

void Triangle::listPoint(int point = 4)
{
	if (point > 4 || point < 0)
	{
		point = 4;
	}
	switch (point)
	{
	case 1:
		cout << "Point one: X = " << pointOne.x << " Y = " << pointOne.y << "\n";
		break;
	case 2:
		cout << "Point two: X = " << pointTwo.x << " Y = " << pointTwo.y << "\n";
		break;
	case 3:
		cout << "Point three: X = " << pointThree.x << " Y = " << pointThree.y << "\n";
		break;
	case 4:
		cout << "Point one: X = " << pointOne.x << " Y = " << pointOne.y << "\n" 
			<< "Point two: X = " << pointTwo.x << " Y = " << pointTwo.y << "\n" 
			<< "Point three: X = " << pointThree.x << " Y = " << pointThree.y << "\n";
	}
}

void Triangle::listSides()
{
	cout << "Side one = " << sideOne << "\n"
		<< "Side two = " << sideTwo << "\n"
		<< "Side three = " << sideThree << "\n";
}

void Triangle::listAll()
{
	listPoint();
	listSides();
	cout << "Perimeter = " << perimeter() << "\n"
		<< "Area = " << area() << "\n";
}

Triangle Triangle::operator+(Triangle& other)
{
	Triangle res;
	res.pointOne.x = pointOne.x + other.pointOne.x;
	res.pointOne.y = pointOne.y + other.pointOne.y;

	res.pointTwo.x = pointTwo.x + other.pointTwo.x;
	res.pointTwo.y = pointTwo.y + other.pointTwo.y;

	res.pointThree.x = pointThree.x + other.pointThree.x;
	res.pointThree.y = pointThree.y + other.pointThree.y;

	res.calculateSides();

	return res;
}

Triangle Triangle::operator+(int add)
{
	Triangle res;
	res.pointOne.x = pointOne.x + add;
	res.pointOne.y = pointOne.y + add;

	res.pointTwo.x = pointTwo.x + add;
	res.pointTwo.y = pointTwo.y + add;

	res.pointThree.x = pointThree.x + add;
	res.pointThree.y = pointThree.y + add;

	res.calculateSides();

	return res;
}

Triangle Triangle::operator*(int mult)
{
	Triangle res;

	
	res.pointOne.x = pointOne.x* mult;
	res.pointOne.y = pointOne.y* mult;
				 
	res.pointTwo.x = pointTwo.x* mult;
	res.pointTwo.y  = pointTwo.y* mult;
				  
	res.pointThree.x = pointThree.x* mult;
	res.pointThree.y = pointThree.y * mult;

	res.calculateSides();

	return res;
}

//destructor
Triangle::~Triangle()
{	
}
