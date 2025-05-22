namespace MesTests;

[TestClass]
public class TPIEnumerableExtension
{
    [TestMethod]
    public void TPIEnumerableExtensionTests()
    {
        var l = new List<int>() { 1, 9, 7, 5, 4, 9, 2, 6, 2,12,11 };

        var sample = l.UnSur(3); // 1,5,2,12
                                 // Methode extension => function static dans classe static + this sur parametre 1
                                 // Generator => utiliser yield return
                                 // Genericité => ajouter anotation de type
                                 // signature => IEnumerable<T> UnSur<T>(this IEnumerable<T>,int nb)


        Assert.IsTrue(sample.SequenceEqual(new int[] { 1, 5, 2, 12 }), "Pas ok");
    }
}

public static class MyLinqExtensions
{
    public static IEnumerable<T> UnSur<T>(this IEnumerable<T> source, int nb)
    {
        int i = 0;
        foreach (var item in source) {
            if (i % nb == 0)
            {

                yield return item;
            }
            i++;
        }
    }
}
