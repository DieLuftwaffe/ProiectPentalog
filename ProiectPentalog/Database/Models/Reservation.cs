using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProiectPentalog.Database.Models
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [ForeignKey(nameof(Room))]
        public int RoomId { get; set; }
        
        public virtual Room Room { get; set; }
    }
}