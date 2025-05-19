namespace Lab1._1Cs.Services;

internal class Program
{
    private static void Main(string[] args)
    {
        Striing myString = new Striing("1234 5678");
        myString.getString();
        myString.sortString();
        myString.measureString();

        Striing copy = new Striing(myString);

        Striing def1 = new Striing(); 
    }
}