// See https://aka.ms/new-console-template for more information
using System.Text;

Console.WriteLine("Hello, World!");


// Declaration de variable
int a;  // a vaut 0 => Default 0
a = 1;

int? b; // B vaut null et est nullable
b = null;

double d = a;

int e = 1;  // var => inférence de type => prend le type de la valeur de droite
int f = e / 2; // Attention calcul sur 2 int => int

var g = 1.0;
g = 1D;

var vDecimal = 1M; // Decimal => Exactitude
var vDouble = 1D; // Double =>Precision plus grande que float 
var vFloat = 1F; // Float => Precision basique


decimal dd = 0;
for (int i = 0; i < 100; i++)
{
    dd += 0.3M;
}

if (dd == 30)
{
    Console.WriteLine("Ligne 33");
}



string s = "Dominique";
// chaines non mutables
// en ram on va garder un certain temps toutes les valeurs intermédiaires de s
for (var i = 0; i < 1000; i++)
{
    s += "*";
}

var sb = new StringBuilder();
sb.Append("Dominique");
for (var i = 0; i < 1000; i++)
{
    // Le stringbuilder gère la mémoire correctement
    sb.Append("*");
}

s = sb.ToString();
string s2 = s; // s2 contient une copie de la valeur s


string S = "Dom";

string path = "c:\\nata";
path = @"c:\data";
path = "toto";



string nomPersonne = "Mauras";
string prenomPersonne = "Dom";


Personne p = new Personne(); // p est une référence vers un emplacement dans le tas

p.Nom = "Mauras";

var p2 = p;  // p2 contient la même adresse que p

p.Nom = "gates";

// p2.Nom <=> p.Nom

p = new Personne(); // Nouvek éléménet dans le tas et p change d'adresse , p2 conserver l'ancienne adresse




class Personne
{
    public string Nom;
    public string Prenom;
}





