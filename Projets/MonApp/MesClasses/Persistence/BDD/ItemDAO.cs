
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MesClasses.Persistence.BDD
{
    // Cette classe est en correspondance avec la table TBL_Items
    public class ItemDAO
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Label { get; set; }
        public Decimal? Price { get; set; }
        public int? Tickets { get; set; }
        public string Description { get; set; }
        public DateTime? RealisationDate { get; set; }

        public Guid FK_Liste { get; set; }

        // Propriété de navigation => va ajouter un IdListe dans TBL_Items
        public virtual ListeDAO Liste { get; set; }

    }
}
