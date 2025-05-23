using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MesClasses.Persistence.BDD
{
    // Objet représentant le type de la fonction OnModelCreating
    public delegate void ModelBuilderDelegate(ModelBuilder modelBuilder);

    public class ListeDbContext : DbContext
    {
        private readonly ModelBuilderDelegate modelBuilderDelegate;

        // DbContextOptions
        // Provider utilisé
        // Chaine de connexion
        // Informations authentifications
        // Autres options

        public ListeDbContext(DbContextOptions options,
                ModelBuilderDelegate modelBuilderDelegate
            ) : base(options) 
        {
            this.modelBuilderDelegate = modelBuilderDelegate;
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Dans cette méthode, je peux préciser des choses dans la BDD
            // Fluent Api

            this.modelBuilderDelegate(modelBuilder);

        }
        public DbSet<ListeDAO>   Listes { get; set; }
        public DbSet<ItemDAO> Items { get; set; }
    }
}
