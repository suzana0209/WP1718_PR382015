using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPI.Models.Enumeracije;

namespace WebAPI.Models
{
    public class Voznja
    {
        
        public DateTime DatumPorudzbine { get; set; }
        public Lokacija LokacijaDolaskaTaksija { get; set; } // Lokacija na koju taksi dolazi
        public TipAutomobila ZeljeniAutomobil { get; set; }
        public Musterija Musterija { get; set; }
        public Lokacija Odrediste { get; set; } // Lokacija na kojoj je voznja uspjesno zavrsena 
        // Ako je dispecer kreirao ili obradio voznju, a ako je vozac prihvatio  onda je ovo polje prazno
        public Dispecer DispecerFormirao { get; set; } 
        public Vozac VozacPrihvatio { get; set; }
        public double Iznos { get; set; }
        public Komentar Komentar { get; set; }
        public StatusVoznje StatusVoznje { get; set; }
    }
}