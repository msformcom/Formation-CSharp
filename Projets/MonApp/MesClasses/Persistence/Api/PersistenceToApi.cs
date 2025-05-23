using Liste;
using MesClasses.Persistence.BDD;
using MonApi.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MesClasses.Persistence.Api
{

    public class PersistenceToApi : IPersistenceListe<Guid>
    {
        private readonly HttpClient httpClient;

        public PersistenceToApi(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public Task<Guid> AddItemToList(Guid idListe, Item item)
        {
            throw new NotImplementedException();
        }

        public async Task<Guid> AddListeAsync(ListeCourses liste)
        {
            ListeDTO dto = new ListeDTO();
            dto.Libelle = liste.Nom;
            dto.Items = liste.Items.Select(c => new ItemDTO() {  Description = c.Description, Libelle = c.Libelle, DateRealisation = c.DateRealisation, Points = c.Points, Prix = c.Prix });
            // Lancer la requète http => response
            var requete = await httpClient.PostAsJsonAsync<ListeDTO>("/api/Listes", dto);
            // Attendre la désérialisation de Json
            var id = await requete.Content.ReadFromJsonAsync<Guid>();
            return id;

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
