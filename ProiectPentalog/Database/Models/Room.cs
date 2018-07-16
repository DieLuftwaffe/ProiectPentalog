using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProiectPentalog.Database.Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int Capacity { get; set; }

        public virtual List<Reservation> Reservations { get; set; }
    }
}