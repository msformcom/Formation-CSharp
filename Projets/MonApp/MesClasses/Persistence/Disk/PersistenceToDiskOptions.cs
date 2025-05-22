using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MesClasses.Persistence
{
    public class PersistenceToDiskOptions
    {
        public string PathToLists { get; set; }
        public string FileNamePattern { get; set; } // "Liste-{0}.json"
    }
}
