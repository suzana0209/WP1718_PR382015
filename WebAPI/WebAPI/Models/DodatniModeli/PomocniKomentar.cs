using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models.DodatniModeli
{
    public class PomocniKomentar
    {
        public string KomOpis { get; set; }
        public string KomOcena { get; set; }
        public Voznja Voz { get; set; }
    }
}