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

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int RoomId { get; set; }

        public String SelectedTime { get; set; }

        //public String SelectedTime { get; set; }
    }
}