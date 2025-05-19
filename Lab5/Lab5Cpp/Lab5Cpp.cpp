#include <iostream>
#include "../StringLibStatic/BigLetters.h";
#include "../StringLibStatic/Chars.h";


static void DoSomething(Strings* str)
{
    cout << "Length: ";
    cout << str->Length() << endl;
    cout << "Count: ";
    cout << str->SymbCount() << endl;
}

int main()
{
  //Strings myChars = Chars("*12*23*34");
  //Strings myBigLetters = BigLetters ("abABaB");
    BigLetters myBigLetters("abABaB");
    Chars myChars("*12*23*34");
   
    DoSomething(&myChars);
    DoSomething(&myBigLetters);
}

