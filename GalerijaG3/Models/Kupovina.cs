using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GalerijaG3.Models
{
    public class Kupovina
    {
        public Korisnik kupac { get; set; }
        public List<Slika> izabraneSlike { get; set; }
        public DateTime datumKupovine { get; set; }
        public double ukupnoNaplaceno { get; set; }

        public Kupovina() { }
    }
}