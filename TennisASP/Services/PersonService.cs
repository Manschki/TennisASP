using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TennisDB;
using TennisASP.DTOs.Person;
using TennisASP.Extensions;
using Microsoft.EntityFrameworkCore;

namespace TennisASP.Services
{
    public class PersonService
    {
        private TennisContext db;

        public PersonService(TennisContext db)
        {
            this.db = db;
        }

        public List<Person> GetPersons()
        {
            return db.Persons
                 .Include(x => x.Bookings)
                 .OrderBy(x => x.Firstname)
                 .ToList();
        }

        public Person GetPersonById(int id)
        {
            return db.Persons
                .Include(x => x.Bookings)
                .FirstOrDefault(x => x.Id == id);
        }

        public Person AddPerson(Person person)
        {
            db.Persons.Add(person);
            db.SaveChanges();
            person = GetPersonById(person.Id);
            return person;
        }

        public Person UpdatePerson(int id, Person person)
        {
            db.Persons.Update(person);
            db.SaveChanges();
            return GetPersonById(person.Id);
        }

        public Person DeletePerson(int id)
        {
            Person toDelete = GetPersonById(id);
            db.Persons.Remove(toDelete);
            db.SaveChanges();
            return toDelete;
        }
    }
}
