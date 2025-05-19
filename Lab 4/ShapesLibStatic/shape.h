#pragma once
#include <string>
#include <vector>

using namespace std;

class Shape {
protected:
    struct point {
		int x;
		int y;
	};

	vector<point> points;

	vector<float> sides;


public:



	//standart
	Shape();

	//parametrized
	Shape(vector<point> derivedPoints);

	Shape(Shape& copy);

	void calculateSides();



};