using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace GalerijaG3.Models
{
    public class Podaci
    {
        public static Dictionary<string, Korisnik> ProcitajKorisnike(string putanja)
        {
            Dictionary<string, Korisnik> korisnici = new Dictionary<string, Korisnik>();
            putanja = HostingEnvironment.MapPath(putanja);
            FileStream fs = new FileStream(putanja, FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            string line = "";
            while((line = sr.ReadLine()) != null)
            {
                string[] podaci = line.Split(';');
                Enum.TryParse(podaci[4], true, out Pol pol);
                Enum.TryParse(podaci[7], true, out Uloga uloga);
                DateTime datumRodjenja = Convert.ToDateTime(podaci[6]);
                bool.TryParse(podaci[8], out bool obrisan);
                Korisnik k = new Korisnik(podaci[0], podaci[1], podaci[2], podaci[3], pol, podaci[5], datumRodjenja, uloga, obrisan);
                korisnici.Add(podaci[0], k);
            }
            sr.Close();
            fs.Close();

            return korisnici;
        }

        public static Dictionary<string, Slika> ProcitajSlike(string putanja)
        {
            Dictionary<string, Slika> slike = new Dictionary<string, Slika>();
            putanja = HostingEnvironment.MapPath(putanja);
            FileStream fs = new FileStream(putanja, FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            string line = "";
            while((line = sr.ReadLine()) != null)
            {
                string[] podaci = line.Split(';');
                uint.TryParse(podaci[2], out uint godinaIzrade);
                uint.TryParse(podaci[5], out uint cena);
                bool.TryParse(podaci[6], out bool naProdaju);
                bool.TryParse(podaci[7], out bool obrisana);
                Slika s = new Slika(podaci[0], podaci[1], godinaIzrade, podaci[3], podaci[4], cena, naProdaju, obrisana, podaci[8]);
                slike.Add(podaci[0], s);
            }
            sr.Close();
            fs.Close();

            return slike;
        }

        public static Dictionary<string, Galerija> ProcitajGalerije(string putanja)
        {
            Dictionary<string, Galerija> galerije = new Dictionary<string, Galerija>();
            putanja = HostingEnvironment.MapPath(putanja);
            FileStream fs = new FileStream(putanja, FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            string line = "";
            while((line = sr.ReadLine()) != null)
            {
                string[] podaci = line.Split(';');
                bool.TryParse(podaci[2], out bool obrisana);
                Galerija g = new Galerija(podaci[0], podaci[1], obrisana);
                galerije.Add(podaci[0], g);
            }
            sr.Close();
            fs.Close();

            return galerije;
        }

        public static void UcitajKorisnika(Korisnik korisnik)
        {
            string path = HostingEnvironment.MapPath("~/App_Data/Korisnici.txt");
            using (StreamWriter file = File.AppendText(path))
                file.WriteLine("{0};{1};{2};{3};{4};{5};{6};{7};{8}", 
                                                                  korisnik.korisnickoIme, korisnik.lozinka, korisnik.ime, korisnik.prezime, korisnik.pol,
                                                                  korisnik.email, korisnik.datumRodjenja.ToString("dd/MM/yyyy"), 
                                                                  korisnik.uloga, korisnik.obrisan);
        }

        public static void UcitajGaleriju(Galerija galerija)
        {
            string path = HostingEnvironment.MapPath("~/App_Data/Galerije.txt");
            using (StreamWriter file = File.AppendText(path))
                file.WriteLine("{0};{1};{2}",galerija.naziv, galerija.adresa, galerija.obrisana);    
        }

        public static void UcitajSliku(Slika slika)
        {
            string path = HostingEnvironment.MapPath("~/App_Data/Slike.txt");
            using (StreamWriter file = File.AppendText(path))
                file.WriteLine("{0};{1};{2};{3};{4};{5};{6};{7};{8}",slika.naziv, slika.autor, slika.godinaIzrade,
                                                                     slika.tehnika, slika.opis, slika.cena,
                                                                     slika.naProdaju, slika.obrisana, slika.galerija);
        }
    }
}