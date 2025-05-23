using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MesClasses.Persistence.BDD
{
    // Cette classe est en correspondance avec la table TBL_Listes
    public class ListeDAO
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }

        public DateTime DateCreation { get; set; } = DateTime.Now;

        // Propriété de navigation
        // 1) Permet de créer une relation TBL_Listes et DBL_Items
        // 2) On va trouver dans le HashSet la liste des Items d'une instance de listeDaAO

        // virtual pour permettre la réécriture des accesseurs
        // sera utile pour les EntityProxies
        public virtual HashSet<ItemDAO> Items { get; set; }

    }
}
