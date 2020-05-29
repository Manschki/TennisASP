using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TennisASP.DTOs.Person;

namespace TennisASP.DTOs.Booking
{
    public class BookingResource : AbstractBooking
    {
        public int Id { get; set; }
        public PersonResource PersonResource { get; set; }

    }
}
