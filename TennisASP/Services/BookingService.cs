using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TennisDB;

namespace TennisASP.Services
{
    public class BookingService
    {
        private TennisContext db;

        public BookingService(TennisContext db)
        {
            this.db = db;
        }

        public List<Booking> GetBookings()
        {
            var res =  db.Bookings
                .Include(x => x.Person)
                .OrderBy(x => x.Week)
                .ToList();
            return res;
        }

        public Booking GetBookingById(int id)
        {
            return db.Bookings
                .Include(x => x.Person)
                .FirstOrDefault(x => x.Id == id);
        }

        public Booking AddBooking(Booking booking)
        {
            db.Bookings.Add(booking);
            db.SaveChanges();
            return GetBookingById(booking.Id);
        }

        public Booking UpdateBooking(Booking toEdit)
        {
            db.Bookings.Update(toEdit);
            db.SaveChanges();
            return GetBookingById(toEdit.Id);
        }

        public Booking DeleteBooking(int id)
        {
            Booking toDel = GetBookingById(id);
            db.Bookings.Remove(toDel);
            db.SaveChanges();
            return toDel;
        }
    }
}
