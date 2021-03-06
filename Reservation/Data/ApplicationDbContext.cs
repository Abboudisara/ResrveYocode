using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reservation.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {

        }


        public DbSet<TypeReservation> Types { get; set; }
        public DbSet<Reserve> Reservations { get; set; }
        public DbSet<Reservation.Data.Reserve> reservation { get; set; }


    }
}
