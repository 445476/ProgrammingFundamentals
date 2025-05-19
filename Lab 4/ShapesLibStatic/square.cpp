#include <string>
#include <vector>

#include "square.h"

Square::Square() : Shape({{0,0},{0,0}, {0,0}, {0,0}})
{

}

Square::Square(vector<point> derivedPoints) : Shape(derivedPoints)
{
}

Square::Square(Square& copy) : Shape(copy)
{
}

float Square::perimeter()
{
	return(sides[0] + sides[1] + sides[2] + sides[3]);
}

float Square::area()
{
	return(sides[0] * sides[1]);
}

Square::point Square::getPoint(int point)
{
	return(points[point]);
}
float Square::getSide(int side)
{
	return(sides[side]);
}


