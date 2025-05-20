
using Liste;


namespace MesTests
{
    [TestClass]
    public class ItemTests
    {
        [TestMethod]
        public void FaitTest()
        {
            // En cas d'Exception dans un test => Fail

            // Arrange 
            var item = new Item("Pates",0);

            // Assert1 : Le fait est à faux
            Assert.IsFalse(item.Fait,$"Valeur par défaut d'un item incorrecte pour {nameof(item.Fait)}" );

            // Act1 : Mettre à vrai
            item.DateRealisation = DateTime.Now;

            // Asert 2: Fait est vrai
            Assert.IsTrue(item.Fait,$"On ne peut pas mettre {nameof(item.Fait)} à vrai");


            Exception mettreAFauxException= null;

            // Act2 : Remettre à faux
            try
            {
                item.DateRealisation = null;
            }
            catch (Exception ex)
            {

                mettreAFauxException = ex;
            }

            // Assert 3: Exception 
            Assert.IsNotNull(mettreAFauxException, $"On peut remettre {nameof(item.Fait)} à faux");
          
        }





        [DataTestMethod]
        [DataRow("Pates", true)]
        [DataRow(null, false)]

#if DEBUG
    // Pour le déboggage, on veut que les exécutions de la méthode soient paralelisés
   [DoNotParallelize]
#else

#endif

        //  [DataRow("", false)]
        //   [DataRow("  ", false)] // La méthode de test sera exécutée 3 fois
        public void ConstructionTest(string libelle, bool ok)
        {
            // Item sans libelle => Non
            // Pour utiliser une classe d'un autre projet :
            // 1) Référencer l'assembly (dll ou exe) ou le projet
            // 2) Préciser le namespace ( using Liste ou Miste.item)

            // Arrange => Préparation des conditions du test
            Exception videException=null;
            Item item = null;

            // Act => Agit
            try
            {
                // Cette méthode doit générer une Exception (Erreur) si la valeur n'est pas acceptable
                item = new Item("libelle",0M);
                item.Libelle = libelle;

             }
            catch (Exception ex)
            {
                videException = ex;

            }
            // Assert => Vérifier
            if (ok)
            {
                Assert.IsNotNull(item);
                Assert.IsNotNull(item.Libelle, "Le libellé ne peut être null");
                Assert.AreEqual(item.Libelle, libelle, "Le paramètre libelle bn'est pas pris en compte");
            }
            else
            {
                
                Assert.IsNotNull(videException,"Pas d'erreur sur une valeur non acceptable "+(libelle ==null ? "null":libelle.ToString()) );
            }
        }
    }
}
