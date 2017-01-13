using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class Terrain
    {
        public int Id { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public string Titre { get; set; }
        public string Description { get; set; }
        public string PathImage { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
        
    }
}