// 2.2 Illia kozlovets 123 var 9
//

#include <iostream>

int main()
{
	{//task 1 find % of students born in winter
		enum Names { David, Tom, Andrew, Kate, Mary, Olga };

		struct Student
		{
			Names name;
			int dOB;
			int mOB;
		};

		Student myStudents[7];
		myStudents[0] = { Tom, 2, 12};
		myStudents[1] = { David, 1, 9 };
		myStudents[2] = { Kate, 2, 4 };
		myStudents[3] = { Andrew, 3, 10 };
		myStudents[4] = { Olga, 1, 1 };
		myStudents[5] = { Mary, 1, 3 };
		myStudents[6].name = Andrew;
		myStudents[6].dOB = 2;
		myStudents[6].mOB = 2;

		int count = 0;
		for (int i = 0; i < 7; i++)
		{

			if (myStudents[i].mOB == 12 || myStudents[i].mOB == 1 || myStudents[i].mOB == 2)
			{
				count++;
			}
		}
		float percent = (float)count * 100 / 7;
	}

	{//task 2 find all students with avrscore 4.5 and residency U
		struct Student
		{
			char residency;
			int yearOfStudy;
			float avgScore;
		};

		Student myStudents[7];
		myStudents[0] = { 'U', 2, 4.49};
		myStudents[1] = { 'A', 1, 5};
		myStudents[2] = { 'C', 2, 3.14};
		myStudents[3] = { 'U', 3, 4.5};
		myStudents[4] = { 'U', 1, 5.0};
		myStudents[5] = { 'B', 1, 2.28};
		myStudents[6].residency = 'T';
		myStudents[6].yearOfStudy = 2;
		myStudents[6].avgScore = 2; //he is literally me

		int count = 0;
 
		for (int i = 0; i < 7; i++)
		{
			if (myStudents[i].residency == 'U' && myStudents[i].avgScore >= 4.5)
			{
				count++;
			}
		}
		float percent = (float)count * 100 / 7;//обчислення відсотка студентів
	}
}



