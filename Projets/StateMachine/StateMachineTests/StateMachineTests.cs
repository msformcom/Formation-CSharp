using Microsoft.VisualStudio.TestTools.UnitTesting;
using StateMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachine.Tests
{
    [TestClass()]
    public class StateMachineTests
    {
        [TestMethod()]
        public async Task ConstructorTest()
        {
            // Arrange
            var etatsTab = new string[] { "Point mort", "Première", "Deuxième" };
            var etatsList = new List<string>(){ "Point mort", "Première", "Deuxième" };
            var etatsEntiers = new List<int>() { 1,7,8 };
            var boiteAVitesse = new StateMachine<string>(etatsTab);
            // Evènement EtatChanged
            // Déclenché quand : Quand Etat change
            // Qu'est-ce qui va être exécuté ? Fonction Gestionnaire évènement
            boiteAVitesse.EtatChanged += (o,e) =>
            {
                int a = 1;
                // o : Instance de StateMachine
                // e : EventArgs => Ancien Etat, Nouvel Etat
            };
            boiteAVitesse.EtatChanged += (o, e) =>
            {
                int a = 2;
                // o : Instance de StateMachine
                // e : EventArgs => Ancien Etat, Nouvel Etat
            };



            // Assert
            Assert.IsTrue(boiteAVitesse.Etat == "Point mort", "Etat initial incorrect");

            // Act
            var etatApresUp= await boiteAVitesse.UpAsync();
          
            // Assert
            Assert.IsTrue(boiteAVitesse.Etat == "Première", "Etat initial incorrect");


        }
    }
}