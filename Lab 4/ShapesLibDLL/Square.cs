using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapesLibDLL
{
    public class Square : Shape
    {
        public Square() : base(new List<point> { new point(0, 0), new point(0, 0), new point(0, 0), new point(0, 0) }) 
        { 
        
        }
        public Square(point p1, point p2, point p3, point p4) : base(new List<point> {p1,p2,p3,p4}) 
        { 
        
        }

        public Square(Square copy) : base(copy)
        {

        }

        public double perimeter()
        {
            return (sides[0] + sides[1] + sides[2] + sides[3]);
        }

        public double area()
        {
            return (sides[0] * sides[1]);
        }

        public point getPoint(int point)
        {
            return (points[point]);
        }
        public double getSide(int side)
        {
            return (sides[side]);
        }
    }
}
