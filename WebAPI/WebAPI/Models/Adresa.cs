using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class Adresa
    {
        public Adresa()
        {
        }

        public Adresa(string formatAdrese)
        {
            FormatAdrese = formatAdrese;
        }

        public string FormatAdrese { get; set; }
        
    }
}