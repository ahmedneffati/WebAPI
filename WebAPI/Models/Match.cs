using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class Match
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int NbDeJoueur { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public Joueur Organisateur { get; set; }
        [ForeignKey("Organisateur")]
        public string OrganisateurEmail { get; set; }
        public ICollection<MatchJoueur> MatchJoueur { get; set; }


    }
}