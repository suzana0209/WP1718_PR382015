using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPI.Models.Enumeracije;

namespace WebAPI.Models
{
    public abstract class Korisnik
    {
        public string KorisnickoIme { get; set; }
        public string Lozinka { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public Pol Pol { get; set; }
        public string Jmbg { get; set; }
        public string KontaktTelefon { get; set; }
        public string Email { get; set; }
        public UlogaKorisnika Uloga { get; set; }
        //public List<Voznja> Voznje { get; set; }
        public List<Voznja> KorisnikVoznje { get; set; }
    }
}