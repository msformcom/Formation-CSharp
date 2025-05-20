using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MesTests.POO
{
    public class Fourchette : IVendable, ILouable
    {
        public string Matiere { get; set; }
        public int NombreDeDents { get; set; }

        public Decimal PrixMensuel { get; set; }

        public Decimal Prix { get; set; }
        public String Reference { get; set; }
        public string Description { get; set; }
        public void Vendre()
        {

        }
    }
}
