using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MesTests.POO
{
    public interface IVendable
    {
        Decimal Prix{ get; set; }
        String Reference { get; set; }
        string Description { get; set; }

        void Vendre();
    }
}
