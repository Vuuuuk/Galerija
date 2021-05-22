using GalerijaG3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GalerijaG3.Controllers
{
    public class LoginController : Controller
    {
        //POMOCNE METODE
        public bool ProveraKorisnika(string korisnickoIme, string lozinka)
        {
            bool izlaz = false;
            Dictionary<string, Korisnik> korisnici = (Dictionary<string, Korisnik>)HttpContext.Application["korisnici"];
            if (korisnici.ContainsKey(korisnickoIme))
                if (korisnici[korisnickoIme].lozinka.Equals(lozinka))
                    if(korisnici[korisnickoIme].obrisan != true)
                        izlaz = true;
            return izlaz;
        }

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Prijava(string korisnickoIme, string lozinka)
        {
            Dictionary<string, Korisnik> korisnici = (Dictionary<string, Korisnik>)HttpContext.Application["korisnici"];
           if(ProveraKorisnika(korisnickoIme, lozinka))
           {
                Session["korisnik"] = korisnici[korisnickoIme];

                //KORPA
                Dictionary<string, Slika> korpa = (Dictionary<string, Slika>)Session["korpa"];
                if(korpa == null)
                {
                    korpa = new Dictionary<string, Slika>();
                    Session["korpa"] = korpa;
                }

                return RedirectToAction("Index", "Slike");
           }
           else
           {
                ViewBag.ErrorMessages = "Pogrešno korisničko ime ili lozinka, molimo pokušajte ponovo ili se obratite administratoru!";
                return View("~/Views/Login/Index.cshtml");
           }
        }

        public ActionResult Odjava()
        {
            Session["korisnik"] = null;
            Session["korpa"] = null;
            return View("~/Views/Slike/Index.cshtml");
        }
    }
}