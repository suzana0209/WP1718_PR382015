using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPI.Models.Enumeracije;

namespace WebAPI.Models
{
    public class Vozac : Korisnik
    {
        public Vozac()
        {
            Uloga = UlogaKorisnika.Vozac;
            KorisnikVoznje = new List<Voznja>();
            Ime = "";
            Prezime = "";
            Blokiran = false;
        }

        public Lokacija Lok { get; set; }
        public Automobil Auto { get; set; }
        public bool Zauzet { get; set; }
        public bool Blokiran { get; set; }
    }
}