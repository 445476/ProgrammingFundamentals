using TextLibDLL;

internal class Program
{
    private static void Main(string[] args)
    {
        TextContainer text = new();
        text.AddLine("Hello world!");
        text.AddLine("Goodbye world.");
        text.AddLine("Hello world!");

        Console.WriteLine("Amount of chars: " + text.TotalCharacters());
        Console.WriteLine("Hello worlds: " + text.FindString("Hello world!"));

        text.ReplaceCharInText('o', '0');
        Console.WriteLine(text.GetLine(0));
        Console.WriteLine(text.GetLine(1));
        Console.WriteLine(text.GetLine(2));

    }
}