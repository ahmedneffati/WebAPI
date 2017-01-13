using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class WebAPIContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public WebAPIContext() : base("name=BaseTerrainnet")
        {
        }

    
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Horaire> Horaires { get; set; }
        public DbSet<Proprietaire> Proprietaires { get; set; }
        public DbSet<Match> Matchs { get; set; }
        public DbSet<Joueur> Joueurs { get; set; }
        public DbSet<Terrain> Terrains { get; set; }
        
        public DbSet<MatchJoueur> MatchJoueurs { get; set; }

        public System.Data.Entity.DbSet<WebAPI.Models.Admin> Admins { get; set; }
    }
}
