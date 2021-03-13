using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Reservation.Data
{
    public class Utilisateur : IdentityUser
    {
        
            [Required]
            public string Name { get; set; }
            [Required]
            public string FullName { get; set; }

            public string Conter { get; set; }
        
    }
}

