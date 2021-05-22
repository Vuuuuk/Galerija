using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GalerijaG3.Models
{
    public class Galerija
    {
        public string naziv { get; set; }
        public string adresa { get; set; }
        public bool obrisana { get; set; }
        public Dictionary<string, Slika> slike { get; set; }

        public Galerija() { }

        public Galerija(string naziv, string adresa, bool obrisana)
        {
            this.naziv = naziv;
            this.adresa = adresa;
            this.obrisana = obrisana;
            slike = new Dictionary<string, Slika>();
        }
    }
}