using Liste;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MesClasses.Persistence
{
    // Contrat permettant d'identifier les méthodes nécessaires à la sauvegarde
    public interface IPersistenceListe<TId>
    {
        Task<TId> AddListeAsync(ListeCourses liste);
        Task RemoveListAsync(TId id);
        Task<TId> AddItemToList(TId idListe,Item item);
        Task<Item> UpdateItem(TId idListe, TId IdItem,Item item);

        Task<ListeCourses> GetListeCoursesAsync(TId id);
    }
}
