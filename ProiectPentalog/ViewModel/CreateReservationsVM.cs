﻿using ProiectPentalog.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProiectPentalog.ViewModel
{
    public class CreateReservationsVM : Reservation
    {
        public String SelectedTime { get; set; }
    }
}