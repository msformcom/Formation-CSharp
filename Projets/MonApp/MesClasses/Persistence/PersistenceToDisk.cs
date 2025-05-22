using Liste;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace MesClasses.Persistence
{
    internal class PersistenceToDisk<T> : IPersistenceListe<Guid>
    {
        private readonly ILogger logger;
        private readonly IConfiguration config;

        // Dans cette classe, j'ai besoin de journaliser une erreur
        // Problème : Quel objet sert à journaliser dans mon appli => Aucune idée
        // Je vais demander l'objet qui sert à journaliser => faire appel à ID
        // Interface associée à la journalisation c'est ILogger
        // Dans le package Microsoft.Extensions.Logging.Abstractions
        public PersistenceToDisk(ILogger logger, IConfiguration config)
        {
            this.logger = logger;
            this.config = config;
        }

        public Task<Guid> AddItemToList(Guid idListe, Item item)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> AddListeAsync(ListeCourses liste)
        {
            // Enregistre la liste sur le disque
            // Ou ? "c:\\data" => config
            // L'application qui va utiliser cette classe fournira une instance de classe
            // qui implement IConfiguration 
            var path =config.GetSection("PathForLists").Value;

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var id=Guid.NewGuid();


            // IDisposable 
            // Dispose() => méthode qui ferme toutes les resources utilisées par l'objet
            // using garantit que quel que soit la situation (Exception / return)
            // .Dispose sera exécuté à la sortie du bloc de code
            try
            {
                using (var s = File.Create(Path.Combine(path, id.ToString() + ".json")))
                {
                    // Creation du fichier et ouverture du fichier

                    // s permet d'ecrire des bytes dans le fichier
                    //var sw = new StreamWriter(s, Encoding.UTF8);
                    // sw.WriteLine("Toto");

                    // Il me faut un serialiser Liste => json
                    //  DataContractJsonSerializer => josn
                    // DataContractSerializer => xml
                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(ListeCourses));
                    // Serialiser la liste en json et l'envoyer dans le stream
                    serializer.WriteObject(s, liste);

                }
            }
            catch (Exception)
            {
                logger.Log(LogLevel.Error, "Sauvegarde de liste sur disque échouée");
                throw new Exception("Ecriture non possible");
            }







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
