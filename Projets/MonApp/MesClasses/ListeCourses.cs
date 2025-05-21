using Liste;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MesClasses
{
    // public => Accessible partout
    // internal => Accessible dans le projet
    // sealed => Pas d'héritage possible
    // partial => Définie dans plusieurs fichier
    // 
    public partial class ListeCourses
    {
        public ListeCourses()
        {
            this.ListeItems = new(); // new List<Item>
        }
        private List<Item> ListeItems ;

        // Version visible de la liste => non modifiable
        public IEnumerable<Item> Items
        {
            get
            {
                return ListeItems.Where(c=>!c.Secret).OrderBy(c=>c.DateRealisation);
            }
        }

        // Asynchronisme => il y a une possibilit pour que l'opération dure un ecrtain temps
        public Task AddItemAsync(Item item)
        {
            int a = 0;
            // Parralélisation : Créer une tache en définissant la fonction à éxécuter en parrallelle
            Task T = new Task(() => { 
                // Ce code sera exécuté par un thread séparé
                // Pour la démo, semblant de temps passé
                Thread.Sleep(3000);
                this.ListeItems.Add(item);                
            
            });

            T.Start();
          
            // Pour gérer l'asynchronisme (fin de l'opération)
            // du coté du code appelant je renvois un objet Task
            // Task => Classe qui représente une opération asynchrone
            return T;

       

        

        }

    }
}
