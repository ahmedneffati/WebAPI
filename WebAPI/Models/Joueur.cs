using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class Joueur
    {
        [Key]
        public string Email { get; set; }

        public string MotDePass { get; set; }
        public string NomEtPrenom { get; set; }
        public string NumTel { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateDeNaiss { get; set; }
        public bool CompteActive { get; set; }
        public ICollection<Match> MatchOrganiser{ get; set; }
        public ICollection<MatchJoueur> MatchsParticiper { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
        

    }
}