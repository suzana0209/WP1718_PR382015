﻿using System;
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
        [ActionName("GetUserStatusByUsername")]
        public bool GetUserStatusByUsername(string username)
        {
            string ss = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Musterije.xml");
            string ss1 = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Vozaci.xml");
            List<Musterija> users = xml.ReadUsers(ss);
            List<Vozac> vozaci = xml.ReadDrivers(ss1);

            bool blokiran = false;
            foreach (Musterija m in users)
            {
                if (m.KorisnickoIme == username && m.Blokiran)
                {
                    blokiran = true;
                    break;
                }
            }
            if (!blokiran)
            {
                foreach (Vozac m in vozaci)
                {
                    if (m.KorisnickoIme == username && m.Blokiran)
                    {
                        blokiran = true;
                        break;
                    }
                }
            }
            return blokiran;
        }


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

            Korisnik c = new Musterija();
            Voznja drive = new Voznja();
            foreach (Korisnik u in users)
            {
                if (u.KorisnickoIme == k.korisnicko)
                {
                    c = u;
                    Adresa a = new Adresa(k.Street);
                    Lokacija l = new Lokacija(k.XCoord, k.YCoord, a);
                    drive.Musterija = (Musterija)c;
                    drive.LokacijaDolaskaTaksija = l;
                    if (k.tipAuta != "")
                    {
                        drive.ZeljeniAutomobil = (TipAutomobila)int.Parse(k.tipAuta);
                    }

                    drive.Iznos = 0;
                    drive.Komentar = new Komentar();
                    drive.DatumPorudzbine = String.Format("{0:F}", DateTime.Now);
                    drive.Odrediste = new Lokacija();
                    drive.DispecerFormirao = new Dispecer();
                    drive.VozacPrihvatio = new Vozac();
                    drive.StatusVoznje = StatusVoznje.KreiranaNaCekanju;
                }
            }
            drives.Add(drive);
            xml.WriteUsers(users, ss);
            xml.WriteDrives(drives, ss1);
            return true;
        }

        [HttpPost]
        [ActionName("AddDriveDispecer")]
        public List<string> AddDriveDispecer([FromBody]VoznjaPomocna k)
        {
            string ss = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Dispeceri.xml");
            string ss1 = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Voznje.xml");
            string ss2 = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Vozaci.xml");

            List<Dispecer> users = xml.ReadDispecer(ss);
            List<Voznja> drives = xml.ReadDrives(ss1);
            List<Vozac> vozaci = xml.ReadDrivers(ss2);

            bool g = true;
            Korisnik c = new Dispecer();
            Voznja drive = new Voznja();

            List<Tuple<Point, string>> prosledi = new List<Tuple<Point, string>>();
            foreach (Vozac v in vozaci)
            {
                if (!v.Zauzet && v.Auto.TipAutomobila == (TipAutomobila)int.Parse(k.tipAuta) && !v.Blokiran)
                {
                    Point pos = new Point(Double.Parse(v.Lok.X), Double.Parse(v.Lok.Y));
                    prosledi.Add(new Tuple<Point, string>(pos, v.KorisnickoIme));
                }
            }
            NajkracaUdaljenost nk = new NajkracaUdaljenost();
            List<string> ret = new List<string>();

            if (!prosledi.Any())
            {
                foreach (Dispecer u in users)
                {
                    if (u.KorisnickoIme == k.korisnicko)
                    {
                        c = u;
                        Adresa a = new Adresa(k.Street);
                        Lokacija l = new Lokacija(k.XCoord, k.YCoord, a);
                        drive.Musterija = new Musterija();
                        drive.LokacijaDolaskaTaksija = l;
                        if (k.tipAuta != "")
                        {
                            drive.ZeljeniAutomobil = (TipAutomobila)int.Parse(k.tipAuta);
                        }
                        drive.Iznos = 0;
                        drive.Komentar = new Komentar();
                        drive.DatumPorudzbine = String.Format("{0:F}", DateTime.Now); ;
                        drive.Odrediste = new Lokacija();
                        drive.DispecerFormirao = (Dispecer)c;
                        drive.StatusVoznje = StatusVoznje.KreiranaNaCekanju;
                        drive.VozacPrihvatio = new Vozac();
                        break;
                    }
                }
                drives.Add(drive);
                xml.WriteDrives(drives, ss1);
            }
            else
            {
                ret = nk.OrderByDistance(prosledi, new Point(Double.Parse(k.XCoord), Double.Parse(k.YCoord)));
            }
            return ret;
        }

        [HttpPost]
        [ActionName("DodajVoznjuDisp")]
        public bool DodajVoznjuDisp([FromBody]KonacnaVoznja k)
        {

            string ss = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Dispeceri.xml");
            string ss1 = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Voznje.xml");
            string ss2 = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Vozaci.xml");
            List<Dispecer> users = xml.ReadDispecer(ss);
            List<Voznja> drives = xml.ReadDrives(ss1);
            List<Vozac> vozaci = xml.ReadDrivers(ss2);
            bool g = true;

            Korisnik c = new Dispecer();
            Voznja drive = new Voznja();
            foreach (Dispecer u in users)
            {
                if (u.KorisnickoIme == k.korisnickoImeAdmin)
                {
                    c = u;
                    Adresa a = new Adresa(k.voznja.Street);
                    Lokacija l = new Lokacija(k.voznja.XCoord, k.voznja.YCoord, a);

                    drive.Musterija = new Musterija();
                    drive.LokacijaDolaskaTaksija = l;

                    if (k.voznja.tipAuta != "")
                    {
                        drive.ZeljeniAutomobil = (TipAutomobila)int.Parse(k.voznja.tipAuta);
                    }
                    drive.Iznos = 0;
                    drive.Komentar = new Komentar();
                    drive.DatumPorudzbine = String.Format("{0:F}", DateTime.Now);
                    drive.Odrediste = new Lokacija();
                    drive.DispecerFormirao = (Dispecer)c;
                    drive.StatusVoznje = StatusVoznje.Formirana;
                }
            }

            foreach (Vozac v in vozaci)
            {
                if (v.KorisnickoIme == k.korisnickoImeVozac)
                {
                    v.Zauzet = true;
                    drive.VozacPrihvatio = v;

                    break;
                }
            }

            drives.Add(drive);
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
            if (k.Drivess == null)
            {
                return new List<Voznja>();
            }

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
        public int Edit([FromBody]EditModel k)
        {
            string ss = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Musterije.xml");

            string adm = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Dispeceri.xml");
            string drv = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Vozaci.xml");
            string voznje = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Voznje.xml");
            string auta = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Automobili.xml");

            List<Musterija> users = xml.ReadUsers(ss);
            List<Dispecer> admins = xml.ReadDispecer(adm);
            List<Vozac> drivers = xml.ReadDrivers(drv);
            List<Voznja> lv = xml.ReadDrives(voznje);
            List<Automobil> la = xml.ReadAuto(auta);

            Vozac vv = new Vozac();
            Musterija mm = new Musterija();
            Dispecer dd = new Dispecer();

            bool g = true;
            int q = 3;
            if (k.OldUsername != k.Username)
            {
                foreach (Musterija u in users)
                {
                    if (u.KorisnickoIme == k.Username)
                    {
                        g = false;
                    }
                }

                foreach (Dispecer ad in admins)
                {
                    if (ad.KorisnickoIme == k.Username)
                    {
                        g = false;
                    }
                }
                foreach (Vozac dr in drivers)
                {
                    if (dr.KorisnickoIme == k.Username)
                    {
                        g = false;
                    }
                }
            }
            if (g)
            {
                foreach (Musterija u in users)
                {
                    if (u.KorisnickoIme == k.OldUsername)
                    {
                        u.KorisnickoIme = k.Username;

                        if (k.Password != null)
                        {
                            u.Lozinka = k.Password;
                        }

                        u.Ime = k.Ime;
                        u.Prezime = k.Prezime;
                        if (k.Pol == "Zensko" || (k.Pol == "1"))
                        {
                            u.Pol = Pol.Zensko;
                        }
                        else
                        {
                            u.Pol = Pol.Musko;
                        }
                        u.Jmbg = k.Jmbg;
                        u.KontaktTelefon = k.Telefon;
                        u.Email = k.Email;     
                        q = 2;
                        mm = u;
                        break;
                    }
                }

                foreach (Dispecer u in admins)
                {
                    if (u.KorisnickoIme == k.OldUsername)
                    {
                        u.KorisnickoIme = k.Username;
                        if (k.Password != null)
                        {
                            u.Lozinka = k.Password;
                        }
                        u.Ime = k.Ime;
                        u.Prezime = k.Prezime;
                        if (k.Pol == "Zensko" || k.Pol == "1")
                        {
                            u.Pol = Pol.Zensko;
                        }
                        else
                        {
                            u.Pol = Pol.Musko;
                        }
                        u.Jmbg = k.Jmbg;
                        u.KontaktTelefon = k.Telefon;
                        u.Email = k.Email;
                        q = 1;
                        dd = u;
                        break;
                    }
                }

                foreach (Vozac u in drivers)
                {
                    if (u.KorisnickoIme == k.OldUsername)
                    {
                        u.KorisnickoIme = k.Username;
                        if (k.Password != null)
                        {
                            u.Lozinka = k.Password;
                        }
                        u.Ime = k.Ime;
                        u.Prezime = k.Prezime;
                        if (k.Pol == "1" || k.Pol == "Zensko")
                        {
                            u.Pol = Pol.Zensko;
                        }
                        else
                        {
                            u.Pol = Pol.Musko;
                        }
                        u.Jmbg = k.Jmbg;
                        u.KontaktTelefon = k.Telefon;
                        u.Email = k.Email;
                        u.Auto.UsernameVozaca = k.Username;
                        q = 0;
                        vv = u;
                        break;
                    }
                }

                if (q == 2)
                {
                    xml.WriteUsers(users, ss);
                    foreach (Voznja v in lv)
                    {
                        if (v.Musterija.KorisnickoIme == k.OldUsername)
                        {
                            v.Musterija.KorisnickoIme = mm.KorisnickoIme;
                            v.Musterija.Lozinka = mm.Lozinka;
                            v.Musterija.Ime = mm.Ime;
                            v.Musterija.Prezime = mm.Prezime;
                            v.Musterija.Pol = mm.Pol;
                            v.Musterija.Uloga = mm.Uloga;
                            v.Musterija.Jmbg = mm.Jmbg;
                            v.Musterija.KontaktTelefon = mm.KontaktTelefon;
                            v.Musterija.Email = mm.Email;
                        }
                    }
                }
                if (q == 1)
                {
                    xml.WriteDispecer(admins, adm);
                    foreach (Voznja v in lv)
                    {
                        if (v.DispecerFormirao.KorisnickoIme == k.OldUsername)
                        {
                            v.DispecerFormirao.KorisnickoIme = dd.KorisnickoIme;
                            v.DispecerFormirao.Lozinka = dd.Lozinka;
                            v.DispecerFormirao.Ime = dd.Ime;
                            v.DispecerFormirao.Prezime = dd.Prezime;
                            v.DispecerFormirao.Pol = dd.Pol;
                            v.DispecerFormirao.Uloga = dd.Uloga;
                            v.DispecerFormirao.Jmbg = dd.Jmbg;
                            v.DispecerFormirao.KontaktTelefon = dd.KontaktTelefon;
                            v.DispecerFormirao.Email = dd.Email;

                        }
                    }
                }
                if (q == 0)
                {
                    xml.WriteDrivers(drivers, drv);
                    foreach (Voznja v in lv)
                    {
                        if (v.VozacPrihvatio.KorisnickoIme == k.OldUsername)
                        {
                            v.VozacPrihvatio.KorisnickoIme = vv.KorisnickoIme;
                            v.VozacPrihvatio.Lozinka = vv.Lozinka;
                            v.VozacPrihvatio.Ime = vv.Ime;
                            v.VozacPrihvatio.Prezime = vv.Prezime;
                            v.VozacPrihvatio.Pol = vv.Pol;
                            v.VozacPrihvatio.Uloga = vv.Uloga;
                            v.VozacPrihvatio.Jmbg = vv.Jmbg;
                            v.VozacPrihvatio.KontaktTelefon = vv.KontaktTelefon;
                            v.VozacPrihvatio.Email = vv.Email;
                            v.VozacPrihvatio.Auto.UsernameVozaca = vv.KorisnickoIme;
                        }
                    }
                    foreach (Automobil a in la)
                    {
                        if (a.UsernameVozaca == k.OldUsername)
                        {
                            a.UsernameVozaca = vv.KorisnickoIme;
                        }
                    }

                }
                xml.WriteAuta(la, auta);
                xml.WriteDrives(lv, voznje);
                return q;
            }
            else
            {
                return q;
            }
        }

        [HttpPost]
        [ActionName("GetFilterUser")]
        public List<Voznja> GetFilterUser([FromBody]KorisnikFilter k)
        {
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

            return listaDrives1;
        }
        [HttpPost]
        [ActionName("GetFilterUserAll")]
        public List<Voznja> GetFilterUserAll([FromBody]KorisnikFilter k)
        {
            if (k.Drivess == null)
            {
                return new List<Voznja>();
            }
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
        

        [HttpPost]
        [ActionName("SortingUser")]
        public List<Voznja> SortingUser([FromBody]KorisnikSort k)
        {
            string ss = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Vozaci.xml");
            List<Vozac> lv = xml.ReadDrivers(ss);

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

            else if (k.PoCemu == 2)
            {
                Point np = new Point();
                foreach (Vozac v in lv)
                {
                    if (v.KorisnickoIme == k.Username)
                    {
                        np.X = Double.Parse(v.Lok.X);
                        np.Y = Double.Parse(v.Lok.Y);
                        break;
                    }
                }
                NajkracaUdaljenost nu = new NajkracaUdaljenost();
                sortiranaVoznja = nu.OrderByDistanceZaVoz(listaDrives, np);
            }

            return sortiranaVoznja;
        }

        [HttpPost]
        [ActionName("OtkaziVoznju")]
        public Voznja OtkaziVoznju([FromBody]PomocnaVoznja k)
        {
            string ss = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Voznje.xml");
            List<Voznja> lista = xml.ReadDrives(ss);
            Voznja ret = new Voznja();
            foreach (Voznja v in lista)
            {
                if (v.Musterija.KorisnickoIme == k.Voznj.Musterija.KorisnickoIme && DateTime.Parse(v.DatumPorudzbine) == DateTime.Parse(k.Voznj.DatumPorudzbine))
                {
                    v.StatusVoznje = StatusVoznje.Otkazana;
                    ret = v;
                    break;
                }
            }
            xml.WriteDrives(lista, ss);
            return ret;
        }

        [HttpPost]
        [ActionName("Komentarisanje")]
        public bool Komentarisanje([FromBody]PomocniKomentar k)
        {
            string ss = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Voznje.xml");
            List<Voznja> lista = xml.ReadDrives(ss);
            bool ret = false;
            foreach (Voznja v in lista)
            {
                if (v.Musterija.KorisnickoIme == k.Voz.Musterija.KorisnickoIme && DateTime.Parse(v.DatumPorudzbine) == DateTime.Parse(k.Voz.DatumPorudzbine))
                {
                    v.Komentar.DatumObjave = DateTime.Parse(String.Format("{0:F}", DateTime.Now));
                    v.Komentar.Opis = k.KomOpis;
                    v.Komentar.OcjenaVoznje = Int32.Parse(k.KomOcena);
                    v.Komentar.KorisnikOstavioKom = k.Voz.Musterija.KorisnickoIme;
                    ret = true;
                    break;
                }
            }
            xml.WriteDrives(lista, ss);
            return ret;
        }


        [HttpPost]
        [ActionName("ObradiVoznjuDisp")]
        public bool ObradiVoznjuDisp([FromBody]KonacnaVoznjaDisp k)
        {

            string ss = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Dispeceri.xml");
            string ss1 = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Voznje.xml");
            string ss2 = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Vozaci.xml");
            List<Dispecer> users = xml.ReadDispecer(ss);
            List<Voznja> drives = xml.ReadDrives(ss1);
            List<Vozac> vozaci = xml.ReadDrivers(ss2);

            Dispecer c = new Dispecer();
            Vozac vozac = new Vozac();
            Voznja voznja = new Voznja();
            foreach (Dispecer u in users)
            {
                if (u.KorisnickoIme == k.korisnickoImeAdmin)
                {
                    c = u;
                }
            }

            foreach (Vozac v in vozaci)
            {
                if (v.KorisnickoIme == k.korisnickoImeVozac)
                {
                    v.Zauzet = true;
                    vozac = v;
                    break;
                }

            }
            foreach (Voznja vo in drives)
            {
                if (DateTime.Parse(vo.DatumPorudzbine) == DateTime.Parse(k.voznja.DatumPorudzbine) && 
                    (vo.Musterija.KorisnickoIme == k.voznja.Musterija.KorisnickoIme || vo.DispecerFormirao.KorisnickoIme == k.voznja.DispecerFormirao.KorisnickoIme))
                {
                    vo.VozacPrihvatio = vozac;
                    vo.DispecerFormirao = c;
                    vo.StatusVoznje = StatusVoznje.Obradjena;
                }
            }

            xml.WriteDrivers(vozaci, ss2);
            xml.WriteDrives(drives, ss1);

            return true;
        }

        [HttpPost]
        [ActionName("ObradiVoznju")]
        public List<string> ObradiVoznju([FromBody]VozacevKomentar k)
        {
            string ss = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Voznje.xml");
            string ss1 = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Vozaci.xml");
            string ss2 = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Dispeceri.xml");
            List<Voznja> lista = xml.ReadDrives(ss);
            List<Vozac> lv = xml.ReadDrivers(ss1);
            List<Dispecer> dispi = xml.ReadDispecer(ss2);
            Dispecer dispecer = new Dispecer();
            Vozac vozacsl = new Vozac();
            Voznja voznja = new Voznja();

            List<Tuple<Point, string>> prosledi = new List<Tuple<Point, string>>();
            foreach (Vozac v in lv)
            {
                if (!v.Zauzet && v.Auto.TipAutomobila == k.Voz.ZeljeniAutomobil && !v.Blokiran)
                {
                    Point pos = new Point(Double.Parse(v.Lok.X), Double.Parse(v.Lok.Y));
                    prosledi.Add(new Tuple<Point, string>(pos, v.KorisnickoIme));

                }

            }
            NajkracaUdaljenost nk = new NajkracaUdaljenost();
            List<string> ret = new List<string>();

            if (!prosledi.Any())
            {
                return new List<string>();
            }
            else
            {
                ret = nk.OrderByDistance(prosledi, new Point(Double.Parse(k.Voz.LokacijaDolaskaTaksija.X), Double.Parse(k.Voz.LokacijaDolaskaTaksija.Y)));

            }

            return ret;
        }


        [HttpPost]
        [ActionName("PreuzmiVoznju")]
        public List<Voznja> PreuzmiVoznju([FromBody]ModelZaObradiVoznju k)
        {
            string ss = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Voznje.xml");
            string ss1 = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Vozaci.xml");

            List<Voznja> lista = xml.ReadDrives(ss);
            List<Vozac> lv = xml.ReadDrivers(ss1);

            Vozac vozacsl = new Vozac();
            Voznja voznja = new Voznja();
            bool nasao = false;
            foreach (Vozac vv in lv)
            {
                if (vv.KorisnickoIme == k.Username)
                {
                    nasao = true;
                    vv.Zauzet = true;
                    vozacsl = vv;
                    break;
                }
            }
            List<Voznja> retL = k.ListaVoznji;
            if (nasao)
            {
                foreach (Voznja v in lista)
                {
                    if ((v.Musterija.KorisnickoIme == k.Voznj.Musterija.KorisnickoIme || v.DispecerFormirao.KorisnickoIme == k.Voznj.DispecerFormirao.KorisnickoIme) &&
                        DateTime.Parse(v.DatumPorudzbine) == DateTime.Parse(k.Voznj.DatumPorudzbine))
                    {
                        v.StatusVoznje = StatusVoznje.Prihvacena;
                        v.VozacPrihvatio = vozacsl;
                        break;
                    }
                }
                foreach (Voznja vvv in retL)
                {
                    if ((vvv.Musterija.KorisnickoIme == k.Voznj.Musterija.KorisnickoIme || vvv.DispecerFormirao.KorisnickoIme == k.Voznj.DispecerFormirao.KorisnickoIme) &&
                        DateTime.Parse(vvv.DatumPorudzbine) == DateTime.Parse(k.Voznj.DatumPorudzbine))
                    {
                        retL.Remove(vvv);
                        break;
                    }
                }
                xml.WriteDrives(lista, ss);
                xml.WriteDrivers(lv, ss1);
            }
            return retL;
        }

        [HttpGet]
        [ActionName("getDriverData")]
        public PodaciOVozacu getDriverData(string username)
        {
            string ss = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Vozaci.xml");
            PodaciOVozacu po = new PodaciOVozacu();
            List<Vozac> listaDrives = xml.ReadDrivers(ss);
            bool nasao = false;
            foreach (Vozac d in listaDrives)
            {
                if (d.KorisnickoIme == username)
                {
                    po.Zauzet = d.Zauzet;
                    po.TipA = d.Auto.TipAutomobila;
                    po.TrenutnaLokacija = d.Lok;
                    nasao = true;
                    break;
                }
            }

            if (!nasao)
            {
                po.Zauzet = true;
                po.TrenutnaLokacija = new Lokacija();
                po.TipA = TipAutomobila.NemaTip;
            }
            return po;
        }

        [HttpPost]
        [ActionName("KomentarisanjeVozac")]
        public bool KomentarisanjeVozac([FromBody]VozacevKomentar k)
        {
            string ss = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Voznje.xml");
            string ss1 = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Vozaci.xml");
            List<Voznja> lista = xml.ReadDrives(ss);
            List<Vozac> lv = xml.ReadDrivers(ss1);
            bool ret = false;
            foreach (Voznja v in lista)
            {
                if (v.VozacPrihvatio.KorisnickoIme == k.Voz.VozacPrihvatio.KorisnickoIme &&
                    DateTime.Parse(v.DatumPorudzbine) == DateTime.Parse(k.Voz.DatumPorudzbine))
                {
                    v.Komentar.DatumObjave = DateTime.Parse(String.Format("{0:F}", DateTime.Now));
                    v.Komentar.Opis = k.Kometar;
                    v.Komentar.OcjenaVoznje = 0;
                    v.Komentar.KorisnikOstavioKom = k.Voz.VozacPrihvatio.KorisnickoIme;
                    v.VozacPrihvatio.Zauzet = false;
                    v.StatusVoznje = StatusVoznje.Neuspjesna;
                    ret = true;
                    break;
                }
            }
            if (ret)
            {
                foreach (Vozac vo in lv)
                {
                    if (vo.KorisnickoIme == k.Voz.VozacPrihvatio.KorisnickoIme)
                    {
                        vo.Zauzet = false;
                        break;
                    }
                }
            }
            xml.WriteDrives(lista, ss);
            xml.WriteDrivers(lv, ss1);
            return ret;
        }


        [HttpPost]
        [ActionName("DodajKraj")]
        public bool DodajKraj([FromBody]LokacijaOdrediste k)
        {
            string ss = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Voznje.xml");
            string ss1 = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Vozaci.xml");
            List<Voznja> lista = xml.ReadDrives(ss);
            List<Vozac> lv = xml.ReadDrivers(ss1);
            bool ret = false;
            foreach (Voznja v in lista)
            {
                if (v.VozacPrihvatio.KorisnickoIme == k.Voz.VozacPrihvatio.KorisnickoIme &&
                    DateTime.Parse(v.DatumPorudzbine) == DateTime.Parse(k.Voz.DatumPorudzbine))
                {
                    v.Iznos = k.Cena;
                    v.Odrediste.X = k.XCoord;
                    v.Odrediste.Y = k.YCoord;
                    v.Odrediste.Adresa.FormatAdrese = k.Street;

                    v.VozacPrihvatio.Lok.X = k.XCoord;
                    v.VozacPrihvatio.Lok.Y = k.YCoord;
                    v.VozacPrihvatio.Lok.Adresa.FormatAdrese = k.Street;

                    v.VozacPrihvatio.Zauzet = false;
                    v.StatusVoznje = StatusVoznje.Uspjesna;
                    ret = true;
                    break;
                }
            }
            if (ret)
            {
                foreach (Vozac vo in lv)
                {
                    if (vo.KorisnickoIme == k.Voz.VozacPrihvatio.KorisnickoIme)
                    {
                        vo.Lok.X = k.XCoord;
                        vo.Lok.Y = k.YCoord;
                        vo.Lok.Adresa.FormatAdrese = k.Street;
                        vo.Zauzet = false;
                        break;
                    }
                }
            }
            xml.WriteDrives(lista, ss);
            xml.WriteDrivers(lv, ss1);
            return ret;
        }

        [HttpPost]
        [ActionName("Izmeni")]
        public bool Izmeni([FromBody]VoznjaPomocnaZaIzmenu k)
        {

            string ss1 = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Voznje.xml");


            List<Voznja> drives = xml.ReadDrives(ss1);

            foreach (Voznja u in drives)
            {
                if (DateTime.Parse(u.DatumPorudzbine) == DateTime.Parse(k.Datum) && u.Musterija.KorisnickoIme == k.korisnicko)
                {
                    u.LokacijaDolaskaTaksija.X = k.XCoord;
                    u.LokacijaDolaskaTaksija.Y = k.YCoord;

                    u.LokacijaDolaskaTaksija.Adresa.FormatAdrese = k.Street;

                    if(k.tipAuta == null)
                    {
                        break;
                    }
                    
                    if (k.tipAuta != "")
                    {
                        u.ZeljeniAutomobil = (TipAutomobila)int.Parse(k.tipAuta);
                    }
                    break;
                }
            }

            xml.WriteDrives(drives, ss1);
            return true;
        }
        [HttpPost]
        [ActionName("IzmeniV")]
        public bool IzmeniV([FromBody]VoznjaPomocnaZaIzmenu k)
        {
            string ss = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Vozaci.xml");
            string ss1 = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Voznje.xml");
            List<Voznja> drives = xml.ReadDrives(ss1);
            List<Vozac> vozaci = xml.ReadDrivers(ss);
            foreach (Voznja u in drives)
            {
                if (u.VozacPrihvatio.KorisnickoIme == k.korisnicko)
                {
                    u.VozacPrihvatio.Lok.X = k.XCoord;
                    u.VozacPrihvatio.Lok.Y = k.YCoord;
                    u.VozacPrihvatio.Lok.Adresa.FormatAdrese = k.Street;
                }
            }
            foreach (Vozac m in vozaci)
            {
                if (m.KorisnickoIme == k.korisnicko)
                {
                    m.Lok.X = k.XCoord;
                    m.Lok.Y = k.YCoord;
                    m.Lok.Adresa.FormatAdrese = k.Street;
                }
            }
            xml.WriteDrives(drives, ss1);
            xml.WriteDrivers(vozaci, ss);
            return true;
        }

        [HttpGet]
        [ActionName("getBlockedUsers")]
        public List<Korisnik> getBlockedUsers()
        {
            string ss = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Musterije.xml");
            string ss1 = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Vozaci.xml");

            List<Musterija> lm = xml.ReadUsers(ss);
            List<Vozac> lv = xml.ReadDrivers(ss1);

            List<Korisnik> ret = new List<Korisnik>();

            foreach (Musterija m in lm)
            {
                if (m.Blokiran)
                {
                    ret.Add(m);
                }
            }
            foreach (Vozac v in lv)
            {
                if (v.Blokiran)
                {
                    ret.Add(v);
                }
            }
            return ret;
        }

        [HttpGet]
        [ActionName("getUnblockedUsers")]
        public List<Korisnik> getUnblockedUsers()
        {
            string ss = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Musterije.xml");
            string ss1 = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Vozaci.xml");

            List<Musterija> lm = xml.ReadUsers(ss);
            List<Vozac> lv = xml.ReadDrivers(ss1);

            List<Korisnik> ret = new List<Korisnik>();

            foreach (Musterija m in lm)
            {
                if (!m.Blokiran)
                {
                    ret.Add(m);
                }
            }
            foreach (Vozac v in lv)
            {
                if (!v.Blokiran)
                {
                    ret.Add(v);
                }
            }
            return ret;
        }


        [HttpGet]
        [ActionName("Blokiraj")]
        public List<Korisnik> Blokiraj(string username)
        {
            string ss = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Musterije.xml");
            string ss1 = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Vozaci.xml");

            List<Musterija> lm = xml.ReadUsers(ss);
            List<Vozac> lv = xml.ReadDrivers(ss1);

            List<Korisnik> ret = new List<Korisnik>();

            foreach (Musterija m in lm)
            {
                if (m.KorisnickoIme == username)
                {
                    m.Blokiran = true;
                }
                if (!m.Blokiran)
                {
                    ret.Add(m);
                }
            }
            foreach (Vozac v in lv)
            {
                if (v.KorisnickoIme == username)
                {
                    v.Blokiran = true;
                }
                if (!v.Blokiran)
                {
                    ret.Add(v);
                }
            }

            xml.WriteUsers(lm, ss);
            xml.WriteDrivers(lv, ss1);
            return ret;
        }

        [HttpGet]
        [ActionName("Odblokiraj")]
        public List<Korisnik> Odblokiraj(string username)
        {
            string ss = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Musterije.xml");
            string ss1 = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Vozaci.xml");

            List<Musterija> lm = xml.ReadUsers(ss);
            List<Vozac> lv = xml.ReadDrivers(ss1);

            List<Korisnik> ret = new List<Korisnik>();

            foreach (Musterija m in lm)
            {
                if (m.KorisnickoIme == username)
                {
                    m.Blokiran = false;
                }
                if (m.Blokiran)
                {
                    ret.Add(m);
                }
            }
            foreach (Vozac v in lv)
            {
                if (v.KorisnickoIme == username)
                {
                    v.Blokiran = false;
                }
                if (v.Blokiran)
                {
                    ret.Add(v);
                }
            }
            xml.WriteUsers(lm, ss);
            xml.WriteDrivers(lv, ss1);

            return ret;
            
        }
    }
}
