using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachine
{
    public class BoiteAVitesse :StateMachine<string>
    {
        public BoiteAVitesse(): base(new string[] {"Point mort","Premiere","Deuxieme"})
        {
            
        }
    }
}
