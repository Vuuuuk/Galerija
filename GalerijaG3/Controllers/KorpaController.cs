using GalerijaG3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GalerijaG3.Controllers
{
    public class KorpaController : Controller
    {
        // GET: Korpa
        public ActionResult Index()
        {
            Dictionary<string, Slika> korpa = (Dictionary<string, Slika>)Session["korpa"];
            uint ukupnaCena = 0;
            if (korpa != null)
            {
                foreach (Slika s in korpa.Values)
                    ukupnaCena += s.cena;
            }
            ViewBag.Cena = ukupnaCena;
            return View();
        }

        [HttpPost]
        public ActionResult Dodaj(string nazivSlike)
        {
            Dictionary<string, Slika> korpa = (Dictionary<string, Slika>)Session["korpa"];
            Dictionary<string, Slika> slike = (Dictionary<string, Slika>)HttpContext.Application["slike"];

            foreach (Slika s in slike.Values)
                if (s.naziv.Equals(nazivSlike))
                    if(!korpa.ContainsKey(nazivSlike))
                        korpa.Add(s.naziv, s);

            HttpContext.Application["slike"] = slike;
            return RedirectToAction("Index", "Slike");
        }

        public ActionResult IzvrsiKupovinu()
        {
            Dictionary<string, Slika> korpa = (Dictionary<string, Slika>)Session["korpa"];
            Dictionary<string, Slika> slike = (Dictionary<string, Slika>)HttpContext.Application["slike"];
            if (korpa == null)
            {
                ViewBag.ErrorMessages = "Korpa je prazna, nemoguće je izvršiti kupovinu, odaberite sliku i pokušajte ponovo!";
                return View("~/Views/Korpa/Index.cshtml");
            }

            foreach (Slika slikaKorpa in korpa.Values)
                foreach (Slika s in slike.Values)
                    if (s.naziv.Equals(slikaKorpa.naziv))
                        s.naProdaju = false;

            Session["korpa"] = new Dictionary<string, Slika>();

            return RedirectToAction("Index", "Slike");
        }
    }
}