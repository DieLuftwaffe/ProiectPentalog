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

		public ActionResult Index(int? id)
		{
			if (id == null)
			{
				var reservations = db.Reservations.Include(r => r.Room);

				return View(reservations.ToList());
			}
			else
			{
				var reservations = db.Reservations.Include(r => r.Room).Where(i => i.RoomId == id);
                ViewBag.SelectedRoom = db.Rooms.SingleOrDefault(r => r.Id == id);
				return View(reservations.ToList());
			}
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

        // GET: Reservations/Create
        public ActionResult Create()
        {
            
            CreateReservationsVM rez = new CreateReservationsVM();
            rez.ReservationDate = System.DateTime.Now;

            int actualHourVal = GetCurrentHour();
            int actualMinuteVal = GetCurrentMinute();

            List<string> listOfHours = new List<string>();
            List<string> listOfStartHour = new List<string>();
            List<string> listOfEndHour = new List<string>();

            if (rez.ReservationDate.Date == System.DateTime.Now.Date)
            {
                listOfHours = GetListOfHours(actualHourVal, actualMinuteVal);
            }
            else
            {
                listOfHours = GetListOfHours(0, 59);
            }

            listOfStartHour = listOfHours;
            rez.StartHour = listOfStartHour[0];
            listOfStartHour.Clear();
            listOfStartHour = GetListOfHours(0, 0);
            listOfEndHour = GetListOfHours(0, 0);

            ViewBag.RoomId = new SelectList(db.Rooms, "Id", "Name");
            ViewBag.ListOfStartHours = new SelectList(listOfStartHour);
            ViewBag.ListOfEndHours = new SelectList(listOfEndHour);
            
            return View(rez);
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateReservationsVM reservation)
        {
            //Validarea suprema
            foreach (var elem in db.Reservations)
            {
                if(reservation.RoomId == elem.RoomId)
                {
                    if (reservation.StartHour != null && reservation.EndHour != null)
                    {
                        if (reservation.ReservationDate.Date == elem.EndDate.Date && reservation.ReservationDate.Date == elem.StartDate.Date)
                        {
                            ModelState.AddModelError("", "This reservation is already in memory!");
                        }
                    }
                }
            }

            //Validari
            if (reservation.Name == null)
            {
                ModelState.AddModelError("Name", "Enter name!");
            }

            if (reservation.RoomId <= 0)
            {
                ModelState.AddModelError("RoomId", "Select room name!");
            }

            DateTime currentDateTime = System.DateTime.Now.Date;

            if (reservation.ReservationDate < currentDateTime)
            {
                ModelState.AddModelError("ReservationDate", "Wrong date!");
            }

            if (reservation.ReservationDate == currentDateTime)
            {
                if (GetHour(reservation.StartHour).CompareTo(currentDateTime.Hour) == -1)
                {
                    ModelState.AddModelError("StartHour", "Wrong Hours!");
                }
            }

            if (reservation.StartHour.CompareTo(reservation.EndHour) == 0)
            {
                ModelState.AddModelError("", "Choose different hours!");
            }

            if (reservation.StartHour == null && reservation.EndHour == null)
            {
                ModelState.AddModelError("StartHour", "Enter Hours!");
                ModelState.AddModelError("EndHour", "Enter Hours!");
            }
            else
            {
                int isDifferentHours = reservation.StartHour.CompareTo(reservation.EndHour);

                if (isDifferentHours == 1)
                {
                    ModelState.AddModelError("EndHour", "The last Last Hour cannot be less then Start Hour!");
                }
            }

            if (ModelState.IsValid)
            {
                Reservation r = new Reservation()
                {
                    Id = reservation.Id,
                    Name = reservation.Name,
                    RoomId = reservation.RoomId,
                };

                r.StartDate = new DateTime(reservation.ReservationDate.Year, reservation.ReservationDate.Month, reservation.ReservationDate.Day, GetHour(reservation.StartHour), GetMinute(reservation.StartHour), 0);
                r.EndDate = new DateTime(reservation.ReservationDate.Year, reservation.ReservationDate.Month, reservation.ReservationDate.Day, GetHour(reservation.EndHour), GetMinute(reservation.EndHour), 0);
               
                db.Reservations.Add(r);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            int actualHourVal = GetCurrentHour();
            int actualMinuteVal = GetCurrentMinute();

            List<string> listOfHours = new List<string>();
            List<string> listOfStartHour = new List<string>();
            List<string> listOfEndHour = new List<string>();

            if (reservation.ReservationDate.Date == System.DateTime.Now.Date)
            {
                listOfHours = GetListOfHours(actualHourVal, actualMinuteVal);
            }
            else
            {
                listOfHours = GetListOfHours(0, 59);
            }

            listOfStartHour = listOfHours;
            listOfStartHour.Clear();
            listOfStartHour = GetListOfHours(0, 0);
            listOfEndHour = GetListOfHours(0, 0);

            ViewBag.ListOfStartHours = new SelectList(listOfStartHour);
            ViewBag.ListOfEndHours = new SelectList(listOfEndHour);

            ViewBag.RoomId = new SelectList(db.Rooms, "Id", "Name", reservation.RoomId);
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

        private int GetCurrentHour()
        {
            string currentHour = System.DateTime.Now.Hour.ToString();
            return Int32.Parse(currentHour);
        }

        private int GetCurrentMinute()
        {
            string currentMinute = System.DateTime.Now.Minute.ToString();
            return Int32.Parse(currentMinute);
        }

        private int GetHour(string dateTime)
        {
            return Int32.Parse(dateTime.Substring(0, 2));
        }

        private int GetMinute(string dateTime)
        {
            return Int32.Parse(dateTime.Substring(3, 2));
        }

        private List<string> GetListOfHours(int startHourList, int startMinuteList)
        {
            List<string> listOfHours = new List<string>();

            StringBuilder builder = new StringBuilder();

            for (int index_hour = 0; index_hour <= 24; index_hour++) //TO DO HERE
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

                    if (index_hour < 24)
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
                    else
                    {
                        builder.Append(index_hour);
                        builder.Append(":00");
                        listOfHours.Add(builder.ToString());
                        builder.Clear();
                    }
                }
            }

            return listOfHours;
        }
    }
}
