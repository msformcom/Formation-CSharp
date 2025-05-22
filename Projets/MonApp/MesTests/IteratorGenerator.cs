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
            // Arr�t par le g�n�rateur
            if (i > 100)
            {
                break;
            }
            i += 2;
        }
    }
}
