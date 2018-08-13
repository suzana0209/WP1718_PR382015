using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;
using WebAPI.Models.DodatniModeli;
using WebAPI.Models.Enumeracije;
using WebAPI.RadSaXml;

namespace WebAPI.Controllers
{
    public class RegILogController : ApiController
    {
        public static XMLData xml = new XMLData();

        [HttpPost]
        [ActionName("Register")]
        public bool Register([FromBody]MusterijaPomocni k)
        {
            string ss = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Musterije.xml");
            string ss1 = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Vozaci.xml");
            string ss2 = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Dispeceri.xml");
            List<Musterija> users = xml.ReadUsers(ss);
            List<Vozac> vozaci = xml.ReadDrivers(ss1);
            List<Dispecer> disp = xml.ReadDispecer(ss2);
            bool g = true;
            foreach (Musterija u in users)
            {
                if (u.KorisnickoIme == k.Username)
                {
                    g = false;
                }
            }
            foreach (Vozac u in vozaci)
            {
                if (u.KorisnickoIme == k.Username)
                {
                    g = false;
                }
            }
            foreach (Dispecer u in disp)
            {
                if (u.KorisnickoIme == k.Username)
                {
                    g = false;
                }
            }
            if (g)
            {
                Musterija user = new Musterija();
                user.KorisnickoIme = k.Username;
                user.Lozinka = k.Password;
                user.Ime = k.Ime;
                user.Prezime = k.Prezime;
                if (k.Pol == "Female")
                {
                    user.Pol = Pol.Zensko;
                }
                else
                {
                    user.Pol = Pol.Musko;
                }
                user.Jmbg = k.Jmbg;
                user.KontaktTelefon = k.Telefon;
                user.Email = k.Email;
                user.Uloga = UlogaKorisnika.Musterija;


                users.Add(user);
                xml.WriteUsers(users, ss);
                return true;
            }
            else
            {
                return false;
            }

        }
        [HttpPost]
        [ActionName("Login")]
        public Korisnik Login([FromBody]LogovaniKorisnik k)
        {
            string ss = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Musterije.xml");
            string ss1 = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Vozaci.xml");
            string ss2 = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Dispeceri.xml");
            List<Musterija> users = xml.ReadUsers(ss);
            List<Vozac> vozaci = xml.ReadDrivers(ss1);
            List<Dispecer> dispeceri = xml.ReadDispecer(ss2);
            foreach (var v in users)
            {
                if (v.KorisnickoIme == k.Username && v.Lozinka == k.Password)
                {
                    return v;
                }
            }
            foreach (var v in vozaci)
            {
                if (v.KorisnickoIme == k.Username && v.Lozinka == k.Password)
                {
                    return v;
                }
            }
            foreach (var v in dispeceri)
            {
                if (v.KorisnickoIme == k.Username && v.Lozinka == k.Password)
                {
                    return v;
                }
            }
            return null;
        }
    }
}
