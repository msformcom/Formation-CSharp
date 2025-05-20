using MesTests.POO;
using System.ComponentModel.Design.Serialization;

namespace MesTests;

[TestClass]
public class CollectionsTests
{
    [TestMethod]
    public void TableauxTests()
    {
        // D�claration d'un tableau avec 4 �l�ments
        int[] tab1D = new int[4]; // 0,0,0,0 => default(int)
        int[] tab1D2 = new int[] { 1, 2, 3, 4 };

        tab1D[0] = 1;
        var cell2 = tab1D[1];

        // Changement de taille impossible

        var tab1D3 = new int[6];
        tab1D.CopyTo(tab1D3, 0);

        int[,] tab2D = new int[4, 5];
        int[,,] tab3D = new int[4, 5, 6];
        int[,,,,] tab5D = new int[4, 5, 6,4,6];

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
        int[] tab1D = new int[] { 5,8,7,5,9,4,2,8};
        for (int i = 0; i < tab1D.Length; i++)
        {
            var e = tab1D[i];
        }

        // Enumeration des �l�ments du tableau
        foreach(int e in tab1D) // e => valeurs successives du tableau
        {

        }


        var enumerator = tab1D.GetEnumerator();
        // Je suis � la position -1
        while (enumerator.MoveNext())
        {
            int e =(int) enumerator.Current;
        }



        // Enumeration des caract�res de la chaine
        foreach (var c in "Toto")
        {

        }

        // Where => IEnumerable 5,8,7,5,9,4,2,8 => 5,5,4,2
        // Selection non mat�rialis�e

        // Linq => M�thodes qui sont attach�es � IEnumerable et qui permettent
        // de d�finir ou modifier des s�lections 
        var petitsElements = tab1D.Where(Filtre); //[5, 5, 4, 2]

        // Selection
        var deuxPremiers = petitsElements.Take(2);

        // Il s'agit de remplir un tableau avec les �l�ments correspondant � la s�l�ction
        var tableau = deuxPremiers.ToArray(); // 5,5

        tab1D[0] = 6; // tab1D 6,8,7,5,9,4,2,8


        var sum1 = deuxPremiers.Sum(); // 6,5 => 11 => Sauvegarde la m�moire 
        var sum2 = tableau.Sum(); // 10 // Sauvegarde processeur

    }


    bool Filtre(int c)
    {
        return c < 7;
    }





}
