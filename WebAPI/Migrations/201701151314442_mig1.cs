namespace WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        Email = c.String(nullable: false, maxLength: 128),
                        MotDePass = c.String(),
                    })
                .PrimaryKey(t => t.Email);
            
            CreateTable(
                "dbo.Horaires",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Intervalle = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        EmailJoueur = c.String(maxLength: 128),
                        IdTerrain = c.Int(nullable: false),
                        HoraireId = c.Int(nullable: false),
                        EtatDeConfirmation = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Horaires", t => t.HoraireId, cascadeDelete: true)
                .ForeignKey("dbo.Joueurs", t => t.EmailJoueur)
                .ForeignKey("dbo.Terrains", t => t.IdTerrain, cascadeDelete: true)
                .Index(t => t.EmailJoueur)
                .Index(t => t.IdTerrain)
                .Index(t => t.HoraireId);
            
            CreateTable(
                "dbo.Joueurs",
                c => new
                    {
                        Email = c.String(nullable: false, maxLength: 128),
                        MotDePass = c.String(),
                        NomEtPrenom = c.String(),
                        NumTel = c.String(),
                        DateDeNaiss = c.DateTime(nullable: true),
                    })
                .PrimaryKey(t => t.Email);
            
            CreateTable(
                "dbo.Matches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Longitude = c.Single(nullable: false),
                        Latitude = c.Single(nullable: false),
                        OrganisateurEmail = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Joueurs", t => t.OrganisateurEmail)
                .Index(t => t.OrganisateurEmail);
            
            CreateTable(
                "dbo.MatchJoueurs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        JoueurEmail = c.String(maxLength: 128),
                        MatchId = c.Int(nullable: false),
                        EtatDeConfirmation = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Joueurs", t => t.JoueurEmail)
                .ForeignKey("dbo.Matches", t => t.MatchId, cascadeDelete: true)
                .Index(t => t.JoueurEmail)
                .Index(t => t.MatchId);
            
            CreateTable(
                "dbo.Terrains",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Longitude = c.Single(nullable: false),
                        Latitude = c.Single(nullable: false),
                        Nom = c.String(),
                        Description = c.String(),
                        PathImage = c.String(),
                        EmailProp = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Proprietaires", t => t.EmailProp)
                .Index(t => t.EmailProp);
            
            CreateTable(
                "dbo.Proprietaires",
                c => new
                    {
                        Email = c.String(nullable: false, maxLength: 128),
                        MotDePass = c.String(),
                        NomEtPrenom = c.String(),
                        NumTel = c.String(),
                    })
                .PrimaryKey(t => t.Email);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservations", "IdTerrain", "dbo.Terrains");
            DropForeignKey("dbo.Terrains", "EmailProp", "dbo.Proprietaires");
            DropForeignKey("dbo.Reservations", "EmailJoueur", "dbo.Joueurs");
            DropForeignKey("dbo.Matches", "OrganisateurEmail", "dbo.Joueurs");
            DropForeignKey("dbo.MatchJoueurs", "MatchId", "dbo.Matches");
            DropForeignKey("dbo.MatchJoueurs", "JoueurEmail", "dbo.Joueurs");
            DropForeignKey("dbo.Reservations", "HoraireId", "dbo.Horaires");
            DropIndex("dbo.Terrains", new[] { "EmailProp" });
            DropIndex("dbo.MatchJoueurs", new[] { "MatchId" });
            DropIndex("dbo.MatchJoueurs", new[] { "JoueurEmail" });
            DropIndex("dbo.Matches", new[] { "OrganisateurEmail" });
            DropIndex("dbo.Reservations", new[] { "HoraireId" });
            DropIndex("dbo.Reservations", new[] { "IdTerrain" });
            DropIndex("dbo.Reservations", new[] { "EmailJoueur" });
            DropTable("dbo.Proprietaires");
            DropTable("dbo.Terrains");
            DropTable("dbo.MatchJoueurs");
            DropTable("dbo.Matches");
            DropTable("dbo.Joueurs");
            DropTable("dbo.Reservations");
            DropTable("dbo.Horaires");
            DropTable("dbo.Admins");
        }
    }
}
