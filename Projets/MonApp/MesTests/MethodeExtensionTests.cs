namespace MesTests;

[TestClass]
public class MethodeExtensionTests
{
    [TestMethod]
    public void TestMethod1()
    {
        var s = "Dominique aime se promener dans la nature";

        var S=s.ToUpper();

        //var S2 = s.Ellipsis(20); // => "Dominique aime se..."


   
        var S2= s.Ellipsis(20);
        // Ellipsis est une fonction d'extenstion => static dans une class static + premier param�tre avec this
        //  var S2 = MesAjouts.Ellipsis(s, 20);
    }

}


// classe static => sans possibilit� de cr�er des instance
static class MesAjouts
{
    // Fonction d'extension
    // Fonction static dans une class static
    // static => la fonction est associ�e � la classe au lieu de l'instance
    public static string Ellipsis(this string s, int n)
    {
        if (s.Length <= n)
        {
            return s;
        }
        return s.Substring(0, n - 3) + "...";
    }
}