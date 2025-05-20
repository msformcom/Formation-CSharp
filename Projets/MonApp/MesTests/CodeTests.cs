namespace MesTests;

[TestClass]
public class CodeTests
{
    [TestMethod]
    public void CheckedTests()
    {
        unchecked
        {
            // Attention au dépassement de capacité
            int a = int.MaxValue;
            a++;
        }
        checked
        {
            // dépassement de capacité entraine une Exception
            int a = int.MaxValue;
            a++;
        }
    }
    [TestMethod]
    public void InfinityTests()
    {
        // Infinity / -infinity, +0, -0 valeurs double
        double d = 0;
        double e = 1 / d;
        double f = 1 / e;

        double g = -0D;
        e = 1 / g;

        // NaN (double seulement) pour identifier les valeurs incalculables
        var sqr = Math.Sqrt(-4D);


    }


    [TestMethod]
    public void CastingTests()
    {
        int a = 0;
        // Pas de perte de valeur
        double b = double.MaxValue;

        if (a == b)
        {

        }

        // perte de valeur double => int
        // (int) => cast => permet de prendre conscience de la perte possible
        // en fonction de checked / unchecked => erreur masquée ou pas
        a = (int)b;


        decimal dec = 1.3M;
        double dou = 1.353D;
        // comparaison double decimal nécéssite un cast car leur nature est différente
        if ((decimal)dou > dec-0001 && (decimal)dou < dec + 0001)
        {

        }



    }



}
