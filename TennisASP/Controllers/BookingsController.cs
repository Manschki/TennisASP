using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TennisASP.DTOs.Booking;
using TennisASP.DTOs.Person;
using TennisASP.Extensions;
using TennisASP.Services;
using TennisDB;

namespace TennisASP.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {

        private BookingService service;
        private PersonService personService;

        public BookingsController(BookingService service, PersonService personService)
        {
            this.service = service;
            this.personService = personService;
        }

        [HttpGet]
        public List<BookingResource> GetBoookings()
        {
            return service.GetBookings()
                .Select(x => {
                    BookingResource b = new BookingResource().CopyPropertiesFrom(x);
                    b.PersonResource = GetPersonResFromBooking(x);
                    return b;
                  })
                .ToList();
        }

        [HttpGet("{Id}")]
        public BookingResource GetBookingById(int Id)
        {
            Booking dbBooking = service.GetBookingById(Id);
            BookingResource b = new BookingResource().CopyPropertiesFrom(dbBooking);
            b.PersonResource = GetPersonResFromBooking(dbBooking);
            return b;
        }

        [HttpPost]
        public BookingResource AddBooking(BookingDTO dto)
        {
            Booking toAdd = new Booking().CopyPropertiesFrom(dto);
            Booking dbBooking = service.AddBooking(toAdd);
            BookingResource b = new BookingResource().CopyPropertiesFrom(dbBooking);
            b.PersonResource = GetPersonResFromBooking(dbBooking);
            return b;
        }

        [HttpPut("{id}")]
        public BookingResource UpdateBooking(int Id, BookingDTO dto)
        {
            Booking toEdit = new Booking().CopyPropertiesFrom(dto);
            toEdit.Id = Id;
            Booking dbBooking = service.UpdateBooking(toEdit);
            BookingResource b = new BookingResource().CopyPropertiesFrom(dbBooking);
            b.PersonResource = GetPersonResFromBooking(dbBooking);
            return b;
        }

        [HttpDelete("{Id}")]
        public BookingResource DeleteBooking(int Id)
        {
            Booking dbBooking = service.DeleteBooking(Id);
            BookingResource b = new BookingResource().CopyPropertiesFrom(dbBooking);
            b.PersonResource = GetPersonResFromBooking(dbBooking);
            return b;
        }

        private PersonResource GetPersonResFromBooking(Booking booking)
        {
            Person dbPerson = personService.GetPersonById(booking.PersonId);
            PersonResource p = new PersonResource().CopyPropertiesFrom(dbPerson);
            p.NrBookings = dbPerson.Bookings.Count();
            return p;
        }
    }
}