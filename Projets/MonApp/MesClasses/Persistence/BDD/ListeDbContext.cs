using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MesClasses.Persistence.BDD
{
    public class ListeDbContext : DbContext
    {
        // DbContextOptions
        // Provider utilisé
        // Chaine de connexion
        // Informations authentifications
        // Autres options

        public ListeDbContext(DbContextOptions options) : base(options) 
        {
            
        }
        public DbSet<ListeDAO>   Listes { get; set; }
        public DbSet<ItemDAO> Items { get; set; }
    }
}
