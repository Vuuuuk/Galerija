using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using GalerijaG3.Models;

namespace GalerijaG3.Controllers
{
    public class RegisterController : Controller
    {
        //POMOCNE METODE
        public bool ValidacijaRegistracije(Korisnik korisnik)
        {
            Dictionary<string, Korisnik> korisnici = (Dictionary<string, Korisnik>)HttpContext.Application["korisnici"];
            Regex korektnaSifra = new Regex("^[a-zA-Z0-9 ]*$");
            List<string> errors = new List<string>();
            bool povratna = false;

            if (korisnik.korisnickoIme.Length < 3 || korisnici.ContainsKey(korisnik.korisnickoIme))
            {
                errors.Add("Korisničko ime je kraće od 3 karaktera ili korisnik [ " + korisnik.korisnickoIme + " ] već postoji u sistemu!");
                povratna = true;
            }

            if (korisnik.lozinka.Length < 8 || !korektnaSifra.IsMatch(korisnik.lozinka))
            {
                errors.Add("Korisnička lozinka je kraća od 8 karaktera ili sadrži neki specijalan karakter!");
                povratna = true;
            }

            ViewBag.ErrorMessages = errors;
            return povratna;
        }

        // GET: Register
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registracija(Korisnik korisnik)
        {
            Dictionary<string, Korisnik> korisnici = (Dictionary<string, Korisnik>)HttpContext.Application["korisnici"];

            if(ValidacijaRegistracije(korisnik))
                return View("~/Views/Register/Index.cshtml");

            else
            {
                korisnik.uloga = Uloga.KUPAC;
                korisnici.Add(korisnik.korisnickoIme, korisnik);
                Podaci.UcitajKorisnika(korisnik);
                return RedirectToAction("Index", "Login");
            }
        }
    }
}