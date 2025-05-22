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
    public class PersistenceToDisk : IPersistenceListe<Guid>
    {
        private readonly ILogger<PersistenceToDisk> logger;
        private readonly PersistenceToDiskOptions options;


        // Dans cette classe, j'ai besoin de journaliser une erreur
        // Problème : Quel objet sert à journaliser dans mon appli => Aucune idée
        // Je vais demander l'objet qui sert à journaliser => faire appel à ID
        // Interface associée à la journalisation c'est ILogger
        // Dans le package Microsoft.Extensions.Logging.Abstractions


        public PersistenceToDisk(ILogger<PersistenceToDisk> logger, PersistenceToDiskOptions options)
        {
            this.logger = logger;
            this.options = options;
        }

        public Task<Guid> AddItemToList(Guid idListe, Item item)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> AddListeAsync(ListeCourses liste)
        {
            // Thread UI qui arrive ici
            logger.Log(LogLevel.Warning, "Ajout de liste");
            return Task.Run(() => {
                // thread secondaire qui exécute ce code => Le thread UI est libéré
                // Enregistre la liste sur le disque
                // Ou ? "c:\\data" => config
                // L'application qui va utiliser cette classe fournira une instance de classe
                // qui implement IConfiguration 
                var pathToDirectory = options.PathToLists;

                if (!Directory.Exists(pathToDirectory))
                {
                    Directory.CreateDirectory(pathToDirectory);
                }
                var id = Guid.NewGuid();


                // IDisposable 
                // Dispose() => méthode qui ferme toutes les resources utilisées par l'objet
                // using garantit que quel que soit la situation (Exception / return)
                // .Dispose sera exécuté à la sortie du bloc de code
                try
                {
                   // var s = string.Format("{1:dd/MM/yyyy}-{0}", 6, DateTime.Now); // "22/05/2025-6"
                    using (var s = File.Create(Path.Combine(pathToDirectory,
                    
                        string.Format(options.FileNamePattern, id) // "Liste-{0}.json" => "Liste-6717617-675675.json"

                        )))
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
                        return id;
                    }
                }
                catch (Exception ex)
                {
                    logger.Log(LogLevel.Error, "Sauvegarde de liste sur disque échouée");
                    var ex2= new Exception("Ecriture non possible",ex);
                    // ex2.InnerException contient ex => permet au dév qui gère ex2 de connaître l'exeption original
                    throw ex2;
                }




            });
 
        }

        public Task<ListeCourses> GetListeCoursesAsync(Guid id)
        {
            return Task.Run(() =>
            {
                var pathToDirectory =options.PathToLists;
                var fileName = Path.Combine(pathToDirectory,
                     string.Format(options.FileNamePattern, id));

                var file = new FileInfo(fileName);
                if (!file.Exists)
                {
                    throw new Exception("Enregistrement non trouvé");
                }
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(ListeCourses));
                using (var s = file.OpenRead())
                {
                    ListeCourses resultat = (ListeCourses)serializer.ReadObject(s);
                    if (resultat != null)
                    {
                        return resultat;
                    }
                    throw new Exception("Impossible de désérialiser les données");
                }
            });
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
