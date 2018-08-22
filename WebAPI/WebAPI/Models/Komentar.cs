using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class Komentar
    {
        public Komentar()
        {
            OcjenaVoznje = 0;
        }
        public Komentar(string opis, DateTime datumObjave, string korisnikOstavioKom, string voznjaKom, int ocjenaVoznje)
        {
            Opis = opis;
            DatumObjave = datumObjave;
            KorisnikOstavioKom = korisnikOstavioKom;
            VoznjaKom = voznjaKom;
            OcjenaVoznje = ocjenaVoznje;
        }

        public string Opis { get; set; }
        public DateTime DatumObjave { get; set; }
        public string KorisnikOstavioKom { get; set; } //Korisnik koji je ostavio komentar

        public string VoznjaKom { get; set; } // Voznja na koju je komentar ostavljen
        public int OcjenaVoznje { get; set; }
    }
}