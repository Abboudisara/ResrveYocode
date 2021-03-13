using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Reservation.Data
{
    public class TypeReservation
    {
        [Key]

        public int id_type { get; set; }
        [Required(ErrorMessage = "Entrer Nom de Reservation")]
        [Display(Name = "Reservation")]
        public string Nom { get; set; }
        [Required(ErrorMessage = "Entrer Nombre acces ")]
        [Display(Name = "Nombre")]
        public int NombreApp { get; set; }
    }
}

