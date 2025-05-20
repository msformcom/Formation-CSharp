
namespace AutresTests
{
    namespace Partie1
    {
        public sealed class Test1
        {
        }
    }

}
namespace MesTests
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod] // Annotation => En C# Attribut => Information associée à ce qui est en dessous
        public void TestExecutionFonction()
        {
            var t = new AutresTests.Partie1.Test1();
            int a = 1;
            // F10 : Passe à la ligne suivant
            // F11 : Entgre dans une fonction au lieu de l'éxécuter sans entrer
            // Shift + F11 : Sort de la fonction et retourne au code
            // Utilisation d'une fonction avec paramètres facultatifs
            int b = Addition(1, 2,3);
            b=Addition(1,2,5,7);
            b = Addition(1); // C => valeur par défaut dans la fonction

            Personne p = new Personne(); // p => 1234
            p.Nom = "Domi";
            ChangeNom(p);
            // p.Nom   = Domi*
        }

        // Les paramètre facultatifs arrivent après les non facultatifs
        /// <summary>
        /// Additionne les nombres
        /// </summary>
        /// <param name="a">Premier nombre</param>
        /// <param name="b">Nombre 2</param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <returns>La somme des nombres</returns>
        public int Addition(int a, int b = 1, int c = 2, int d = 3)
        {
            return a + b + c + d;
        }


        public double Addition(int a, double b)
        {
            return a + b ;
        }


        public void ChangeNom(Personne personne)
        {
            //  personne => 1234 Une copie de la référence de la personne
            personne = new Personne(); // 5678
            personne.Nom += "*";
        }

        public class Personne
        {
            public string Nom;
        }
    }



}
