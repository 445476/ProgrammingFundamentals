using ShapesLibDLL;
using System.Drawing;
using System.Reflection;

namespace ShapesLibDLL;

internal class Program
{
    private static void Main(string[] args)
    {
        Square mySquare = new Square(new point(1, 1 ),new point( 2, 1 ),new point( 2, 2 ),new point( 1, 2) );
        Console.WriteLine("Point One: X: " + mySquare.getPoint(0).X + " Y: " + mySquare.getPoint(0).Y);
        Console.WriteLine("Side four: " + mySquare.getSide(1));
        Console.WriteLine("Perimeter: " + mySquare.perimeter());
        Console.WriteLine("Area " + mySquare.area());

        Square mySquare2 = new Square(mySquare);
        Square baseSquare = new Square();
    }
}