using MesTests.POO;
using System.ComponentModel.Design.Serialization;

namespace MesTests;

[TestClass]
public class CollectionsTests
{

    [TestMethod]
    public void ListeTest()
    {
        var catalogue=new List<IVendable>();
        var c = new Chien();
        catalogue.Add(c);
        catalogue.Add(new Chiwawa());
        catalogue.Add(new Fourchette());

        // linq
        var articlesChers=catalogue.Where(c => c.Prix > 1000);

        catalogue.Remove(c); // Enleve le chien c
        catalogue.RemoveAt(1); // Enleve l'élément en 2eme position
        var chiens=catalogue.OfType<Chien>();

        var catalogueFerme = catalogue.AsEnumerable();

    }

    [TestMethod]
    public void TableauxTests()
    {
        // Déclaration d'un tableau avec 4 éléments
        int[] tab1D = new int[4]; // 0,0,0,0 => default(int)
        int[] tab1D2 = new int[] { 1, 2, 3, 4 };

        tab1D[0] = 1;
        var cell2 = tab1D[1];

        // Changement de taille impossible

        var tab1D3 = new int[6];
        tab1D.CopyTo(tab1D3, 0);

        int[,] tab2D = new int[4, 5];
        int[,,] tab3D = new int[4, 5, 6];
        int[,,,,] tab5D = new int[4, 5, 6, 4, 6];

        var l = tab5D.Length; //2880
        var lD2 = tab5D.GetLength(1); // 5
        var nbDim = tab5D.Rank; // Nombre de dimensions => 5


    }

    [TestMethod]
    public void InterfaceTest()
    {
        IVendable produit = new Chien();

        IVendable[] catalogue = new IVendable[4];
        catalogue[0] = new Chien();
        catalogue[1] = new Chiwawa();
        catalogue[2] = new Fourchette();
        // catalogue[3] = new Baleine(); pas possible

        catalogue[1].Vendre();
        if (catalogue[1] is Chien)
        {
            var nom = ((Chien)catalogue[1]).Nom;
        }


    }




    [TestMethod]
    public void EnumerationTests()
    {
        int[] tab1D = new int[] { 5, 8, 7, 5, 9, 4, 2, 8 };
        for (int i = 0; i < tab1D.Length; i++)
        {
            var e = tab1D[i];
        }

        // Enumeration des éléments du tableau
        foreach (int e in tab1D) // e => valeurs successives du tableau
        {

        }


        var enumerator = tab1D.GetEnumerator();
        // Je suis à la position -1
        while (enumerator.MoveNext())
        {
            int e = (int)enumerator.Current;
        }



        // Enumeration des caractères de la chaine
        foreach (var c in "Toto")
        {

        }

        // Where => IEnumerable 5,8,7,5,9,4,2,8 => 5,5,4,2
        // Selection non matérialisée

        // Linq => Méthodes qui sont attachées à IEnumerable et qui permettent
        // de définir ou modifier des sélections 
        var petitsElements = tab1D.Where(Filtre); //[5, 5, 4, 2]

        // Selection
        var deuxPremiers = petitsElements.Take(2);

        // Il s'agit de remplir un tableau avec les éléments correspondant à la séléction
        var tableau = deuxPremiers.ToArray(); // 5,5

        tab1D[0] = 6; // tab1D 6,8,7,5,9,4,2,8


        var sum1 = deuxPremiers.Sum(); // 6,5 => 11 => Sauvegarde la mémoire 
        var sum2 = tableau.Sum(); // 10 // Sauvegarde processeur

    }




    [TestMethod]
    public void TPIEnumerable()
    {
        int[] entiers = new int[] { 6, 1, 8, 4, 6, 9,33, 3, 10, 11, 1, 12, 4, 9, 6 };

        // Méthodes IEnumerable
        // Where (filtrage), OrderBy,OrderByDescending (ordre), Sum, Average, Count,
        // GroupBy (regroupement), Select (mapping), Distinct (Dédoublonage)
        // Skip (passer des éléments), Take (n'en retenir qu'un certain nombbre) - Pagination, 
        // % => modulo = reste de la division par 17 % 7 => 3

        // Somme des entiers pairs
        var sommeEntiersPairs = entiers.Where(c => c % 2 == 0).Sum();
        // Moyenne des 5 plus gros entiers
        var moyenne5GrosEntiers = entiers.OrderDescending().Take(5).Average();
        // Nombre d'élément dans chaque dizaine les entiers par dizaine
        var a = 19 / 10;
        var groupByDizaine = entiers
            .GroupBy(c =>c/10)
            .Select(c=>new { Dizaine=c.Key, Nombre= c.Count() })
            .OrderByDescending(c=>c.Nombre);
        // Compter les entiers >10
        var nbEntiersSup10 = entiers.Where(c => c > 10).Count();
        // double des entiers ( x 2)
        var doubleDesEntiers = entiers.Select(c => c * 2);
        // La valeur de l'entier le plus gros
        var maxEntier = entiers.Max();
        // Somme des entiers > 1000
        var sommeEntiersSup1000 = entiers.Where(c => c > 1000).DefaultIfEmpty(10).Max();

        var semection = from c in entiers orderby c select c * 2;


    }
    bool Filtre(int c)
    {
        return c < 7;
    }





}
