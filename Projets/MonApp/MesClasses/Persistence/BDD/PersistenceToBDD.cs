using Liste;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
        public async Task<Guid> AddItemToList(Guid idListe, Item item)
        {
            // Transformation Item => ItemDAO
            // ItemDAO=> Item
            // je peux demander à l'injection de dépendance un IMapper
            // Avec config Label<=>Libelle
            // var dao=mapper.Map<ItemDAO>(item);
           
            var dao = new ItemDAO()
            {
                Description = item.Description,
                Label = item.Libelle,
                Price = item.Prix,
                Tickets = item.Points,
                RealisationDate = item.DateRealisation,
                FK_Liste = idListe

            };
            await context.AddAsync(dao);
            await context.SaveChangesAsync();
            return dao.Id;
        }

        public async Task<Guid> AddListeAsync(ListeCourses liste)
        {
            // var dao=mapper.Map<ListeDAO>(liste);
            var dao =new ListeDAO();
            dao.Name = liste.Nom;

            // Ajout du nouvel objet à la liste des enregistrement => Rien est envoyé à la BDD
            await context.Listes.AddAsync(dao);

            // var itemDaos=mapper.Map<IEnumerable<Item>>(liste.Items);
            var itemDaos = liste.Items.Select(i => new ItemDAO()
            {
                Description = i.Description,
                Label = i.Libelle,
                Price = i.Prix,
                Tickets = i.Points,
                RealisationDate = i.DateRealisation,
                FK_Liste=dao.Id
            });
            await context.Items.AddRangeAsync(itemDaos);

            // Autres changements dans Listes ou Items

            // Génère des instructions INSERT, DELETE, UPDATE en fonction des changements effectués
            // dans context.Listes
            await context.SaveChangesAsync();
            return dao.Id;

        }

        public Task<ListeCourses> GetListeCoursesAsync(Guid id)
        {
            var dao=context.Listes.Find(id)
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
