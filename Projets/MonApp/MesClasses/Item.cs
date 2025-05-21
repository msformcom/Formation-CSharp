using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Liste
{
    public class Item
    {
        // Constructeur => initialisation des valeurs de la classe
        // Permet d'imposer certaines valeur
        public Item(string libelle, decimal prix) : this(libelle, prix, null)
        {
            // Continuer la construction si nécessaire
        }

        // ce constructeur utilise un autre constructeur avec des valeurs spécifiques
        public Item(string libelle, int points) : this(libelle,null, points) 
        {

        }

        private Item(string libelle, decimal? prix, int? points){
            this.Libelle = libelle;
            this.Prix = prix;
            this.Points = points;
        }

        public bool Secret { get; set; } = false;




        #region Propriété Description


        private string? _Description;

        public string? Description
        {
            get
            {
                return _Description;
            }
            set
            {
                // TODO : Contoller value
                _Description = value;
            }
        }
        #endregion


        #region Propriété Prix


        private Decimal? _Prix=null;

        public Decimal? Prix
        {
            get
            {
                return _Prix;
            }
            set
            {
                // TODO : Contoller value
                if (this.Points != null)
                {
                    throw new InvalidOperationException();
                }
                _Prix = value;
            }
        }
        #endregion

        #region Propriété Points


        private int? _Points=null;

        public int? Points
        {
            get
            {
                return _Points;
            }
            set
            {
                // TODO : Contoller value
                if(value != null)
                {
                    this._Prix = null;
                }
                _Points = value;
            }
        }
        #endregion



        #region Propriété Libelle

        // Champs => stocker l'information
        private string _Libelle; // Pas de valeur par défaut

        // Propriété
        // item.Libelle="Toto" => execution du set avec value = "Toto"
        // var l=item.Libelle => execution du get
        public string Libelle
        {
            get
            {
                return _Libelle;
            }
            set
            {
                if(string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Le libellé est vide");
                }
                this._Libelle = value;
            }
        }
        #endregion

        #region Propriété Fait

        // Fait est une valeur calculée
        // pas de setter => lecture seule
        public bool Fait
        {
            get
            {
                return this._DateRealisation != null;
            }
        }
        #endregion

        #region Propriété DateRealisation

        private DateTime? _DateRealisation=null;
        // Raccourci pour créer une propriété sans contrôle avec champs masqué
        [DisplayName("Date de réalisation")] // Avec MVC l'affichage de cette info => label avec Date de réalisation
        [Column("RealisationDate")] // Avec EntityFramework => Utiliser RealisationDate comme nom de colonne
        // public DateTime DateRealisation { get; set; } // Propriété raccourcie

        public DateTime? DateRealisation
        {
            get { return _DateRealisation; }
            set {
                
                if(value==null && this._DateRealisation != null)
                {
                    throw new InvalidOperationException();
                }
                _DateRealisation = value; }
        }

        #endregion

        



    }
}


