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
    public class ProfController : ApiController
    {
        public static XMLData xml = new XMLData();
        [HttpGet]
        [ActionName("GetUserByUsername")]
        public Korisnik GetUserByUsername(string username)
        {
            string ss = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Musterije.xml");
            string ss1 = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Vozaci.xml");
            string ss2 = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Dispeceri.xml");
            List<Musterija> users = xml.ReadUsers(ss);
            List<Vozac> vozaci = xml.ReadDrivers(ss1);
            List<Dispecer> dispeceri = xml.ReadDispecer(ss2);
            foreach (Musterija us in users)
            {
                if (us.KorisnickoIme == username)
                {
                    Musterija kor = new Musterija();
                    kor.KorisnickoIme = us.KorisnickoIme;
                    kor.Ime = us.Ime;
                    kor.Prezime = us.Prezime;
                    kor.Uloga = us.Uloga;
                    kor.Email = us.Email;
                    kor.KontaktTelefon = us.KontaktTelefon;
                    kor.Pol = us.Pol;
                    kor.Lozinka = null;
                    kor.Jmbg = us.Jmbg;


                    return kor;
                }
            }
            foreach (Dispecer us in dispeceri)
            {
                if (us.KorisnickoIme == username)
                {
                    Dispecer kor = new Dispecer();
                    kor.KorisnickoIme = us.KorisnickoIme;
                    kor.Ime = us.Ime;
                    kor.Prezime = us.Prezime;
                    kor.Uloga = us.Uloga;
                    kor.Email = us.Email;
                    kor.KontaktTelefon = us.KontaktTelefon;
                    kor.Pol = us.Pol;
                    kor.Lozinka = null;
                    kor.Jmbg = us.Jmbg;
                    return kor;
                }
            }
            foreach (Vozac us in vozaci)
            {
                if (us.KorisnickoIme == username)
                {
                    Vozac kor = new Vozac();
                    kor.KorisnickoIme = us.KorisnickoIme;
                    kor.Ime = us.Ime;
                    kor.Prezime = us.Prezime;
                    kor.Uloga = us.Uloga;
                    kor.Email = us.Email;
                    kor.KontaktTelefon = us.KontaktTelefon;
                    kor.Pol = us.Pol;
                    kor.Lozinka = null;
                    kor.Jmbg = us.Jmbg;
                    return kor;
                }
            }
            return null;
        }
        [HttpPost]
        [ActionName("AddDriveCustomer")]
        public bool AddDriveCustomer([FromBody]VoznjaPomocna k)
        {
            string ss = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Musterije.xml");
            string ss1 = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Voznje.xml");
            List<Musterija> users = xml.ReadUsers(ss);
            List<Voznja> drives = xml.ReadDrives(ss1);
            // bool g = true;
            Korisnik c = new Musterija();
            Voznja drive = new Voznja();
            foreach (Korisnik u in users)
            {
                if (u.KorisnickoIme == k.korisnicko)
                {
                    c = u;
                    Adresa a = new Adresa(k.Street, k.Number, k.Town, Int32.Parse(k.PostalCode));
                    Lokacija l = new Lokacija(k.XCoord, k.YCoord, a);
                    drive.Musterija = (Musterija)c;
                    drive.LokacijaDolaskaTaksija = l;
                    if (k.tipAuta != "")
                    {
                        drive.ZeljeniAutomobil = (TipAutomobila)int.Parse(k.tipAuta);
                    }
                    drive.Iznos = 0;
                    // drive.Komentar = null;
                    drive.Komentar = new Komentar();
                    drive.DatumPorudzbine = String.Format("{0:F}", DateTime.Now);
                    //drive.Odrediste = null;
                    drive.Odrediste = new Lokacija();
                    drive.DispecerFormirao = new Dispecer();
                    //drive.VozacPrihvatio = null;
                    drive.VozacPrihvatio = new Vozac();
                    drive.StatusVoznje = StatusVoznje.KreiranaNaCekanju;
                    // u.Drives.Add(drive);
                    //  g = false;
                }
            }
            drives.Add(drive);
            xml.WriteUsers(users, ss);
            xml.WriteDrives(drives, ss1);
            return true;
        }
        [HttpPost]
        [ActionName("AddDriveDispecer")]
        public bool AddDriveDispecer([FromBody]VoznjaPomocna k)
        {
            string ss = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Dispeceri.xml");
            string ss1 = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Voznje.xml");
            string ss2 = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Vozaci.xml");

            List<Dispecer> users = xml.ReadDispecer(ss);
            List<Voznja> drives = xml.ReadDrives(ss1);
            List<Vozac> vozaci = xml.ReadDrivers(ss2);
            // bool g = true;
            Korisnik c = new Dispecer();
            Voznja drive = new Voznja();
            foreach (Dispecer u in users)
            {
                if (u.KorisnickoIme == k.korisnicko)
                {
                    c = u;
                    Adresa a = new Adresa(k.Street, k.Number, k.Town, Int32.Parse(k.PostalCode));
                    Lokacija l = new Lokacija(k.XCoord, k.YCoord, a);
                    drive.Musterija = new Musterija();
                    drive.LokacijaDolaskaTaksija = l;
                    if (k.tipAuta != "")
                    {
                        drive.ZeljeniAutomobil = (TipAutomobila)int.Parse(k.tipAuta);
                    }
                    drive.Iznos = 0;
                    //drive.Komentar = null;
                    drive.Komentar = new Komentar();
                    drive.DatumPorudzbine = String.Format("{0:F}", DateTime.Now);
                    //drive.Odrediste = null;
                    drive.Odrediste = new Lokacija();
                    drive.DispecerFormirao = (Dispecer)c;
                    //drive.VozacPrihvatio = null;
                    //drive.StatusVoznje = StatusVoznje.KreiranaNaCekanju;

                    drive.StatusVoznje = StatusVoznje.Formirana;
                }
            }

            bool imaSlobodan = false;
            foreach (Vozac v in vozaci)
            {
                if ((!v.Zauzet) && (v.Auto.TipAutomobila == (TipAutomobila)int.Parse(k.tipAuta)))
                {
                    v.Zauzet = true;
                    drive.VozacPrihvatio = v;
                    imaSlobodan = true;
                    break;
                }
            }
            if (!imaSlobodan)
            {
                drive.VozacPrihvatio = new Vozac();
                drive.StatusVoznje = StatusVoznje.KreiranaNaCekanju;
            }

            drives.Add(drive);
            xml.WriteDispecer(users, ss);
            xml.WriteDrivers(vozaci, ss2);
            xml.WriteDrives(drives, ss1);
            return true;
        }

        [HttpGet]
        [ActionName("GetDrives")]
        public List<Voznja> GetDrives(string username)
        {
            string ss = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Voznje.xml");

            List<Voznja> listaDrives = xml.ReadDrives(ss);
            List<Voznja> listaDrives1 = new List<Voznja>();
            foreach (Voznja d in listaDrives)
            {
                if (d.Musterija.KorisnickoIme == username || d.DispecerFormirao.KorisnickoIme == username || d.VozacPrihvatio.KorisnickoIme == username)
                {
                    listaDrives1.Add(d);
                }
            }
            return listaDrives1;
        }

        [HttpGet]
        [ActionName("GetAllDrives")]
        public List<Voznja> GetAllDrives(string username)
        {
            string ss = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Voznje.xml");
            List<Voznja> listaDrives = xml.ReadDrives(ss);

            return listaDrives;
        }

        [HttpGet]
        [ActionName("GetWaitingDrives")]
        public List<Voznja> GetWaitingDrives(string username)
        {
            string ss = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Voznje.xml");
            List<Voznja> listaDrives = xml.ReadDrives(ss);
            List<Voznja> listaDrives1 = new List<Voznja>();
            foreach (Voznja d in listaDrives)
            {
                if (d.StatusVoznje == StatusVoznje.KreiranaNaCekanju)
                {
                    listaDrives1.Add(d);
                }
            }
            return listaDrives1;
        }

        [HttpPost]
        [ActionName("Pretraga")]
        public List<Voznja> Pretraga([FromBody]PretraziModel k)
        {
            //string ss = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Voznje.xml");
            if (k.Drivess == null)
            {
                return new List<Voznja>();
            }
            //List<Voznja> listaDrives = xml.ReadDrives(ss);
            List<Voznja> listaDrives = k.Drivess;
            List<Voznja> listaDrives1 = k.Drivess;
            if (k.DatumOd != null)
            {
                listaDrives1 = listaDrives1.Where(o => DateTime.Parse(o.DatumPorudzbine) >= DateTime.Parse(k.DatumOd)).ToList();
            }
            if (k.DatumDo != null)
            {
                listaDrives1 = listaDrives1.Where(o => DateTime.Parse(o.DatumPorudzbine) <= DateTime.Parse(k.DatumDo)).ToList();
            }
            if (k.CenaOd != null)
            {
                listaDrives1 = listaDrives1.Where(o => o.Iznos >= Double.Parse(k.CenaOd)).ToList();
            }
            if (k.CenaDo != null)
            {
                listaDrives1 = listaDrives1.Where(o => o.Iznos <= Double.Parse(k.CenaDo)).ToList();
            }
            if (k.OcenaOd != null)
            {
                listaDrives1 = listaDrives1.Where(o => o.Komentar.OcjenaVoznje >= Double.Parse(k.OcenaOd)).ToList();
            }
            if (k.OcenaDo != null)
            {
                listaDrives1 = listaDrives1.Where(o => o.Komentar.OcjenaVoznje <= Double.Parse(k.OcenaDo)).ToList();
            }
            if ((UlogaKorisnika)int.Parse(k.Uloga) == UlogaKorisnika.Dispecer)
            {
                if (k.MustIme != null)
                {
                    listaDrives1 = listaDrives1.Where(o => o.Musterija.Ime.Contains(k.MustIme)).ToList();
                }
                if (k.MustPrezime != null)
                {
                    listaDrives1 = listaDrives1.Where(o => o.Musterija.Prezime.Contains(k.MustPrezime)).ToList();
                }
                if (k.VozIme != null)
                {
                    listaDrives1 = listaDrives1.Where(o => o.VozacPrihvatio.Ime.Contains(k.VozIme)).ToList();
                }
                if (k.VozPrezime != null)
                {
                    listaDrives1 = listaDrives1.Where(o => o.VozacPrihvatio.Prezime.Contains(k.VozPrezime)).ToList();
                }
            }
            return listaDrives1;
        }

        [HttpPost]
        [ActionName("Edit")]
        public bool Edit([FromBody]KorisnikPomocna k)
        {
            string ss = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Musterije.xml");
            string ss1 = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Vozaci.xml");
            string ss2 = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Dispeceri.xml");
            List<Musterija> users = xml.ReadUsers(ss);
            List<Vozac> vozaci = xml.ReadDrivers(ss1);
            List<Dispecer> disp = xml.ReadDispecer(ss2);
            bool g = true;
            bool imaGaUMus = false;
            foreach (Musterija u in users)
            {
                if (u.KorisnickoIme == k.Username)
                {
                    imaGaUMus = true;
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
                user.Lozinka = k.Prezime;
                user.Ime = k.Ime;
                user.Prezime = k.Prezime;
                if (k.Pol.ToString() == "Zensko")
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
        [ActionName("GetFilterUser")]
        public List<Voznja> GetFilterUser([FromBody]KorisnikFilter k)
        {
            //string ss = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Voznje.xml");
            //List<Voznja> listaDrives = xml.ReadDrives(ss);

            if (k.Drivess == null)
            {
                return new List<Voznja>();
            }
            List<Voznja> listaDrives = k.Drivess;
            List<Voznja> listaDrives1 = new List<Voznja>();

            if (k.Status == null || k.Status == "")
            {
                return listaDrives1;
            }
            foreach (Voznja d in listaDrives)
            {
                if (d.StatusVoznje == (StatusVoznje)int.Parse(k.Status))
                {
                    listaDrives1.Add(d);
                }
            }

            //if ((UlogaKorisnika)int.Parse(k.Uloga) == UlogaKorisnika.Musterija)
            //{
            //    foreach (Voznja d in listaDrives)
            //    {
            //        if (d.StatusVoznje == (StatusVoznje)int.Parse(k.Status) && d.Musterija.KorisnickoIme == k.Username)
            //        {
            //            listaDrives1.Add(d);
            //        }
            //    }
            //}
            //else if ((UlogaKorisnika)int.Parse(k.Uloga) == UlogaKorisnika.Vozac)
            //{
            //    foreach (Voznja d in listaDrives)
            //    {
            //        if (d.Stat == (StatusVoznje)int.Parse(k.Status) && d.Voz.KorisnickoIme == k.Username)
            //        {
            //            listaDrives1.Add(d);
            //        }
            //    }
            //}
            //else if ((UlogaKorisnika)int.Parse(k.Uloga) == UlogaKorisnika.Dispecer)
            //{
            //    foreach (Voznja d in listaDrives)
            //    {
            //        if (d.Stat == (StatusVoznje)int.Parse(k.Status) && d.Disp.KorisnickoIme == k.Username)
            //        {
            //            listaDrives1.Add(d);
            //        }
            //    }
            //}
            return listaDrives1;
        }
        [HttpPost]
        [ActionName("GetFilterUserAll")]
        public List<Voznja> GetFilterUserAll([FromBody]KorisnikFilter k)
        {
            //string ss = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Voznje.xml");
            if (k.Drivess == null)
            {
                return new List<Voznja>();
            }
            //List<Voznja> listaDrives = xml.ReadDrives(ss);
            List<Voznja> listaDrives = k.Drivess;
            List<Voznja> listaDrives1 = new List<Voznja>();
            if (k.Status == null || k.Status == "")
            {
                return listaDrives;
            }

            if ((UlogaKorisnika)int.Parse(k.Uloga) == UlogaKorisnika.Dispecer)
            {
                foreach (Voznja d in listaDrives)
                {
                    if (d.StatusVoznje == (StatusVoznje)int.Parse(k.Status))
                    {
                        listaDrives1.Add(d);
                    }
                }
            }

            return listaDrives1;
        }
        //[HttpGet]
        //[ActionName("SortingUser")]
        //public List<Voznja> SortingUser(string k)
        //{
        //    string ss = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Voznje.xml");
        //    List<Voznja> listaDrives = xml.ReadDrives(ss);
        //    List<Voznja> listaDrives1 = new List<Voznja>();
        //    foreach (Voznja d in listaDrives)
        //    {
        //        if (d.Musterija.KorisnickoIme == k || d.VozacPrihvatio.KorisnickoIme == k || d.DispecerFormirao.KorisnickoIme == k)
        //        {
        //            listaDrives1.Add(d);
        //        }
        //    }
        //    List<Voznja> sortiranaVoznja = listaDrives1.OrderByDescending(o => o.Komentar.OcjenaVoznje).ToList();
        //    return sortiranaVoznja;
        //}

        [HttpPost]
        [ActionName("SortingUser")]
        public List<Voznja> SortingUser([FromBody]KorisnikSort k)
        {
            // string ss = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Voznje.xml");
            //List<Voznja> listaDrives = xml.ReadDrives(ss);
            List<Voznja> listaDrives = k.Drivess;
            List<Voznja> sortiranaVoznja = new List<Voznja>();

            if (k.PoCemu == 0)
            {
                sortiranaVoznja = listaDrives.OrderByDescending(o => o.Komentar.OcjenaVoznje).ToList();
            }
            else if (k.PoCemu == 1)
            {
                sortiranaVoznja = listaDrives.OrderByDescending(o => DateTime.Parse(o.DatumPorudzbine)).ToList();
            }

            // List<Voznja> sortiranaVoznja = listaDrives.OrderByDescending(o => o.Komentar.OcjenaVoznje).ToList();
            return sortiranaVoznja;
        }
    }
}
