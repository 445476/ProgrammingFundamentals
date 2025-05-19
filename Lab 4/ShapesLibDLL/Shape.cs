using System;
using System.Collections.Generic;
using System.Drawing;


namespace ShapesLibDLL
{

    public struct point
    {
        public int X, Y;

        public int getX()
        {
            return X;
        }
        public int getY()
        {
            return Y;
        }
        public point(int x, int y)
        {
            X = x;
            Y = y;
        }
    };

    public class Shape
    {


        protected List<point> points;

        protected List<double> sides = new List<double>{0};
        public Shape()
        {
        }

        public Shape(List<point> derivedPoints)
        {
            points = derivedPoints;
            
        //   for (int i = 0; i < derivedPoints.Count; i++)
        //   {
        //       points.Add(derivedPoints[i]);
        //   }

            calculateSides();
        }

        public Shape(Shape copy)
        {
            this.points = copy.points;
            this.sides = copy.sides;
        }


        void calculateSides()
        {
            sides.Clear();
            for (int i = 0, j = 1; i < points.Count ; i++, j++)
            {

                if (i == points.Count - 1)
                {
                    sides.Add(Math.Sqrt(Math.Pow(points[0].getX() - points[i].getX(), 2) + Math.Pow(points[0].getY() - points[i].getY(), 2)));
                }
                else
                {
                    sides.Add(Math.Sqrt(Math.Pow(points[i].getX() - points[j].getX(), 2) + Math.Pow(points[i].getY() - points[j].getY(), 2)));
                }
            }
        }

    }
}
