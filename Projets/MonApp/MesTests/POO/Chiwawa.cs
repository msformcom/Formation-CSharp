using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MesTests.POO
{
    public class Chiwawa : Chien
    {
        public override string Cri()
        {
            return base.Cri().ToLower(); // Le cri du chiwawa est le cri du chein en minuscules
        }
    }
}
