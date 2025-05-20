using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MesTests.POO
{
    public  class Chien : IVendable
    {
        public string Nom { get; set; }

        public virtual string Cri()  // Virtual => Permet de changer la méthode dans les classes héritées
        {
            return "WOUAF";
        }
        public Decimal Prix { get; set; }
        public String Reference { get; set; }
        public string Description { get; set; }
        public void Vendre() { }
    }
}
