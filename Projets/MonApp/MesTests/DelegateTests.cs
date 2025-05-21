namespace MesTests;

[TestClass]
public class DelegateTests
{
    [TestMethod]
    public void DelegateTest()
    {
        Action<string, int> g = (a, b) =>
        {
            Console.WriteLine(a);
            Console.WriteLine(b);
        };

        // TransformeEntierEnChaine est un nom donné à Func<int,string>
        TransformeEntierEnChaine tec = (e) => e.ToString();
        

        Func<int, int, double> f = Addition;
        var r=f(1, 2);

        f = Soustraction;
        r = f(1, 2);

        f = (a, b) => a * b;
        f = (a, b) =>
        {
            if (b == 0) return 0;
            return a / b;
        };

        f(1, 2);
    }

    double Addition(int a,int b)
    {
        return a + b;
    }
    double Soustraction(int a, int b)
    {
        return a - b;
    }

     // Definition d'un délégate 
     // Type de fonction
     public delegate string TransformeEntierEnChaine(int a);

    [TestMethod]
    public void MonWhereTest()
    {
        var entiers = Enumerable.Range(0,int.MaxValue); // IEnumerable = generateur
        var petitsEntiers =MonWhere(entiers,c=>{
            return c < 5;
        });



        var lesPremiers = petitsEntiers.Take(4);


        // Iterator => ce code demande les éléments un par un
        foreach (var e in lesPremiers)
        {
            int a = e;
        }

    }


    // generator=> fonction qui renvoit des élément sur demande (un par un)
    IEnumerable<int> MonWhere(IEnumerable<int> ensemble, Func<int, bool> filtre)
    {
     
        foreach (int i in ensemble)
        {
            if (filtre(i))
            {
                yield return i;
            }
        }
    }

    [TestMethod]
    public void FiboTest()
    {
        foreach(var e in Fibo().Take(10))
        {
            int a = e;
        }
    }

    // Suite de fibo => 1,1,2,3,5,8,13,
    public IEnumerable<int> Fibo()
    {
        int a = 0;
        int b = 1;
        while (true)
        {
            yield return b;
            var c = a;
            a = b;
            b = b + c;
        }

    }



    //IEnumerable<int>  MonWhere(IEnumerable<int> ensemble, Func<int,bool> filtre)
    //{
    //    List<int> resultat=new List<int>();
    //    foreach (int i in ensemble)
    //    {
    //        if (filtre(i))
    //        {
    //            resultat.Add(i);
    //        }
    //    }
    //    return resultat;
    //}


}
