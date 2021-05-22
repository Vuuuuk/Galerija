using GalerijaG3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GalerijaG3.Controllers
{
    public class SlikeController : Controller
    {
        //POMOCNE METODE
        public bool ValidacijaGalerije(string nazivGalerije, string lokacijaGalerije)
        {
            Dictionary<string, Galerija> galerije = (Dictionary<string, Galerija>)HttpContext.Application["galerije"];
            Dictionary<string, Slika> slike = (Dictionary<string, Slika>)HttpContext.Application["slike"];
            List<string> errors = new List<string>();
            bool povratna = false;

            if (nazivGalerije.Equals(string.Empty) || lokacijaGalerije.Equals(string.Empty))
            {
                errors.Add("Molimo unesite sva potrebna polja prilikom dodavanja galerije!\n");
                povratna = true;
            }

            if (galerije.ContainsKey(nazivGalerije))
            {
                galerije[nazivGalerije].obrisana = false;
                foreach (Slika s in slike.Values)
                    if (s.galerija.Equals(nazivGalerije))
                        s.obrisana = false;
                errors.Add("Galerija sa unetim nazivom već postoji u sistemu, radi lakšeg testiranja ukoliko je bila obrisana više nije!\n");
                povratna = true;
            }

            ViewBag.ErrorMessages = errors;
            HttpContext.Application["galerije"] = galerije;
            HttpContext.Application["slike"] = slike;
            return povratna;
        }

        public bool ValidacijaSlike(Slika s)
        {
            Dictionary<string, Slika> slike = (Dictionary<string, Slika>)HttpContext.Application["slike"];
            List<string> errors = new List<string>();
            bool povratna = false;

            if (s.cena > 100000000 || s.cena > uint.MaxValue)
            {
                errors.Add("Cena je van dozvoljenog opsega, dozvoljen opseg je [0 - 100 000 000] EUR!\n");
                povratna = true;
            }

            if (slike.ContainsKey(s.naziv))
            {
                errors.Add("Slika sa unetim nazivom već postoji u sistemu!\n");
                povratna = true;
            }

            ViewBag.ErrorMessages = errors;
            return povratna;
        }

        public bool ValidacijaSlikeIzmena(Slika s)
        {
            List<string> errors = new List<string>();
            bool povratna = false;

            if (s.cena > 100000000 || s.cena > uint.MaxValue)
            {
                errors.Add("Cena je van dozvoljenog opsega, dozvoljen opseg je [0 - 100 000 000] EUR!\n");
                povratna = true;
            }

            ViewBag.ErrorMessages = errors;
            return povratna;
        }

        // GET: Slike
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Sortiranje(string polje, string vrsta)
        {
            Dictionary<string, Slika> slike = (Dictionary<string, Slika>)HttpContext.Application["slike"];
            Dictionary<string, Slika> sortiraneSlike = new Dictionary<string, Slika>();

            switch(polje)
            {
                case "Naziv slike":
                    {
                        if (vrsta.Equals("Opadajuće"))
                        {
                            foreach (KeyValuePair<string, Slika> slika in slike.OrderByDescending(vrednost => vrednost.Value.naziv))
                                sortiraneSlike.Add(slika.Value.naziv, slika.Value);
                        }
                        else
                        {
                            foreach (KeyValuePair<string, Slika> slika in slike.OrderBy(vrednost => vrednost.Value.naziv))
                                sortiraneSlike.Add(slika.Value.naziv, slika.Value);
                        }
                        break;
                    }
                case "Tehnika":
                    {
                        if (vrsta.Equals("Opadajuće"))
                        {
                            foreach (KeyValuePair<string, Slika> slika in slike.OrderByDescending(vrednost => vrednost.Value.tehnika))
                                sortiraneSlike.Add(slika.Value.naziv, slika.Value);
                        }
                        else
                        {
                            foreach (KeyValuePair<string, Slika> slika in slike.OrderBy(vrednost => vrednost.Value.tehnika))
                                sortiraneSlike.Add(slika.Value.naziv, slika.Value);
                        }
                        break;
                    }
                case "Cena":
                    {
                        if (vrsta.Equals("Opadajuće"))
                        {
                            foreach (KeyValuePair<string, Slika> slika in slike.OrderByDescending(vrednost => vrednost.Value.cena))
                                sortiraneSlike.Add(slika.Value.naziv, slika.Value);
                        }
                        else
                        {
                            foreach (KeyValuePair<string, Slika> slika in slike.OrderBy(vrednost => vrednost.Value.cena))
                                sortiraneSlike.Add(slika.Value.naziv, slika.Value);
                        }
                        break;
                    }
            }

            HttpContext.Application["slike"] = sortiraneSlike;
            return View("~/Views/Slike/Index.cshtml");
        }

        [HttpPost]
        public ActionResult PretragaNaziva(string poljePretrage, string nazivPretrage)
        {
            Dictionary<string, Slika> slike = (Dictionary<string, Slika>)HttpContext.Application["slike"];
            Dictionary<string, Slika> pretrazeneSlike = new Dictionary<string, Slika>();
            if(!nazivPretrage.Equals(string.Empty) && !poljePretrage.Equals(string.Empty))
            {
                switch (poljePretrage)
                {
                    case "Naziv slike":
                        {
                            foreach (Slika s in slike.Values)
                                if (s.naziv.Equals(nazivPretrage))
                                    pretrazeneSlike.Add(s.naziv, s);
                            break;
                        }
                    case "Tehnika":
                        {
                            foreach (Slika s in slike.Values)
                                if (s.tehnika.Equals(nazivPretrage))
                                    pretrazeneSlike.Add(s.naziv, s);
                            break;
                        }
                }
                HttpContext.Application["slike"] = pretrazeneSlike;
                return View("~/Views/Slike/Index.cshtml");
            }
            else
            {
                HttpContext.Application["slike"] = Podaci.ProcitajSlike("~/App_Data/Slike.txt");
                return View("~/Views/Slike/Index.cshtml");
            }
        }

        [HttpPost]
        public ActionResult PretragaCene(string cenaOd, string cenaDo)
        {
            Dictionary<string, Slika> slike = (Dictionary<string, Slika>)HttpContext.Application["slike"];
            Dictionary<string, Slika> pretrazeneSlike = new Dictionary<string, Slika>();
            if (!cenaOd.Equals(string.Empty) && !cenaDo.Equals(string.Empty))
            {
                foreach (Slika s in slike.Values)
                    if (Convert.ToUInt32(cenaOd) <= Convert.ToInt32(s.cena) && (uint)s.cena <= Convert.ToUInt32(cenaDo))
                        pretrazeneSlike.Add(s.naziv, s);
                HttpContext.Application["slike"] = pretrazeneSlike;
                return View("~/Views/Slike/Index.cshtml");
            }
            else
            {
                HttpContext.Application["slike"] = Podaci.ProcitajSlike("~/App_Data/Slike.txt");
                return View("~/Views/Slike/Index.cshtml");
            }
        }

        [HttpPost]
        public ActionResult DeaktivirajKorisnika(string korisnickoIme)
        {
            Dictionary<string, Korisnik> korisnici = (Dictionary<string, Korisnik>)HttpContext.Application["korisnici"];
            foreach (Korisnik k in korisnici.Values)
                if (k.korisnickoIme.Equals(korisnickoIme))
                    korisnici[korisnickoIme].obrisan = true;
            HttpContext.Application["korisnici"] = korisnici;
            return View("~/Views/Slike/Index.cshtml");
        }

        [HttpPost]
        public ActionResult AktivirajKorisnika(string korisnickoIme)
        {
            Dictionary<string, Korisnik> korisnici = (Dictionary<string, Korisnik>)HttpContext.Application["korisnici"];
            foreach (Korisnik k in korisnici.Values)
                if (k.korisnickoIme.Equals(korisnickoIme))
                    korisnici[korisnickoIme].obrisan = false;
            HttpContext.Application["korisnici"] = korisnici;
            return View("~/Views/Slike/Index.cshtml");
        }

        [HttpPost]
        public ActionResult DodajGaleriju(string nazivGalerije, string lokacijaGalerije)
        {
            Dictionary<string, Galerija> galerije = (Dictionary<string, Galerija>)HttpContext.Application["galerije"];
            Dictionary<string, Slika> slike = (Dictionary<string, Slika>)HttpContext.Application["slike"];
            if (ValidacijaGalerije(nazivGalerije, lokacijaGalerije))
                return View("~/Views/Slike/Index.cshtml");
            else
            {
                Galerija g = new Galerija(nazivGalerije, lokacijaGalerije, false);
                galerije.Add(g.naziv, g);
                foreach (Slika s in slike.Values)
                    if (s.galerija.Equals(nazivGalerije))
                        s.obrisana = false;
                HttpContext.Application["slike"] = slike;
                HttpContext.Application["galerije"] = galerije;
                Podaci.UcitajGaleriju(g);
                return View("~/Views/Slike/Index.cshtml");
            }
        }

        [HttpPost]
        public ActionResult ObrisiGaleriju(string nazivGalerije)
        {
            Dictionary<string, Galerija> galerije = (Dictionary<string, Galerija>)HttpContext.Application["galerije"];
            Dictionary<string, Slika> slike = (Dictionary<string, Slika>)HttpContext.Application["slike"];
            foreach (Galerija g in galerije.Values)
                if (g.naziv.Equals(nazivGalerije))
                {
                    galerije[nazivGalerije].obrisana = true;
                    galerije[nazivGalerije].slike.Clear();
                    foreach (Slika s in slike.Values)
                        if (s.galerija.Equals(nazivGalerije))
                            s.obrisana = true;

                }
            HttpContext.Application["slike"] = slike;
            HttpContext.Application["galerije"] = galerije;
            return View("~/Views/Slike/Index.cshtml");
        }

        [HttpPost]
        public ActionResult PrebacivanjeIzmenaSlika()
        {

            return View("~/Views/Slike/IzmeniSliku.cshtml");
        }

        [HttpPost]
        public ActionResult PrebacivanjeSlika()
        {
            return View("~/Views/Slike/DodajSliku.cshtml");
        }

        [HttpPost]
        public ActionResult DodajSliku(Slika s, string galerija)
        {
            Dictionary<string, Galerija> galerije = (Dictionary<string, Galerija>)HttpContext.Application["galerije"];
            Dictionary<string, Slika> slike = (Dictionary<string, Slika>)HttpContext.Application["slike"];
            if (ValidacijaSlike(s))
                return View("~/Views/Slike/DodajSliku.cshtml");
            else
            {
                slike.Add(s.naziv, s);
                galerije[galerija].slike.Add(s.naziv, s);
                HttpContext.Application["slike"] = slike;
                HttpContext.Application["galerije"] = galerije;
                Podaci.UcitajSliku(s);
                return View("~/Views/Slike/Index.cshtml");
            }
        }

        [HttpPost]
        public ActionResult IzmeniSliku(Slika s)
        {
            Dictionary<string, Galerija> galerije = (Dictionary<string, Galerija>)HttpContext.Application["galerije"];
            Dictionary<string, Slika> slike = (Dictionary<string, Slika>)HttpContext.Application["slike"];
            if (ValidacijaSlikeIzmena(s))
                return View("~/Views/Slike/IzmeniSliku.cshtml");
            else
            {
                s.naziv =  s.naziv.Replace("_", " ");
                slike[s.naziv] = s;
                galerije[s.galerija].slike[s.naziv] = s;
                HttpContext.Application["slike"] = slike;
                HttpContext.Application["galerije"] = galerije;
                return View("~/Views/Slike/Index.cshtml");
            }
        }
    }
}