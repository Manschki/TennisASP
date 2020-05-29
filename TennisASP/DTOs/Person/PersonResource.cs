using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TennisASP.DTOs.Person
{
    public class PersonResource : AbstractPerson
    {
        public int Id { get; set; }
        public int NrBookings { get; set; }

    }
}
