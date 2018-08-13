using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models.Enumeracije
{
    public enum StatusVoznje
    {
        KreiranaNaCekanju,
        Formirana,
        Obradjena,
        Prihvacena,
        Otkazana,
        Uspjesna,
        Neuspjesna
    }
}