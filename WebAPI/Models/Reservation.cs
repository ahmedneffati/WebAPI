using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        public Joueur Joueur { get; set; }
        [ForeignKey("Joueur")]
        public string EmailJoueur { get; set; }
        public Terrain Terrain { get; set; }
        [ForeignKey("Terrain")]
        public int IdTerrain { get; set; }
        public Horaire Horaire { get; set; }
        [ForeignKey("Horaire")]
        public int HoraireId { get; set; }
        public string EtatDeConfirmation { get; set; }
    }
}