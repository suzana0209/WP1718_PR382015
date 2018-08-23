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
                if((!v.Zauzet) && (v.Auto.TipAutomobila == (TipAutomobila)int.Parse(k.tipAuta)))
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
        public int Edit([FromBody]EditModel k)
        {
            string ss = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Musterije.xml");
            //string ss1 = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Vozaci.xml");
            //string ss2 = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Dispeceri.xml");
            string adm = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Dispeceri.xml");
            string drv = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Vozaci.xml");
            string voznje = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Voznje.xml");
            string auta = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Automobili.xml");

            List<Musterija> users = xml.ReadUsers(ss);
            //List<Vozac> vozaci = xml.ReadDrivers(ss1);
            //List<Dispecer> disp = xml.ReadDispecer(ss2);
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
                        u.Lozinka = k.Password;
                        u.Ime = k.Ime;
                        u.Prezime = k.Prezime;
                        if (k.Pol == "1")
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
                        // users.ad(user);
                        q = 0;
                        mm = u;
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
                        if (k.Pol == "1")
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
                        q = 2;
                        vv = u;
                        break;
                    }
                }

                if (q == 0)
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
                    // return 2;
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
                if (q == 2)
                {
                    xml.WriteDrivers(drivers, drv);
                    //return 3;
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
                    v.Komentar.KorisnikOstavioKom = k.Voz.Komentar.KorisnikOstavioKom;
                    ret = true;
                    break;
                }
            }
            xml.WriteDrives(lista, ss);
            return ret;
        }

        [HttpPost]
        [ActionName("ObradiVoznju")]
        public List<Voznja> ObradiVoznju([FromBody]ModelZaObradiVoznju k)
        {
            string ss = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Voznje.xml");
            string ss1 = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Vozaci.xml");
            string ss2 = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Dispeceri.xml");
            List<Voznja> lista = xml.ReadDrives(ss);
            List<Vozac> lv = xml.ReadDrivers(ss1);
            List<Dispecer> dispi = xml.ReadDispecer(ss2);
            Vozac vozacsl = new Vozac();
            Voznja voznja = new Voznja();
            bool nasao = false;
            foreach (Vozac vv in lv)
            {
                if (!vv.Zauzet && vv.Auto.TipAutomobila == k.Voznj.ZeljeniAutomobil)
                {
                    nasao = true;
                    vv.Zauzet = true;
                    vozacsl = vv;
                    break;
                }
            }
            List<Voznja> retL = k.ListaVoznji;
            Dispecer dispe = new Dispecer();
            foreach (Dispecer d in dispi)
            {
                if (d.KorisnickoIme == k.Username)
                {
                    dispe = d;
                }
            }
            if (nasao)
            {
                foreach (Voznja v in lista)
                {
                    if ((v.Musterija.KorisnickoIme == k.Voznj.Musterija.KorisnickoIme || v.DispecerFormirao.KorisnickoIme == k.Voznj.DispecerFormirao.KorisnickoIme) && 
                        DateTime.Parse(v.DatumPorudzbine) == DateTime.Parse(k.Voznj.DatumPorudzbine))
                    {
                        v.StatusVoznje = StatusVoznje.Obradjena;
                        v.VozacPrihvatio = vozacsl;
                        v.DispecerFormirao = dispe;
                        break;
                    }
                }
                foreach (Voznja vvv in retL)
                {
                    if ((vvv.Musterija.KorisnickoIme == k.Voznj.Musterija.KorisnickoIme || vvv.DispecerFormirao.KorisnickoIme == k.Voznj.DispecerFormirao.KorisnickoIme) && 
                        DateTime.Parse(vvv.DatumPorudzbine) == DateTime.Parse(k.Voznj.DatumPorudzbine))
                    {
                        vvv.StatusVoznje = StatusVoznje.Obradjena;
                        vvv.DispecerFormirao = dispe;
                        vvv.VozacPrihvatio = vozacsl;
                    }
                }
                xml.WriteDrives(lista, ss);
                xml.WriteDrivers(lv, ss1);
            }
            return retL;
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
                    nasao = true;
                    break;
                }
            }
            if (!nasao)
            {
                po.Zauzet = true;
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
                    v.Odrediste.Adresa.Ulica = k.Street;
                    v.Odrediste.Adresa.Broj = k.Number;
                    v.Odrediste.Adresa.NaseljenoMesto = k.Town;
                    v.Odrediste.Adresa.PozivniBroj = Int32.Parse(k.PostalCode);
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
                        vo.Zauzet = false;
                        break;
                    }
                }
            }
            xml.WriteDrives(lista, ss);
            xml.WriteDrivers(lv, ss1);
            return ret;
        }
    }
}
