using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_3_Cs.Resources
{
    internal class Triangle
    {
        private struct point
        {
            int x;
            int y;

            public void setX(int num)
            {
                x = num;
            }

            public void setY(int num)
            {
                y = num;
            }

            public int getX()
            {
                return x;
            }
            public int getY()
            {
                return y;
            }
        }

        private point[] points = new point[3];
        private double[] sides = new double[3];

        public Triangle()
        {

        }

        public Triangle(int x1, int y1, int x2, int y2, int x3, int y3)
        {
            points[0].setX(x1);
            points[0].setY(y1);

            points[1].setX(x2);
            points[1].setY(y2);

            points[2].setX(x3);
            points[2].setY(y3);

            calcSides();
        }

        public Triangle(Triangle copy)
        {
            points[0].setX(copy.points[0].getX());
            points[0].setY(copy.points[0].getY());

            points[1].setX(copy.points[1].getX());
            points[1].setY(copy.points[1].getY());

            points[2].setX(copy.points[2].getX());
            points[2].setY(copy.points[2].getY());

            calcSides();
        }

        private void calcSides()
        {
            // Square root of (x1-x0)^2+(y1-y0)^2
            sides[0] = Math.Sqrt(Math.Pow(points[1].getX() - points[0].getX(), 2) + Math.Pow(points[1].getY() - points[0].getY(), 2));
            sides[1] = Math.Sqrt(Math.Pow(points[2].getX() - points[1].getX(), 2) + Math.Pow(points[2].getY() - points[1].getY(), 2));
            sides[2] = Math.Sqrt(Math.Pow(points[0].getX() - points[2].getX(), 2) + Math.Pow(points[0].getY() - points[2].getY(), 2));
        }

        private double perimeter()
        {
            return sides[0] + sides[1] + sides[2];
        }

        private double area()
        {
            double p = perimeter() / 2;
            return Math.Sqrt((p * (p - sides[0]) * (p - sides[1]) * (p - sides[2])));
        }

        public void listPoint(int point = 4)
        {
            if (point > 4 || point < 0)
            {
                point = 4;
            }

            if (point == 4)
            {
                for (int i = 0; i < points.Length; i++)
                {
                    Console.WriteLine("Point " + (i + 1) + ": X=" + points[i].getX() + " Y= " + points[i].getY());
                }
            }    

            else
            {
                Console.WriteLine("Point " + point + ": X=" + points[point-1].getX() + " Y= " + points[point-1].getY());
            }
            
        }

        public void listPoint(int point, bool isX)
        {
            if (point > 4 || point < 0)
            {
                point = 4;
            }

            if (point == 4)
            {
                for (int i = 0; i < points.Length; i++)
                {
                    Console.WriteLine("Point " + (i + 1) + ": X=" + points[i].getX() + " Y= " + points[i].getY());
                }
            }

            else
            {
                Console.WriteLine("Point " + point + ": X=" + points[point - 1].getX() + " Y= " + points[point - 1].getY());
            }
        }

        public void listSides()
        {
            for (int i = 0; i < sides.Length; i++)
            {
                Console.WriteLine("Side " + (i+1) + ": " + sides[i]);
            }    
        }

        public void listSides(int i)
        {
            Console.WriteLine("Side " + i + ": " + sides[i-1]);
        }

        public void listAll()
        {
            listPoint();
            listSides();
            Console.WriteLine("Perimeter: " + perimeter());
            Console.WriteLine("Area: " + area());
        }

        public static Triangle operator +(Triangle a, Triangle b)
        {
            for(int i = 0; i < 3; i++)
            {
                a.points[i].setX(a.points[i].getX() + b.points[i].getX());
                a.points[i].setY(a.points[i].getY() + b.points[i].getY());
            }

            a.calcSides();
            
            return a;
        }

        public static Triangle operator *(Triangle a, int mult)
        {
            for (int i = 0; i < 3; i++)
            {
                a.points[i].setX(a.points[i].getX() * mult);
                a.points[i].setY(a.points[i].getY() * mult);
            }

            a.calcSides();

            return a;
        }
    }
}
