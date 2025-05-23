using Liste;
using MesClasses;
using System.Runtime.Serialization.DataContracts;

namespace MesTests;

[TestClass]
public class ReflexionTests
{
    [TestMethod]
    public void TestMethod1()
    {

        Object o = new Item("Pâtes",10M);
        // o = new ListeCourses();

        var typeDuContenuDeO = o.GetType();

        var props=typeDuContenuDeO.GetProperties();
        var fields = typeDuContenuDeO.GetFields(); // Les champs (public par défaut)
        fields = typeDuContenuDeO.GetFields(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        foreach ( var field in fields)
        {

        }

        var champsPrix = typeDuContenuDeO.GetField("_Prix", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        // Changement de valeur pour le champs _Prix sur l'objet o
        var valeurActuelleDu_Prix = (Decimal)champsPrix.GetValue(o);
        champsPrix.SetValue(o, valeurActuelleDu_Prix+1);

        // Refererence vers une méthode de o
        var methodeDeO = typeDuContenuDeO.GetMethod("Toto");
        // executer Toto(1,2)
        methodeDeO.Invoke(o,new object[] {1,2});
        
        var dataContractAttribute= typeDuContenuDeO.GetCustomAttributes(typeof(DataContract),false).FirstOrDefault();


        if (o is Item) { 
            var i=(Item)o;
        }


    }
}
