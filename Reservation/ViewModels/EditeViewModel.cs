using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Reservation.ViewModels
{
    public class EditeViewModel
    {
        public EditeViewModel()
        {
            Users = new List<string>();
        }
        public String id { get; set; }

        [Required(ErrorMessage ="Role Name is Required")]
        public string RoleName { get; set; }
        public List<String> Users { get; set; }
    }
}
