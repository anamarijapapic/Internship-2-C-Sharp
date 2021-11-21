using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace PopisStanovnistvaConsoleApp
{
    class Program
    {
        static void Main()
        {
            var popisStanovnistva = new Dictionary<string, (string nameAndSurname, DateTime dateOfBirth)>();
            popisStanovnistva.Add("12345678910", ("Ante Antic", new DateTime(2000, 12, 24)));
            popisStanovnistva.Add("11111111111", ("Ante Antic", new DateTime(2000, 12, 24)));
            popisStanovnistva.Add("22222222222", ("Pero Peric", new DateTime(2012, 9, 6)));
            popisStanovnistva.Add("33333333333", ("Iva Ivic", new DateTime(2001, 7, 10)));
            popisStanovnistva.Add("44444444444", ("Mara Maric", new DateTime(1999, 4, 19)));
            popisStanovnistva.Add("55555555555", ("Jure Antic", new DateTime(2001, 8, 1)));

            GlavniIzbornik(popisStanovnistva);
        }

        static void GlavniIzbornik(Dictionary<string, (string nameAndSurname, DateTime dateOfBirth)> popisStanovnistva)
        {
            Console.Clear();
            Console.WriteLine("Odaberite akciju:");
            Console.WriteLine("1 - Ispis stanovnistva");
            Console.WriteLine("2 - Ispis stanovnika po OIB-u");
            Console.WriteLine("3 - Ispis OIB-a po unosu imena i prezimena te datuma rodenja");
            Console.WriteLine("4 - Unos novog stanovnika");
            Console.WriteLine("5 - Brisanje stanovnika po OIB-u");
            Console.WriteLine("6 - Brisanje stanovnika po imenu i prezimenu te datumu rodenja");
            Console.WriteLine("7 - Brisanje svih stanovnika");
            Console.WriteLine("8 - Uredivanje stanovnika");
            Console.WriteLine("9 - Statistika");
            Console.WriteLine("0 - Izlaz iz aplikacije");

            int userChoice;
            do
            {
                Console.WriteLine("\nUnesite svoj odabir:");
                userChoice = int.Parse(Console.ReadLine());
                switch (userChoice)
                {
                    case 1:
                        Console.Clear();
                        IspisStanovnistva(popisStanovnistva);
                        break;
                    case 2:
                        Console.Clear();
                        IspisStanovnikaPoOIBu(popisStanovnistva);
                        break;
                    case 3:
                        Console.Clear();
                        IspisOIBaStanovnika(popisStanovnistva);
                        break;
                    case 4:
                        Console.Clear();
                        UnosNovogStanovnika(popisStanovnistva);
                        break;
                    case 5:
                        Console.Clear();
                        BrisiStanovnikaPoOIBu(popisStanovnistva);
                        break;
                    case 6:
                        Console.Clear();
                        BrisiStanovnikaImePrezimeDatum(popisStanovnistva);
                        break;
                    case 7:
                        Console.Clear();
                        BrisiSveStanovnike(popisStanovnistva);
                        break;
                    case 8:
                        Console.Clear();
                        UrediStanovnika(popisStanovnistva);
                        break;
                    case 9:
                        Console.Clear();
                        Statistika(popisStanovnistva);
                        break;
                    case 0:
                        Console.Clear();
                        Console.WriteLine("Izlaz iz aplikacije. Pozdrav!");
                        break;
                    default:
                        Console.WriteLine("Neispravan unos!");
                        break;
                }
            } while (userChoice < 0 || userChoice > 9);
        }

        static void GlavniIzbornikPovratak(Dictionary<string, (string nameAndSurname, DateTime dateOfBirth)> popisStanovnistva)
        {
            Console.WriteLine("\n0 - Povratak na glavni izbornik");
            int userChoice;
            do
            {
                Console.WriteLine("\nUnesite svoj odabir:");
                userChoice = int.Parse(Console.ReadLine());

            } while (userChoice != 0);
            GlavniIzbornik(popisStanovnistva);
        }

        static void IspisStanovnistva(Dictionary<string, (string nameAndSurname, DateTime dateOfBirth)> popisStanovnistva)
        {
            Console.WriteLine("Ispis stanovnistva");
            Console.WriteLine("\t1 - Onako kako su spremljeni");
            Console.WriteLine("\t2 - Po datumu rodenja uzlazno");
            Console.WriteLine("\t3 - Po datumu rodenja silazno");
            Console.WriteLine("\t0 - Povratak na glavni izbornik");

            int userChoice;
            do
            {
                Console.WriteLine("\nUnesite svoj odabir:");
                userChoice = int.Parse(Console.ReadLine());

                switch (userChoice)
                {
                    case 1:
                        IspisStanovnistvaSpremljeni(popisStanovnistva);
                        break;
                    case 2:
                        IspisStanovnistvaDatumUzlazno(popisStanovnistva);
                        break;
                    case 3:
                        IspisStanovnistvaDatumSilazno(popisStanovnistva);
                        break;
                    case 0:
                        GlavniIzbornik(popisStanovnistva);
                        break;
                    default:
                        Console.WriteLine("Neispravan unos!");
                        break;
                }
            } while (userChoice < 0 || userChoice > 3);
        }

        static void IspisStanovnistvaSpremljeni(Dictionary<string, (string nameAndSurname, DateTime dateOfBirth)> popisStanovnistva)
        {
            Console.WriteLine("\n1 - Onako kako su spremljeni");
            foreach (var osoba in popisStanovnistva)
            {
                Console.WriteLine($"OIB: {osoba.Key}, Ime i prezime: {osoba.Value.nameAndSurname}, Datum rodenja: {osoba.Value.dateOfBirth.ToString("dd.MM.yyyy.")}");
            }
            Console.WriteLine();
            IspisStanovnistva(popisStanovnistva);
        }

        static void IspisStanovnistvaDatumUzlazno(Dictionary<string, (string nameAndSurname, DateTime dateOfBirth)> popisStanovnistva)
        {
            Console.WriteLine("\n2 - Po datumu rodenja uzlazno");
            var sortiranPopis = from entry in popisStanovnistva orderby entry.Value.dateOfBirth.Year ascending select entry;
            foreach (var osoba in sortiranPopis)
            {
                Console.WriteLine($"OIB: {osoba.Key}, Ime i prezime: {osoba.Value.nameAndSurname}, Datum rodenja: {osoba.Value.dateOfBirth.ToString("dd.MM.yyyy.")}");
            }
            Console.WriteLine();
            IspisStanovnistva(popisStanovnistva);
        }

        static void IspisStanovnistvaDatumSilazno(Dictionary<string, (string nameAndSurname, DateTime dateOfBirth)> popisStanovnistva)
        {
            Console.WriteLine("\n3 - Po datumu rodenja silazno");
            var sortiranPopis = from entry in popisStanovnistva orderby entry.Value.dateOfBirth.Year descending select entry; ;
            foreach (var osoba in sortiranPopis)
            {
                Console.WriteLine($"OIB: {osoba.Key}, Ime i prezime: {osoba.Value.nameAndSurname}, Datum rodenja: {osoba.Value.dateOfBirth.ToString("dd.MM.yyyy.")}");
            }
            Console.WriteLine();
            IspisStanovnistva(popisStanovnistva);
        }

        static void IspisStanovnikaPoOIBu(Dictionary<string, (string nameAndSurname, DateTime dateOfBirth)> popisStanovnistva)
        {
            Console.WriteLine("Ispis stanovnika po OIB-u\n");
            string OIB = UnesiOIB();
            bool pronaden = false;
            foreach (var osoba in popisStanovnistva)
            {
                if (OIB == osoba.Key)
                {
                    pronaden = true;
                    Console.WriteLine($"\nOsoba s trazenim OIB-om ({OIB}):");
                    Console.WriteLine($"Ime i prezime: {osoba.Value.nameAndSurname}, rod.: {osoba.Value.dateOfBirth.ToString("dd.MM.yyyy.")}");
                }
            }
            if (pronaden is false)
                Console.WriteLine("\nOsoba s trazenim OIB-om nije na popisu.");
            GlavniIzbornikPovratak(popisStanovnistva);
        }

        static string UnesiOIB()
        {
            string OIB;
            do
            {
                Console.WriteLine("Unesite OIB stanovnika:");
                OIB = Console.ReadLine().Trim();
                if (!ProvjeriOIB(OIB))
                    Console.WriteLine("Neispravan OIB!");
            } while (!ProvjeriOIB(OIB));
            return OIB;
        }

        static bool ProvjeriOIB(string OIB)
        {
            if (OIB.Length == 11 && OIB.All(char.IsDigit))
                return true;
            return false;
        }

        static void IspisOIBaStanovnika(Dictionary<string, (string nameAndSurname, DateTime dateOfBirth)> popisStanovnistva)
        {
            Console.WriteLine("Ispis OIB-a po unosu imena i prezimena te datuma rodenja\n");
            string imePrezime = UnesiImePrezime();
            DateTime datumRodenja = UnesiDatumRodenja();
            bool pronaden = false;
            foreach (var osoba in popisStanovnistva)
            {
                if (imePrezime == osoba.Value.nameAndSurname && datumRodenja == osoba.Value.dateOfBirth)
                {
                    pronaden = true;
                    Console.WriteLine($"\nOIB osobe ({imePrezime}, rod. {datumRodenja.ToString("dd.MM.yyyy.")}):");
                    Console.WriteLine(osoba.Key);
                }
            }
            if (pronaden is false)
                Console.WriteLine("\nOsoba s tim imenom, prezimenom i datumom rodenja nije na popisu.");
            GlavniIzbornikPovratak(popisStanovnistva);
        }

        static string UnesiImePrezime()
        {
            string ime, prezime;
            do
            {
                Console.WriteLine("Unesite ime stanovnika:");
                ime = Console.ReadLine().Trim();
                if (!ProvjeriImePrezime(ime))
                    Console.WriteLine("Neispravan unos!");
            } while (!ProvjeriImePrezime(ime));
            do
            {
                Console.WriteLine("Unesite prezime stanovnika:");
                prezime = Console.ReadLine().Trim();
                if (!ProvjeriImePrezime(prezime))
                    Console.WriteLine("Neispravan unos!");
            } while (!ProvjeriImePrezime(prezime));
            return ime + " " + prezime;
        }

        static bool ProvjeriImePrezime(string imePrezime)
        {
            if (imePrezime.Length >= 1 && Regex.Match(imePrezime, "^[A-Z][a-zA-Z]*$").Success)
                return true;
            return false;
        }

        static DateTime UnesiDatumRodenja()
        {
            DateTime datumRodenja;
            do
            {
                Console.WriteLine("Unesite datum rodenja (dd.MM.yyyy.):");
                datumRodenja = DateTime.Parse(Console.ReadLine().Trim());
                if (!ProvjeriDatumRodenja(datumRodenja))
                    Console.WriteLine("Neispravan unos!");
            } while (!ProvjeriDatumRodenja(datumRodenja));
            return datumRodenja;
        }

        static bool ProvjeriDatumRodenja(DateTime datumRodenja)
        {
            if (datumRodenja <= DateTime.Now)
                return true;
            return false;
        }

        static void UnosNovogStanovnika(Dictionary<string, (string nameAndSurname, DateTime dateOfBirth)> popisStanovnistva)
        {
            Console.WriteLine("Unos novog stanovnika\n");
            string OIB;
            do
            {
                OIB = UnesiOIB();
                if (popisStanovnistva.ContainsKey(OIB))
                    Console.WriteLine("OIB koji ste unijeli vec postoji na popisu.");
            } while (popisStanovnistva.ContainsKey(OIB));
            string imePrezime = UnesiImePrezime();
            DateTime datumRodenja = UnesiDatumRodenja();

            popisStanovnistva.Add(OIB, (imePrezime, datumRodenja));
            Console.WriteLine("\nNovi stanovnik uspjesno dodan na popis!");
            GlavniIzbornikPovratak(popisStanovnistva);
        }

        static bool PotvrdaBrisanja()
        {
            int userDecision;
            Console.WriteLine("! Potvrdite brisanje");
            Console.WriteLine("\t1 - Potvrda");
            Console.WriteLine("\t2 - Odustani");
            do
            {
                Console.WriteLine("\nUnesite svoj odabir:");
                userDecision = int.Parse(Console.ReadLine());
                if (userDecision < 1 || userDecision > 2)
                    Console.WriteLine("Neispravan unos!");
            } while (userDecision < 1 || userDecision > 2);
            if (userDecision == 1)
                return true;
            return false;
        }

        static void BrisiStanovnikaPoOIBu(Dictionary<string, (string nameAndSurname, DateTime dateOfBirth)> popisStanovnistva)
        {
            Console.WriteLine("Brisanje stanovnika po OIB-u\n");
            string OIB;
            do
            {
                OIB = UnesiOIB();
                if (!popisStanovnistva.ContainsKey(OIB))
                    Console.WriteLine("Osoba s tim OIB nije popisu.");
            } while (!popisStanovnistva.ContainsKey(OIB));
            if (PotvrdaBrisanja())
            {
                popisStanovnistva.Remove(OIB);
                Console.WriteLine("\nStanovnik uspjesno izbrisan s popisa!");
            }
            GlavniIzbornikPovratak(popisStanovnistva);
        }

        static void BrisiStanovnikaImePrezimeDatum(Dictionary<string, (string nameAndSurname, DateTime dateOfBirth)> popisStanovnistva)
        {
            Console.WriteLine("Brisanje stanovnika po imenu i prezimenu te datumu rodenja\n");
            string imePrezime = UnesiImePrezime();
            DateTime datumRodenja = UnesiDatumRodenja();
            string OIB = "";
            int istiPodatci = 0;
            foreach (var osoba in popisStanovnistva)
            {
                if (imePrezime == osoba.Value.nameAndSurname && datumRodenja == osoba.Value.dateOfBirth)
                {
                    istiPodatci++;
                    OIB = osoba.Key;
                }
            }
            if (istiPodatci == 0)
            {
                Console.WriteLine("Ne postoji osoba s tim podatcima.");
                GlavniIzbornikPovratak(popisStanovnistva);
            }
            else if (istiPodatci == 1)
            {
                if (PotvrdaBrisanja())
                {
                    popisStanovnistva.Remove(OIB);
                    Console.WriteLine("\nStanovnik uspjesno izbrisan s popisa!");
                }
                GlavniIzbornikPovratak(popisStanovnistva);
            }
            else if (istiPodatci > 1)
            {
                Console.WriteLine("Postoji vise osoba s odgovarajucim podatcima.");
                foreach (var osoba in popisStanovnistva)
                    if (imePrezime == osoba.Value.nameAndSurname && datumRodenja == osoba.Value.dateOfBirth)
                    {
                        Console.WriteLine($"({imePrezime}, rod. {datumRodenja.ToString("dd.MM.yyyy.")}):");
                        Console.WriteLine($"OIB: {osoba.Key}\n");
                    }
                BrisiStanovnikaPoOIBu(popisStanovnistva);
            }
        }

        static void BrisiSveStanovnike(Dictionary<string, (string nameAndSurname, DateTime dateOfBirth)> popisStanovnistva)
        {
            Console.WriteLine("Brisanje svih stanovnika\n");
            if (PotvrdaBrisanja())
            {
                popisStanovnistva.Clear();
                Console.WriteLine("\nSvi stanovnici su uspjesno izbrisani s popisa!");
            }
            GlavniIzbornikPovratak(popisStanovnistva);
        }

        static bool PotvrdaPromjene()
        {
            int userDecision;
            Console.WriteLine("! Potvrdite promjenu");
            Console.WriteLine("\t1 - Potvrda");
            Console.WriteLine("\t2 - Odustani");
            do
            {
                Console.WriteLine("\nUnesite svoj odabir:");
                userDecision = int.Parse(Console.ReadLine());
                if (userDecision < 1 || userDecision > 2)
                    Console.WriteLine("Neispravan unos!");
            } while (userDecision < 1 || userDecision > 2);
            if (userDecision == 1)
                return true;
            return false;
        }

        static void UrediStanovnika(Dictionary<string, (string nameAndSurname, DateTime dateOfBirth)> popisStanovnistva)
        {
            Console.WriteLine("Uredivanje stanovnika");
            Console.WriteLine("\t1 - Uredi OIB stanovnika");
            Console.WriteLine("\t2 - Uredi ime i prezime stanovnika");
            Console.WriteLine("\t3 - Uredi datum rođenja");
            Console.WriteLine("\t0 - Povratak na glavni izbornik");

            int userChoice;
            do
            {
                Console.WriteLine("\nUnesite svoj odabir:");
                userChoice = int.Parse(Console.ReadLine());

                switch (userChoice)
                {
                    case 1:
                        UrediOIB(popisStanovnistva);
                        break;
                    case 2:
                        UrediImePrezime(popisStanovnistva);
                        break;
                    case 3:
                        UrediDatumRodenja(popisStanovnistva);
                        break;
                    case 0:
                        GlavniIzbornik(popisStanovnistva);
                        break;
                    default:
                        Console.WriteLine("Neispravan unos!");
                        break;
                }
            } while (userChoice < 0 || userChoice > 3);
        }

        static void UrediOIB(Dictionary<string, (string nameAndSurname, DateTime dateOfBirth)> popisStanovnistva)
        {
            string OIB = UnesiOIB();
            if (!popisStanovnistva.ContainsKey(OIB))
            {
                Console.WriteLine($"Ne postoji osoba s OIB-om {OIB}.");
            }
            else
            {
                Console.WriteLine("Unesite novi podatak:");
                string promjenaOIB = UnesiOIB();
                if (PotvrdaPromjene())
                {
                    popisStanovnistva.Add(promjenaOIB, popisStanovnistva[OIB]);
                    popisStanovnistva.Remove(OIB);
                    Console.WriteLine($"OIB {OIB} uspjesno promijenjen u {promjenaOIB}.\n");
                }
            }
            UrediStanovnika(popisStanovnistva);
        }

        static void UrediImePrezime(Dictionary<string, (string nameAndSurname, DateTime dateOfBirth)> popisStanovnistva)
        {
            string OIB = UnesiOIB();
            if (!popisStanovnistva.ContainsKey(OIB))
            {
                Console.WriteLine($"Ne postoji osoba s OIB-om {OIB}.");
            }
            else
            {
                foreach (var osoba in popisStanovnistva)
                {
                    if (OIB == osoba.Key)
                    {
                        Console.WriteLine("Unesite novi podatak:");
                        string promjenaImePrezime = UnesiImePrezime();
                        string imePrezime = osoba.Value.nameAndSurname;
                        if (PotvrdaPromjene())
                        {
                            popisStanovnistva[OIB] = (promjenaImePrezime, osoba.Value.dateOfBirth);
                            Console.WriteLine($"Ime i prezime {imePrezime} uspjesno promijenjeni u {promjenaImePrezime}.\n");
                        }
                    }
                }
            }
            UrediStanovnika(popisStanovnistva);
        }

        static void UrediDatumRodenja(Dictionary<string, (string nameAndSurname, DateTime dateOfBirth)> popisStanovnistva)
        {
            string OIB = UnesiOIB();
            if (!popisStanovnistva.ContainsKey(OIB))
            {
                Console.WriteLine($"Ne postoji osoba s OIB-om {OIB}.");
            }
            else
            {
                foreach (var osoba in popisStanovnistva)
                {
                    if (OIB == osoba.Key)
                    {
                        Console.WriteLine("Unesite novi podatak:");
                        DateTime promjenaDatumRodenja = UnesiDatumRodenja();
                        DateTime datumRodenja = osoba.Value.dateOfBirth;
                        if (PotvrdaPromjene())
                        {
                            popisStanovnistva[OIB] = (osoba.Value.nameAndSurname, promjenaDatumRodenja);
                            Console.WriteLine($"Datum rodenja {datumRodenja} uspjesno promijenjen u {promjenaDatumRodenja}.\n");
                        }
                    }
                }
            }
            UrediStanovnika(popisStanovnistva);
        }

        static void Statistika(Dictionary<string, (string nameAndSurname, DateTime dateOfBirth)> popisStanovnistva)
        {
            Console.WriteLine("Statistika");
            Console.WriteLine("\t1 - Postotak nezaposlenih (od 0 do 23 godine i od 65 do 100 godine) i postotak zaposlenih (od 23 do 65 godine)");
            Console.WriteLine("\t2 - Ispis najcesceg imena i koliko ga stanovnika ima");
            Console.WriteLine("\t3 - Ispis najcesceg prezimena i koliko ga stanovnika ima");
            Console.WriteLine("\t4 - Ispis datum na koji je roden najveci broj ljudi i koji je to datum");
            Console.WriteLine("\t5 - Ispis broja ljudi rodenih u svakom od godisnjih doba");
            Console.WriteLine("\t6 - Ispis najmladeg stanovnika");
            Console.WriteLine("\t7 - Ispis najstarijeg stanovnika");
            Console.WriteLine("\t8 - Prosjecan broj godina (na 2 decimale)");
            Console.WriteLine("\t9 - Medijan godina");
            Console.WriteLine("\t0 - Povratak na glavni izbornik");

            int userChoice;
            do
            {
                Console.WriteLine("\nUnesite svoj odabir:");
                userChoice = int.Parse(Console.ReadLine());

                switch (userChoice)
                {
                    case 1:
                        PostotakRad(popisStanovnistva);
                        break;
                    case 2:
                        NajcesceImePrezime(popisStanovnistva, 0);
                        break;
                    case 3:
                        NajcesceImePrezime(popisStanovnistva, 1);
                        break;
                    case 4:
                        NajcesciDatumRodenja(popisStanovnistva);
                        break;
                    case 5:
                        GodisnjaDoba(popisStanovnistva);
                        break;
                    case 6:
                        NajmladaOsoba(popisStanovnistva);
                        break;
                    case 7:
                        NajstarijaOsoba(popisStanovnistva);
                        break;
                    case 8:
                        ProsjecnaDob(popisStanovnistva);
                        break;
                    case 9:
                        MedijanGodina(popisStanovnistva);
                        break;
                    case 0:
                        GlavniIzbornik(popisStanovnistva);
                        break;
                    default:
                        Console.WriteLine("Neispravan unos!");
                        break;
                }
            } while (userChoice < 0 || userChoice > 9);
        }

        static void PostotakRad(Dictionary<string, (string nameAndSurname, DateTime dateOfBirth)> popisStanovnistva)
        {
            int zaposleni = 0, nezaposleni = 0;
            foreach (var osoba in popisStanovnistva)
            {
                TimeSpan godine = DateTime.Now - osoba.Value.dateOfBirth;
                if (godine.Days >= (23 * 365.25) && godine.Days <= (65 * 365.25)) 
                    zaposleni++;
                else nezaposleni++;
            }
            double postotakNezaposlenih = Math.Round((double)nezaposleni / popisStanovnistva.Count, 4) * 100;
            double postotakZaposlenih = Math.Round((double)zaposleni / popisStanovnistva.Count, 4) * 100;
            Console.WriteLine($"Postotak nezaposlenih (0-23, 65+) je {postotakNezaposlenih}%.\n");
            Console.WriteLine($"Postotak zaposlenih (23-65) je {postotakZaposlenih}%.\n");
            Statistika(popisStanovnistva);
        }

        static void NajcesceImePrezime(Dictionary<string, (string nameAndSurname, DateTime dateOfBirth)> popisStanovnistva, int userChoice)
        {
            var arhivaImePrezime = new Dictionary<string, int>();
            string ime = "";
            string prezime = "";
            int brojac;
            foreach (var osoba in popisStanovnistva)
            {
                if (userChoice == 0)
                {
                    ime = osoba.Value.nameAndSurname.Substring(0, osoba.Value.nameAndSurname.IndexOf(" "));
                    if (arhivaImePrezime.ContainsKey(ime))
                    {
                        if (arhivaImePrezime.TryGetValue(ime, out brojac))
                            arhivaImePrezime[ime] = brojac + 1;
                    }
                    else
                        arhivaImePrezime.Add(ime, 1);
                }
                else if (userChoice == 1)
                {
                    prezime = osoba.Value.nameAndSurname.Substring(osoba.Value.nameAndSurname.IndexOf(" ") + 1);
                    if (arhivaImePrezime.ContainsKey(prezime))
                    {
                        if (arhivaImePrezime.TryGetValue(prezime, out brojac))
                            arhivaImePrezime[prezime] = brojac + 1;
                    }
                    else
                        arhivaImePrezime.Add(prezime, 1);
                }
            }
            var brojPonavljanja = 0;
            var najImePrezime = " ";
            foreach (var imePrezime in arhivaImePrezime)
            {
                if (imePrezime.Value > brojPonavljanja)
                {
                    brojPonavljanja = imePrezime.Value;
                    najImePrezime = imePrezime.Key;
                }
            }
            if (userChoice == 0)
                Console.WriteLine($"Najcesce ime je {najImePrezime} i ima ga {brojPonavljanja} ljudi.\n");
            if (userChoice == 1)
                Console.WriteLine($"Najcesce prezime je {najImePrezime} i ima ga {brojPonavljanja} ljudi.\n");
            Statistika(popisStanovnistva);
        }

        static void NajcesciDatumRodenja(Dictionary<string, (string nameAndSurname, DateTime dateOfBirth)> popisStanovnistva)
        {
            var arhivaDatumRodenja = new Dictionary<DateTime, int>() { };
            DateTime datumRodenja;
            int brojac;
            foreach (var osoba in popisStanovnistva)
            {
                datumRodenja = osoba.Value.dateOfBirth.Date;
                if (arhivaDatumRodenja.ContainsKey(datumRodenja))
                {
                    if (arhivaDatumRodenja.TryGetValue(datumRodenja, out brojac))
                        arhivaDatumRodenja[datumRodenja] = brojac + 1;
                }
                else
                    arhivaDatumRodenja.Add(datumRodenja, 1);
            }
            var brojPonavljanja = 0;
            DateTime najDatumRodenja = new DateTime();
            foreach (var datum in arhivaDatumRodenja)
            {
                if (datum.Value > brojPonavljanja)
                {
                    brojPonavljanja = datum.Value;
                    najDatumRodenja = datum.Key;
                }
            }
            Console.WriteLine($"Najcesci datum rodenja je {najDatumRodenja.ToString("dd.MM.yyyy.")} i ima ga {brojPonavljanja} ljudi.\n");
            Statistika(popisStanovnistva);
        }

        static void GodisnjaDoba(Dictionary<string, (string nameAndSurname, DateTime dateOfBirth)> popisStanovnistva)
        {
            int jesen = 0, zima = 0, proljece = 0, ljeto = 0;

            foreach (var osoba in popisStanovnistva)
            {
                var datumRodenja = (float)osoba.Value.dateOfBirth.Month + osoba.Value.dateOfBirth.Day / 100.00;
                if (datumRodenja >= 9.23 && datumRodenja < 12.21)
                    jesen++;
                else if (datumRodenja >= 3.21 && datumRodenja < 6.21)
                    proljece++;
                else if (datumRodenja >= 6.21 && datumRodenja < 9.23)
                    ljeto++;
                else if (datumRodenja >= 12.21 || datumRodenja < 3.21)
                    zima++;
            }
            List<(string naziv, int broj)> GodisnjaDobaList = new();
            GodisnjaDobaList.Add(("Zima", zima));
            GodisnjaDobaList.Add(("Proljeće", proljece));
            GodisnjaDobaList.Add(("Ljeto", ljeto));
            GodisnjaDobaList.Add(("Jesen", jesen));
            GodisnjaDobaList.Sort((naziv, broj) => broj.broj.CompareTo(naziv.broj));
            foreach (var doba in GodisnjaDobaList)
            {
                Console.WriteLine($"U {doba.naziv} ima {doba.broj} rodenih ljudi.");
            }
            Console.WriteLine();
            Statistika(popisStanovnistva);
        }

        static void NajmladaOsoba(Dictionary<string, (string nameAndSurname, DateTime dateOfBirth)> popisStanovnistva)
        {
            var najmladi = popisStanovnistva.First();
            var datumRodenja = popisStanovnistva.First().Value.dateOfBirth;
            foreach (var osoba in popisStanovnistva)
            {
                if (osoba.Value.dateOfBirth > datumRodenja)
                {
                    najmladi = osoba;
                    datumRodenja = osoba.Value.dateOfBirth;
                }
            }
            Console.WriteLine($"Najmladi stanovnik je {najmladi} roden {datumRodenja.ToString("dd.MM.yyyy.")}.\n");
            Statistika(popisStanovnistva);
        }

        static void NajstarijaOsoba(Dictionary<string, (string nameAndSurname, DateTime dateOfBirth)> popisStanovnistva)
        {
            var najstariji = popisStanovnistva.First();
            var datumRodenja = popisStanovnistva.First().Value.dateOfBirth;
            foreach (var osoba in popisStanovnistva)
            {
                if (osoba.Value.dateOfBirth < datumRodenja)
                {
                    najstariji = osoba;
                    datumRodenja = osoba.Value.dateOfBirth;
                }
            }
            Console.WriteLine($"Najstariji stanovnik je {najstariji} roden {datumRodenja.ToString("dd.MM.yyyy.")}.\n");
            Statistika(popisStanovnistva);
        }

        static void ProsjecnaDob(Dictionary<string, (string nameAndSurname, DateTime dateOfBirth)> popisStanovnistva)
        {
            int starostUkupnoDani = 0;
            TimeSpan starost;
            foreach (var osoba in popisStanovnistva)
            {
                starost = DateTime.Now - osoba.Value.dateOfBirth;
                starostUkupnoDani += starost.Days;
            }
            var prosjek = (100 * Math.Round((double)(starostUkupnoDani / popisStanovnistva.Count) / 365.25, 2)) / 100;
            Console.WriteLine($"Prosjek godina cijelog stanovnistva je {prosjek} godina.\n");
            Statistika(popisStanovnistva);
        }

        static void MedijanGodina(Dictionary<string, (string nameAndSurname, DateTime dateOfBirth)> popisStanovnistva)
        {
            var medijan = DateTime.Now.Year - popisStanovnistva.ElementAt(Convert.ToInt32(Math.Round((double)popisStanovnistva.Count() / 2))).Value.dateOfBirth.Year;
            Console.WriteLine($"Medijan godina je: {medijan}.\n");
            Statistika(popisStanovnistva);
        }
    }
}