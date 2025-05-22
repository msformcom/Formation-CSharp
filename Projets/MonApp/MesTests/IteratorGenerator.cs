namespace MesTests;

[TestClass]
public class IteratorGenerator
{
    [TestMethod]
    public void IterationTests()
    {
        foreach(var e in NombrePairs())
        {
            int a = e;
            if (e > 50)
            {
                break;
            }
        }
    }

    public IEnumerable<int> NombrePairs()
    {
        int i = 0;
        while (true)
        {
            yield return i;
            // Arrêt par le générateur
            if (i > 100)
            {
                break;
            }
            i += 2;
        }
    }
}
