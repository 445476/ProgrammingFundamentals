// RGR Illia Kozlovets 123
// Create a scanner that check strings with alphabet "0..5, A..M, =" and consists of two parts where first part is a sequence of letter A-M and second is
// a sequence of digits separated by =, e.g. ABC1=2=3

#include <iostream>
#include <string>
using namespace std;

string inputLine()
{
    cout << "Enter string: ";
    string line;
    getline(cin, line);
    return line;
}

bool scanLine(const string& line)
{
    bool result = true;
    bool firstString = true;
    bool nextChar;

    for (int i = 0; i < line.length(); i++)
    {
        if (result)
        {
            nextChar = line[i + 1] == 0x3d || i == line.length() - 1;
            switch (firstString)
            {
            case true:
                result = line[i] >= 0x41 && line[i] <= 0x4d; //checking if letter a-m

                if (isdigit(line[i]) && line[i] >= 0x30 && line[i] <= 0x35 && nextChar) //checking if digit from 1 to 5 and if it's last digit or after it comes =
                {
                    result = true;
                    firstString = false;
                }
                continue;
            
            case false:      
                result = isdigit(line[i]) && line[i] >= 0x30 && line[i] <= 0x35 && nextChar; //check for digit and =
                    
                    if (line[i] == 0x3d && i < line.length()-1) //checking for =
                    {
                        result = true;
                    }
                continue;
            }
        }
        else
        {
            break;
        }
    }

    return result;
}

int main()
{
        cout << scanLine(inputLine()) << endl;
}

