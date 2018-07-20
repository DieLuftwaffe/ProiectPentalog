using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProiectPentalog.Database;
using ProiectPentalog.Database.Models;
using System.Text;
using ProiectPentalog.ViewModel;

namespace ProiectPentalog.Controllers
{
    public class ReservationsController : Controller
    {
        private RoomsDbContext db = new RoomsDbContext();

        // GET: Reservations
        public ActionResult Index()
        {
            var reservations = db.Reservations.Include(r => r.Room);
            return View(reservations.ToList());
        }

        // GET: Reservations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        private void formatDateString ()
        {
            //String currentTime = System.DateTime.Now.ToString("yyyy MM dd h:mm:ss tt");
            String currentHour = DateTime.Now.Hour.ToString();
            String currentMinut = DateTime.Now.Minute.ToString();
            String currentMeridian = System.DateTime.Now.ToString("tt");

            if (currentMeridian.Equals("PM"))
            {
                int hour = Int32.Parse(currentHour);
                hour += 12;
                currentHour = hour.ToString();
            }

            if (currentMeridian.Equals("AM"))
            {
                currentHour = "0" + currentHour;
            }
        }

        private int getCurrentHour()
        {
            String currentHour = System.DateTime.Now.Hour.ToString();
            return Int32.Parse(currentHour);
        }

        private int getHour(String dateTime)
        {
            return Int32.Parse(dateTime.Substring(0, 2));
        }

        private int getMinute(String dateTime)
        {
            return Int32.Parse(dateTime.Substring(3, 2));
        }

        private int getCurrentMinute()
        {
            String currentMinute = System.DateTime.Now.Minute.ToString();
            return Int32.Parse(currentMinute);
        }

        private List<String> getListOfHours()
        {
            List<String> listOfHours = new List<string>();

            StringBuilder builder = new StringBuilder();

            for (int index_hour = 0; index_hour < 24; index_hour++) //TO DO HERE
            {
                if (index_hour < 10)
                {
                    builder.Append("0");
                    builder.Append(index_hour);
                    builder.Append(":00");
                    listOfHours.Add(builder.ToString());
                    builder.Clear();

                    builder.Append("0");
                    builder.Append(index_hour);
                    builder.Append(":30");
                    listOfHours.Add(builder.ToString());
                    builder.Clear();
                }
                else
                {
                    builder.Append(index_hour);
                    builder.Append(":00");
                    listOfHours.Add(builder.ToString());
                    builder.Clear();

                    builder.Append(index_hour);
                    builder.Append(":30");
                    listOfHours.Add(builder.ToString());
                    builder.Clear();
                }
            }

            return listOfHours;
        }

        // GET: Reservations/Create
        public ActionResult Create()
        {
            ViewBag.RoomId = new SelectList(db.Rooms, "Id", "Name");

            StringBuilder builder = new StringBuilder();

            //List<string> listOfHours = new List<string>(getListOfHours()); // initializare lista bruta
            List<string> listOfStartHour = new List<string>();
            List<string> listOfEndHour = new List<string>();

            int actualHourVal = getCurrentHour(); // 
            int actualMinuteVal = getCurrentMinute(); //

            for (int index_hour = actualHourVal; index_hour < 24; index_hour++) //TO DO HERE
            {
                if(index_hour == actualHourVal && actualMinuteVal < 30)
                {
                    if (index_hour < 10)
                    {
                        builder.Append("0");
                        builder.Append(index_hour);
                        builder.Append(":30");
                        listOfStartHour.Add(builder.ToString());
                        builder.Clear();
                    }
                    else
                    {
                        builder.Append(index_hour);
                        builder.Append(":30");
                        listOfStartHour.Add(builder.ToString());
                        builder.Clear();
                    }
                }
               
                if(index_hour == actualHourVal && actualMinuteVal >= 30)
                {
                    // To Do Here
                }
                
                if(index_hour != actualHourVal)
                {
                    if(index_hour < 10)
                    {
                        builder.Append("0");
                        builder.Append(index_hour);
                        builder.Append(":00");
                        listOfStartHour.Add(builder.ToString());
                        builder.Clear();

                        builder.Append("0");
                        builder.Append(index_hour);
                        builder.Append(":30");
                        listOfStartHour.Add(builder.ToString());
                        builder.Clear();
                    }
                    else
                    {
                        builder.Append(index_hour);
                        builder.Append(":00");
                        listOfStartHour.Add(builder.ToString());
                        builder.Clear();

                        builder.Append(index_hour);
                        builder.Append(":30");
                        listOfStartHour.Add(builder.ToString());
                        builder.Clear();
                    }
                }
            }

            for (int i = 1; i < listOfStartHour.Count; i++)
            {
                listOfEndHour.Add(listOfStartHour[i]);
            }

            ViewBag.ListOfStartHours = new SelectList(listOfStartHour);
            ViewBag.ListOfEndHours = new SelectList(listOfEndHour);
            

            CreateReservationsVM rez = new CreateReservationsVM();
            rez.ReservationDate = System.DateTime.Now;

            return View(rez);
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateReservationsVM reservation)
        {
            Reservation r = new Reservation()
            {
                Id = reservation.Id,
                Name = reservation.Name,
                RoomId = reservation.RoomId,
            };

            {
                r.StartDate = new DateTime(reservation.ReservationDate.Year, reservation.ReservationDate.Month, reservation.ReservationDate.Day, getHour(reservation.StartHour), getMinute(reservation.StartHour), 0);
                r.EndDate = new DateTime(reservation.ReservationDate.Year, reservation.ReservationDate.Month, reservation.ReservationDate.Day, getHour(reservation.EndHour), getMinute(reservation.EndHour), 0);
            }

            if (ModelState.IsValid)
            {
                db.Reservations.Add(r);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            // aici incep

            //String currentTime = System.DateTime.Now.ToString("yyyy MM dd h:mm:ss tt");

            //StringBuilder builder = new StringBuilder();

            //List<string> listOfHours = new List<string>(getListOfHours()); // initializare lista bruta
            //List<string> listOfStartHour = new List<string>();
            //List<string> listOfEndHour = new List<string>();


            //for (int index = 0; index < listOfHours.Count; index++)
            //{
            //    //if (System.DateTime.Now.Day == reservation.)
            //}


            int isDifferentHours = reservation.StartHour.CompareTo(reservation.EndHour);

            if (isDifferentHours == -1)
            {
                ModelState.AddModelError("StartHour", "Error message for User");
            }





  
            // aici termin
            ViewBag.RoomId = new SelectList(db.Rooms, "Id", "Name", r.RoomId);
            return View(reservation);
        }

        // GET: Reservations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoomId = new SelectList(db.Rooms, "Id", "Name", reservation.RoomId);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,StartDate,EndDate,RoomId")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RoomId = new SelectList(db.Rooms, "Id", "Name", reservation.RoomId);
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reservation reservation = db.Reservations.Find(id);
            db.Reservations.Remove(reservation);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
