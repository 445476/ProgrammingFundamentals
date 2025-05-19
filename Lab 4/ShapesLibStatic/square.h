#pragma once
#include <string>
#include <vector>

#include "shape.h"

class Square : public Shape {
public:
	
	//def
	Square();

	//parametrized
	Square(vector<point> derivedPoints);

	//Copy
	Square(Square& copy);

	float perimeter();
	float area();
	point getPoint(int point);
	float getSide(int side);

};