using Liste;
using Microsoft.EntityFrameworkCore;
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
            // First => erreur si pas d'élément trouvé
            // Find => renvoit null si pas d'élément trouvé

            //  dao.Items => null car non chargement  par défaut des propriétés de navigation
            // SELECT TOP 1 * FROM TBL_Listes WHERE Id=89798
            // Si je veux charger les items je dois le spécifier dans mon code
            var dao = context.Listes.Find(id);

            // dao.items => Rempli avec les items => EagerLoading => Propriété navigation chargée en avance
            // SELECT TOP 1 * FROM TBL_Listes
            // INNER JOIN TBL_Items ON ...
            // WHERE Id=89798
            var daoAvecItems = context.Listes.Include(c => c.Items).First(c => c.Id==id);

            // Juste les données de l'item 
            ListeDAO? daoSansItems = context.Listes.Find(id);
            
            // daoSansItems.Items => null
            if (daoSansItems != null)
            {
                // Je vais charger la liste de l'item daoSansItems => ExplicitLoading
                context.Entry(daoSansItems!).Collection(c => c.Items).Load();
                //daoAvecItems.Items => Rempli
            }

            // LasyLoading implicit => 
            // Package Proxies => Ajoute la capacité de charger automatiquement les proproétés de nav
      //      daoSansItems = context.Listes.Find(id);
            // Les items ne sont pas chargés
          //  daoAvecItems.Items.ToArray(); // => Va envoyer la requete SELECT pour obtenir les Items
            // 






            var req2 = context.Items.Include(c=>c.Liste).First(c => c.Id == id);
            // req2.Liste => ListeDAO
            // req2.Liste.Items => null


            var req3 = context.Items
                    .Include(c => c.Liste)
                    .ThenInclude(c=>c.Items).First(c => c.Id == id);
            // req2.Liste => ListeDAO
            // req2.Liste.Items => ItemDAOs


            //var dao2 = context.Listes.First(c => c.Id == id);
            if (dao == null)
            {
                throw new Exception("Id non existatnt");
            }

            var poco = new ListeCourses();
            
            poco.Nom = dao.Name;

            // Obtenir la référence vers la List<Item> privée de poco dans le champs ListeItems
            var typeListeCourses = poco.GetType();
            typeListeCourses = typeof(ListeCourses);
            typeListeCourses = Type.GetType("MesClasses.ListeCourses");
            var champs=typeListeCourses.GetField("ListeItems", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var listeItemsDePoco = (List<Item>)champs.GetValue(poco);
            //listeItemsDePoco.AddRange(dao.Items.Select(c=>new Item(c.Label,c.Price)
            //{

            //}))

            // poco.Items.Add => IEnumerable
            // poco.ListeItems.Add => Impossible car privé
            // Avec Reflection => Je vais accéder à un champs privé
            return Task.FromResult(poco);

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
