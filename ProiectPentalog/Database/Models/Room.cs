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

		[MaxLength(20)]
        public string Name { get; set; }
		[Range(0,60)]
		public int Capacity { get; set; }

        public virtual List<Reservation> Reservations { get; set; }

        public string Dotari { get; set; }

        //public int BrandId { get; set; }

        public byte[] BrandImage { get; set; }
    }
}