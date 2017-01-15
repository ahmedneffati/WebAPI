using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class Admin
    {
        [Key]
        public string Email { get; set; }

        public string MotDePass { get; set; }
    }
}