#pragma once
#include <string>

using namespace std;

class String {
private:
	string data;
public:
	String();
	
	String(string input);

	String(String copyable);

	String(String&& moveable);

	~String();

	string getString();
	void setString(string input);

	int measureString();
	string sortString();
};