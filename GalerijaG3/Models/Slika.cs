using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GalerijaG3.Models
{
    public class Slika
    {
        public string naziv { get; set; }
        public string autor { get; set; }
        public uint godinaIzrade { get; set; }
        public string tehnika { get; set; }
        public string opis { get; set; }
        public uint cena { get; set; }
        public bool naProdaju { get; set; }

        public bool obrisana { get; set; }

        public string galerija { get; set; }

        public Slika() { }

        public Slika(string naziv, string autor, uint godinaIzrade, string tehnika, string opis, uint cena, bool naProdaju, bool obrisana, string galerija)
        {
            this.naziv = naziv;
            this.autor = autor;
            this.godinaIzrade = godinaIzrade;
            this.tehnika = tehnika;
            this.opis = opis;
            this.cena = cena;
            this.naProdaju = naProdaju;
            this.obrisana = obrisana;
            this.galerija = galerija;
        }
    }
}