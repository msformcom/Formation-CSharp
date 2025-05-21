using Microsoft.VisualStudio.TestTools.UnitTesting;
using MesClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Liste;

namespace MesClasses.Tests
{
    [TestClass()]
    public class ListeCoursesTests
    {
        [TestMethod()]
        public async Task ListeCoursesTest()
        {
            var liste = new ListeCourses();
            var pates = new Item("Pates fraiches au chcolat", 10);
            var riz = new Item("Riz au chcolat", 10);
            var chocolat = new Item("chcolat", 10);
            var gateaux = new Item("Gateaux au chcolat", 10);

            // Sans await L'exécution de cette méthode Async est instantanéé
            // Mais le thread qui exécute les instruction parrallelisée n'a pas forcément

            // Avec await => les instruction qui suivent sont exécutées lorsque la tache est terminée
            await liste.AddItemAsync(pates); // 1s
            await liste.AddItemAsync(riz); //1s
            // Arrivée ici au bout de  2s

            await Task.WhenAll(liste.AddItemAsync(chocolat), liste.AddItemAsync(gateaux));
            // Arrivée ici au bout 1s => si le nombre de coeurs est suffisant pour lancer les taches en parrallelisé




            // liste.AddItem()
            // Thread.Sleep(2000);
            // Ligne 23 => Instantané
            Assert.IsTrue(liste.Items.Count() == 4);
        }
    }
}