using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TennisASP.DTOs.Person;
using TennisASP.Extensions;
using TennisASP.Services;
using TennisDB;

namespace TennisASP.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {

        private PersonService service;

        public PersonsController(PersonService service)
        {
            this.service = service;
        }

        [HttpGet]
        public List<PersonResource> GetPersons()
        {
            return service.GetPersons()
                .Select(x => {
                    PersonResource p = new PersonResource().CopyPropertiesFrom(x);
                    p.NrBookings = x.Bookings.Count();
                    return p;
                 })
                .ToList();
        }

        [HttpGet("{Id}")]
        public PersonResource GetPersonById(int Id)
        {
            Person dbPerson = service.GetPersonById(Id);
            PersonResource p = new PersonResource().CopyPropertiesFrom(dbPerson);
            p.NrBookings = dbPerson.Bookings.Count();
            return p;
        }

        [HttpPost]
        public PersonResource AddPerson(PersonDTO dto)
        {
            Person toAdd = new Person().CopyPropertiesFrom(dto);
            Person dbPerson = service.AddPerson(toAdd);
            PersonResource p = new PersonResource().CopyPropertiesFrom(dbPerson);
            p.NrBookings = dbPerson.Bookings.Count();
            return p;
        }

        [HttpPut("{Id}")]
        public PersonResource UpdatePerson(int Id, PersonDTO dto)
        {
            Person toEdit = new Person().CopyPropertiesFrom(dto);
            toEdit.Id = Id;

            Person dbPerson = service.UpdatePerson(Id, toEdit);
            PersonResource p = new PersonResource().CopyPropertiesFrom(dbPerson);
            p.NrBookings = dbPerson.Bookings.Count();
            return p;
        }

        [HttpDelete("{Id}")]
        public PersonResource DeletePerson(int Id)
        {
            Person dbPerson = service.DeletePerson(Id);
            PersonResource p = new PersonResource().CopyPropertiesFrom(dbPerson);
            p.NrBookings = dbPerson.Bookings.Count();
            return p;
        }


    }
}