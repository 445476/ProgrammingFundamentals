#pragma once
#include <string>

using namespace std;

class Triangle{
private:

	struct point {
		int x;
		int y;
	};

	point pointOne;
	point pointTwo;
	point pointThree;

	float sideOne;
	float sideTwo;
	float sideThree;
public:
	//standart
	Triangle();

	//parametrized
	Triangle(int x1, int y1, int x2, int y2, int x3, int y3);
	
	//copy
	Triangle(Triangle& copyable);

	float findSide(point& first, point& second);

	void calculateSides();

	float perimeter();

	float area();

	void listPoint(int point);
	void listSides();
	void listAll();

	Triangle operator+(Triangle& other);
	Triangle operator+(int add);
	Triangle operator*(int mult);

	~Triangle();

};