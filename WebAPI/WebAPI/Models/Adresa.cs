using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class Adresa
    {
        public Adresa()
        {
            Ulica = "";
            Broj = "";
            NaseljenoMesto = "";
            PozivniBroj = 0;
        }

        public Adresa(string ulica, string broj, string naseljenoMesto, double pozivniBroj)
        {
            Ulica = ulica;
            Broj = broj;
            NaseljenoMesto = naseljenoMesto;
            PozivniBroj = pozivniBroj;
        }

        public String Ulica { get; set; }
        public String Broj { get; set; }
        public String NaseljenoMesto { get; set; }
        public Double PozivniBroj { get; set; }
        
    }
}