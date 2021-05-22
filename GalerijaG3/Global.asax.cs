using GalerijaG3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace GalerijaG3
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //INIT 
            Dictionary<string, Korisnik> initAdministratori = Podaci.ProcitajKorisnike("~/App_Data/Korisnici.txt");
            HttpContext.Current.Application["korisnici"] = initAdministratori;

            Dictionary<string, Slika> initSlike = Podaci.ProcitajSlike("~/App_Data/Slike.txt");
            HttpContext.Current.Application["slike"] = initSlike;

            Dictionary<string, Galerija> initGalerije = Podaci.ProcitajGalerije("~/App_Data/Galerije.txt");
            HttpContext.Current.Application["galerije"] = initGalerije;

            Dictionary<string, Slika> initSlikeGalerije = new Dictionary<string, Slika>();
            HttpContext.Current.Application["slikeGalerije"] = initSlikeGalerije;

            //UCITAVANJE SLIKA U GALERIJE
            foreach (Galerija g in initGalerije.Values)
                foreach (Slika s in initSlike.Values)
                    if(g.naziv.Equals(s.galerija))
                        g.slike.Add(s.naziv, s);
        }
    }
}
