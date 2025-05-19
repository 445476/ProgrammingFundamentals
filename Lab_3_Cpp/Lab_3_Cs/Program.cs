using Lab_3_Cs.Resources;

internal class Program
{
    private static void Main(string[] args)
    {
        Triangle T1 = new Triangle();
        Triangle T2 = new Triangle(0,0,1,1,1,2);
        Triangle T3 = new Triangle(T2);

        T2 = T2 * 2;
        T2 = T2 + T3;
        T1 = T2;

        T1.listAll();

       // T2 = T2 + T3 + T1;
       // T2.listAll();

       // T2.listPoint(isX: true, point: 4);
    }
}