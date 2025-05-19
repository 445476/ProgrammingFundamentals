using System.Runtime.InteropServices;
using StringLibDLL;

internal class Program
{
    static void DoSomething(Strings str)
    {
        Console.WriteLine("Length:");
        Console.WriteLine(str.Length());
        Console.WriteLine("Count: ");
        Console.WriteLine(str.SymbCount());
    }
    private static void Main(string[] args)
    {
        Strings chars = new Chars("*12*23*34");
        Strings letters = new BigLetters("abABaB");

        DoSomething(chars);
        DoSomething(letters);
    }
}