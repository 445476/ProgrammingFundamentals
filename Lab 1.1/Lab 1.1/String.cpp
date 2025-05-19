#include "String.h"

#include <iostream>
#include <string>

using namespace std;

// empty
String::String()
{
}

// parametrized
String::String(string input)
{
	setString(input);
}

// copy 
String::String(String& copyable)
{
	setString(copyable.data);
}

//move
String::String(String&& moveable)
{
	setString(moveable.data);
	moveable.setString(" ");
}

//destructor
String::~String()
{
	std::cout << "\n Destructor called \n";
}

string String::getString()
{
	return data;
}

void String::setString(string input)
{
	data = input;
}

//can be done using size
int String::measureString()
{
	int i = 0;

	for (char c : data)
	{
		i++;
	}
	return i;
}

string String::sortString()
{
	string sorted = data;

	for (int i = 0; i < sorted.size() - 1; i++)
	{
		short first = 0;

		for (int j = 0; j < sorted.size() - 1 - i; j++)
		{			
			if (sorted[first] < sorted[first+1])
			{
				//can be done with swap
				char temp;
				temp = sorted[first];
				sorted[first] = sorted[first+1];
				sorted[first+1] = temp;

				first++;
			}
			else
			{
				first++;
			}
		}
	}
	return sorted;
}