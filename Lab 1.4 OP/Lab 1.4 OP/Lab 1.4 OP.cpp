#include <iostream>

int main()
{
	// 1
	unsigned short wVar1;
	int iVar2;
	float fVar3;
	double dVar4;

	//2
	unsigned short *pwVar1;
	int *piVar2;
	float *pfVar3;
	double *pdVar4;
	
	//3
	void *pV;

	//4
	pwVar1 = &wVar1;
	piVar2 = &iVar2;
	pfVar3 = &fVar3;
	pdVar4 = &dVar4;
	
	//5
	*pwVar1 = 612;
	*piVar2 = -805;
	*pfVar3 = 14.4328;
	*pdVar4 = -30.22e100;
	
	//6
	int sizeOfwVar1 = sizeof(wVar1);
	int sizeOfiVar2 = sizeof(iVar2);
	int sizeOfdVar4 = sizeof(dVar4);
	int sizeOffVar4 = sizeof(fVar3);

	int sizeOfpiVar2 = sizeof(piVar2);
	int sizeOfDerefpiVar2 = sizeof(*piVar2);
	int sizeOfpV = sizeof(pV);
	
	//7
	pV = &pwVar1;

}