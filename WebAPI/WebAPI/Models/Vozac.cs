﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPI.Models.Enumeracije;

namespace WebAPI.Models
{
    public class Vozac : Korisnik
    {
        public Lokacija Lok { get; set; }
        public Automobil Auto { get; set; }

    }
}