using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class Horaire
    {
        public int Id { get; set; }
        public string Intervalle { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }
}