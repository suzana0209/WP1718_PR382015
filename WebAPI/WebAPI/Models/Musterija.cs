using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPI.Models.Enumeracije;

namespace WebAPI.Models
{
    public class Musterija : Korisnik
    {
       public Musterija()
        {
            Uloga = UlogaKorisnika.Musterija;
        }
    }
}