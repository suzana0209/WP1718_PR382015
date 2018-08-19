using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPI.Models.Enumeracije;

namespace WebAPI.Models
{
    public class Dispecer : Korisnik
    {
        public Dispecer()
        {
            Uloga = UlogaKorisnika.Dispecer;
            KorisnikVoznje = new List<Voznja>();
            Ime = "";
            Prezime = "";
        }
    }
}