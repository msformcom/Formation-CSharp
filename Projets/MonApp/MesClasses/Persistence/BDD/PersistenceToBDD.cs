using Liste;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MesClasses.Persistence.BDD
{
    public class PersistenceToBDD : IPersistenceListe<Guid>
    {
        private readonly ListeDbContext context;

        public PersistenceToBDD(ListeDbContext context, ILogger<PersistenceToBDD> logger)
        {
            this.context = context;
        }
        public Task<Guid> AddItemToList(Guid idListe, Item item)
        {
            throw new NotImplementedException();
        }

        public async Task<Guid> AddListeAsync(ListeCourses liste)
        {
            var dao=new ListeDAO();
            dao.Name = liste.Nom;

            // Ajout du nouvel objet à la liste des enregistrement => Rien est envoyé à la BDD
            context.Listes.Add(dao);

            // Autres changements dans Listes ou Items

            // Génère des instructions INSERT, DELETE, UPDATE en fonction des changements effectués
            // dans context.Listes
            await context.SaveChangesAsync();
            return dao.Id;

        }

        public Task<ListeCourses> GetListeCoursesAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveListAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Item> UpdateItem(Guid idListe, Guid IdItem, Item item)
        {
            throw new NotImplementedException();
        }
    }
}
