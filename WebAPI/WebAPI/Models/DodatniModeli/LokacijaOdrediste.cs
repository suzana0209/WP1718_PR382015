﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models.DodatniModeli
{
    public class LokacijaOdrediste
    {
        public double Cena { get; set; }
        public string XCoord { get; set; }
        public string YCoord { get; set; }
        public string Street { get; set; }
       
        public Voznja Voz { get; set; }
    }
}