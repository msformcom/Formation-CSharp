using Liste;
using MesClasses.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MesClasses
{
    // public => Accessible partout
    // internal => Accessible dans le projet
    // sealed => Pas d'héritage possible
    // partial => Définie dans plusieurs fichier
    // 
    [DataContract]
    public partial class ListeCourses
    {
        // Quand on crée une liste, on peut fournir l'objet servant à sauvegarder
        public ListeCourses()
        {
            this.ListeItems = new(); // new List<Item>
        }

        #region Propriété Nom

        [DataMember(Name ="Name")]
        private string _Nom;

        public string Nom
        {
            get
            {
                return _Nom;
            }
            set
            {
                // TODO : Contoller value
                _Nom = value;
            }
        }
        #endregion


        [DataMember(Name ="Items")]
        private List<Item> ListeItems ;

        // Version visible de la liste => non modifiable
        public IEnumerable<Item> Items
        {
            get
            {
                return ListeItems.Where(c=>!c.Secret).OrderBy(c=>c.DateRealisation);
            }
        }

        // Ajoouter la déclation d' event ItemAdded
        // signature du gestionnaire void (object,ItemAddedEventArgs)
        public event EventHandler<ItemAddedEventArgs> ItemAdded;

        // Fonction qui déclenche l'évènement
        // protected => Accessible uniquement par les classes qui héritent
        // virtual => Peut être réécrite par les classes qui héritent
        protected virtual void OnItemAdded(Item addedItem)
        {
            if (ItemAdded != null)
            {
                ItemAdded(this, new ItemAddedEventArgs() { AddedItem= addedItem });
            }
        }


        // Le déclencher au bon moment (ligne 46)
        // Dans le test associer une fonction (gestionnaire) à l'évènement


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
                OnItemAdded(item);
            });

            T.Start();
          
            // Pour gérer l'asynchronisme (fin de l'opération)
            // du coté du code appelant je renvois un objet Task
            // Task => Classe qui représente une opération asynchrone
            return T;

       

        

        }

        // Cette classe est destinée à être utilisée avec l'evènement ItemAdded
        // Pour faire passer les informations de l'évènement
        public class ItemAddedEventArgs : EventArgs
        {
            public Item AddedItem { get; set; }
        }

    }
}
