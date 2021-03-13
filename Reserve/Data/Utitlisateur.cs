﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Reserve.Data
{
    public class Utitlisateur:IdentityUser
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string FullName { get; set; }
    }
}