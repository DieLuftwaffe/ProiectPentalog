using ProiectPentalog.Database.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProiectPentalog.ViewModel
{
    public class CreateReservationsVM
    {
        public int Id { get; set; }

        [MaxLength(20)]
        public string Name { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime ReservationDate { get; set; }

        public int RoomId { get; set; }

        public String StartHour { get; set; } //

        public String EndHour { get; set; } //
    }
}