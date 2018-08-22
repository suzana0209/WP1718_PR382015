using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class Lokacija
    {
        public Lokacija()
        {
            X = "";
            Y = "";
            Adresa = new Adresa();
        }
        public Lokacija(string x, string y, Adresa adresa)
        {
            X = x;
            Y = y;
            Adresa = adresa;
        }

        public string X { get; set; }
        public string Y { get; set; }
        public Adresa Adresa { get; set; }
    }
}