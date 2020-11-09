using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlogCore.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Required(ErrorMessage ="Nombre Obligatorio")]
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        [Required(ErrorMessage = "Ciudad Obligatorio")]
        public string Ciudad { get; set; }
        [Required(ErrorMessage = "Pais Obligatorio")]
        public string Pais { get; set; }

    }
}
