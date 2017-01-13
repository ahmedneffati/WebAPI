using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class MatchJoueur
    {
        public int Id { get; set; }
        //[Key, Column(Order = 0)]
        [ForeignKey("Joueur")]
        public string JoueurEmail { get; set; }
       // [Key, Column(Order = 1)]//il fait partire de clé primaire 
        [ForeignKey("Match")]
        public int MatchId { get; set; }
        public string EtatDeConfirmation { get; set; }
        public Match Match { get; set; }
        public Joueur Joueur { get; set; }
    }
}