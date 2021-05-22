using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GalerijaG3.Models
{
    public enum Pol
    {
        MUSKI,
        ZENSKI,
        DRUGO
    }

    public enum Uloga
    {
        ADMINISTRATOR,
        KUPAC
    }

    public class Korisnik
    {
        public string korisnickoIme { get; set; }
        public string lozinka { get; set; }
        public string ime { get; set; }
        public string prezime { get; set; }
        public Pol pol { get; set; }
        public string email { get; set; }
        public DateTime datumRodjenja { get; set; }
        public Uloga uloga { get; set; }
        public bool obrisan { get; set; }

        public Korisnik() { }

        public Korisnik(string korisnickoIme, string lozinka, string ime, string prezime, Pol pol, string email, DateTime datumRodjenja, Uloga uloga, bool orbisan)
        {
            this.korisnickoIme = korisnickoIme;
            this.lozinka = lozinka;
            this.ime = ime;
            this.prezime = prezime;
            this.pol = pol;
            this.email = email;
            this.datumRodjenja = datumRodjenja;
            this.uloga = uloga;
            this.obrisan = obrisan;
        }
    }
}