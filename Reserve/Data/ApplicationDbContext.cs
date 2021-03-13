using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reserve.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public virtual DbSet<Utitlisateur> user { get; set; }
        public DbSet<TypeReservation> Types { get; set; }
        public DbSet<reservation> Reservations { get; set; }
        public DbSet<Reserve.Data.reservation> reservation { get; set; }


    }
}
