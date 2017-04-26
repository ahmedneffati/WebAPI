using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class Proprietaire
    {
        [Key]
        public string Email { get; set; }
        public string MotDePass { get; set; }
        public string NomEtPrenom { get; set; }
        public string NumTel { get; set; }
        public bool CompteActive { get; set; }
        public ICollection<Terrain> TerrainS { get; set; }
    }
}