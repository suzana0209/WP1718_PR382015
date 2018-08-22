using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPI.Models.Enumeracije;

namespace WebAPI.Models.DodatniModeli
{
    public class PodaciOVozacu
    {
        public bool Zauzet { get; set; }
        public TipAutomobila TipA { get; set; }
    }
}