#include "shape.h"

Shape::Shape()
{
}

Shape::Shape(vector<point> derivedPoints)
{	
	points.resize(derivedPoints.size());

	for (int i = 0; i < derivedPoints.size(); i++)
	{
		points[i] = derivedPoints[i];
	}

	calculateSides();
}

Shape::Shape(Shape & copy)
{
	this->points = copy.points;
	this->sides = copy.sides;
}


void Shape::calculateSides()
{
	sides.resize(points.size());

	for (int i = 0, j = 1; i < sides.size() ; i++, j++)
	{
		if (i == sides.size()-1)
		{
			sides[i] = sqrt(pow(points[0].x - points[i].x, 2) + pow(points[0].y - points[i].y, 2));
		}
		else
		{
			sides[i] = sqrt(pow(points[i].x - points[j].x, 2) + pow(points[i].y - points[j].y, 2));
		}
	}
}

