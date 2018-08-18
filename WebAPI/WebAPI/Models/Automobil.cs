using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPI.Models.Enumeracije;

namespace WebAPI.Models
{
    public class Automobil
    {
        public Automobil() { }

        public Automobil(string vozac, int godisteAutomobila, string brojRegistarskeOznake, int brojTaksiVozila, TipAutomobila tipAutomobila)
        {
            GodisteAutomobila = godisteAutomobila;
            BrojRegistarskeOznake = brojRegistarskeOznake;
            BrojTaksiVozila = brojTaksiVozila;
            TipAutomobila = tipAutomobila;
        }

        public Int32 GodisteAutomobila { get; set; }
        public string BrojRegistarskeOznake { get; set; }
        public int BrojTaksiVozila { get; set; }
        public TipAutomobila TipAutomobila { get; set; }
        //public bool Zauzeto { get; set; }
        public string UsernameVozaca { get; set; }
    }
}