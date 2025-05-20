namespace Liste
{
    public class Item
    {
        // Constructeur => initialisation des valeurs de la classe
        // Permet d'imposer certaines valeur
        public Item(string libelle)
        {
            this.Libelle = libelle;
        }

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


        private bool _Fait;

        public bool Fait
        {
            get { 
                return _Fait; 
            }
            set { 
                if(!value && this.Fait == true)
                {
                    throw new InvalidOperationException("Impossible, car Item fait");
                }
                _Fait = value; 
            }
        }

    }
}


