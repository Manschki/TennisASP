﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TennisDB
{
    public class Person
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int Age { get; set; }
        public List<Booking> Bookings { get; set; }
    }
}
