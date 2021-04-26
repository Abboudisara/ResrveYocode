using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Reservation.Models
{
    public class ReservationModel
    {
        [Key]
        public int id_reservation { get; set; }

        [Required(ErrorMessage = "Entrer Date")]
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public string date { get; set; }
        [Required(ErrorMessage = "Svp choisir un Type Reservation ")]
        public int typeReservationid_type { get; set; }

        [ForeignKey(nameof(typeReservationid_type))]
        public TypeReservationModel typeReservation { get; set; }
        public string User_id { get; set; }

        [ForeignKey("User_id")]
        public ApplicationUser utitlisateur { get; set; }

        public String Status { get; set; }
    }
}
