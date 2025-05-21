using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MesTests.POO
{
    // POCO
    public class Personne
    {
        public string Nom { get; set; }
        public string  Prenom { get; set; }
    }

    // DAO => Classe générique => un ou plusieurs types sont variables
    public class PersonneDAO<T> : Personne
    {
        public T Id { get; set; }
    }
}
