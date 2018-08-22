using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models.DodatniModeli
{
    public class VozacevKomentar
    {
        public string Kometar { get; set; }
        public Voznja Voz { get; set; }
    }
}