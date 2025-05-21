namespace StateMachine
{
    // L'état géré par cette classe n'est pas défini de manière définitive => Généricité
    public class StateMachine<TEtat>
    {
        public StateMachine(IEnumerable<TEtat> etats)
        {
            this.etats = etats.ToArray();
        }
        private TEtat[] etats =null;
        // Etat initial de la machine
        private int indexEtat=0;
        private bool enCoursDeChangement = false;

        // Declaration de l'évènement
        // On peut associer des fonctions à cet évènement
        // instance.EtatChanged+=(o,e)=>{ code à éxécuter}
        public event EventHandler EtatChanged;

        public TEtat Etat
        {
            get
            {
                return etats[indexEtat];
            }
        }


        public Task<TEtat> UpAsync()  // TEtat Up()    
        {
            if (enCoursDeChangement) {
                throw new InvalidOperationException("Changement d'état en cours");
            }
            if (this.indexEtat >= this.etats.Length - 1)
            {
                throw new InvalidOperationException("On est à l'état final");
            }
            this.enCoursDeChangement= true;

            return Task.Run( async () => {
                // await File.AppendText("c:\\data\state","kljhk")
                // Thread.Sleep(1000); // Operation type appel api lancée
                await Task.Delay(1000);
                indexEtat++;
                // Si il y a une fonction associée à cet évènement
                if (this.EtatChanged != null)
                {
                    // Déclenchement de l'évènement avec les informations associée
                    this.EtatChanged(this, EventArgs.Empty);
                }
                this.enCoursDeChangement = false;
                return this.Etat;
            });
        }
    }
}
