using GalerijaG3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GalerijaG3.Controllers
{
    public class GalerijaController : Controller
    {
        // GET: Galerija
        public ActionResult Index(string nazivGalerije)
        {
            Dictionary<string, Slika> slike = (Dictionary<string, Slika>)HttpContext.Application["slike"];
            Dictionary<string, Slika> slikeGalerije = (Dictionary<string, Slika>)HttpContext.Application["slikeGalerije"];
            slikeGalerije.Clear();
            foreach (Slika s in slike.Values)
                if (s.galerija.Equals(nazivGalerije))
                    slikeGalerije.Add(s.naziv, s);

            HttpContext.Application["slikeGalerije"] = slikeGalerije;
            return View();
        }
    }
}