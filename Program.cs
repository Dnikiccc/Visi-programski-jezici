using System;
using System.Collections.Generic;
using System.Linq;

namespace FitnessApp
{
    public class Trening
    {
        public string TipAktivnosti { get; set; } = string.Empty; // inicijalizacija
        public int Trajanje { get; set; }
        public int Kalorije { get; set; }
        public DateTime Datum { get; set; }
    }

    class Program
    {
        static List<Trening> treninzi = new List<Trening>();

        static void Main(string[] args)
        {
            int izbor;

            do
            {
                PrikaziMeni();

                if (!int.TryParse(Console.ReadLine(), out izbor))
                {
                    Console.WriteLine("Neispravan unos!");
                    Console.ReadKey();
                    continue;
                }

                switch (izbor)
                {
                    case 1:
                        DodajTrening();
                        break;
                    case 2:
                        PrikaziSve();
                        break;
                    case 3:
                        ObrisiTrening();
                        break;
                    case 4:
                        Pretrazi();
                        break;
                    case 5:
                        Napredak();
                        break;
                    case 0:
                        Console.WriteLine("Izlaz...");
                        break;
                    default:
                        Console.WriteLine("Nepostojeća opcija!");
                        Console.ReadKey();
                        break;
                }

            } while (izbor != 0);
        }

        static void PrikaziMeni()
        {
            Console.Clear();
            Console.WriteLine("=================================");
            Console.WriteLine(" EVIDENCIJA FITNESS AKTIVNOSTI");
            Console.WriteLine("=================================");
            Console.WriteLine("1. Dodaj novi trening");
            Console.WriteLine("2. Prikaži sve treninge");
            Console.WriteLine("3. Obriši trening");
            Console.WriteLine("4. Pretraži po tipu aktivnosti");
            Console.WriteLine("5. Prikaži napredak kroz vrijeme");
            Console.WriteLine("0. Izlaz");
            Console.WriteLine("---------------------------------");

            int ukupnoKalorija = treninzi.Sum(t => t.Kalorije);
            Console.WriteLine($"Ukupno treninga: {treninzi.Count} | Ukupno kalorija: {ukupnoKalorija}");
            Console.WriteLine("---------------------------------");
            Console.Write("Izbor: ");
        }

        static void DodajTrening()
        {
            Console.Write("Tip aktivnosti: ");
            string? tipUnos = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(tipUnos))
            {
                Console.WriteLine("Neispravan tip aktivnosti!");
                Console.ReadKey();
                return;
            }

            Console.Write("Trajanje (min): ");
            if (!int.TryParse(Console.ReadLine(), out int trajanje) || trajanje <= 0)
            {
                Console.WriteLine("Neispravno trajanje!");
                Console.ReadKey();
                return;
            }

            Console.Write("Kalorije: ");
            if (!int.TryParse(Console.ReadLine(), out int kalorije) || kalorije <= 0)
            {
                Console.WriteLine("Neispravne kalorije!");
                Console.ReadKey();
                return;
            }

            Console.Write("Datum (yyyy-mm-dd): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime datum))
            {
                Console.WriteLine("Neispravan datum!");
                Console.ReadKey();
                return;
            }

            treninzi.Add(new Trening
            {
                TipAktivnosti = tipUnos,
                Trajanje = trajanje,
                Kalorije = kalorije,
                Datum = datum
            });

            Console.WriteLine("Trening dodan!");
            Console.ReadKey();
        }

        static void PrikaziSve()
        {
            if (treninzi.Count == 0)
            {
                Console.WriteLine("Nema treninga.");
            }
            else
            {
                for (int i = 0; i < treninzi.Count; i++)
                {
                    var t = treninzi[i];
                    Console.WriteLine($"{i}. {t.TipAktivnosti} | {t.Trajanje} min | {t.Kalorije} kcal | {t.Datum.ToShortDateString()}");
                }
            }

            Console.ReadKey();
        }

        static void ObrisiTrening()
        {
            if (treninzi.Count == 0)
            {
                Console.WriteLine("Nema treninga.");
                Console.ReadKey();
                return;
            }

            PrikaziSve();

            Console.Write("\nUnesi indeks: ");
            if (int.TryParse(Console.ReadLine(), out int index) &&
                index >= 0 && index < treninzi.Count)
            {
                treninzi.RemoveAt(index);
                Console.WriteLine("Obrisano!");
            }
            else
            {
                Console.WriteLine("Neispravan unos!");
            }

            Console.ReadKey();
        }

        static void Pretrazi()
        {
            Console.Write("Tip aktivnosti: ");
            string? unos = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(unos))
            {
                Console.WriteLine("Neispravan unos!");
                Console.ReadKey();
                return;
            }

            string tip = unos.ToLower();
            var rezultat = treninzi.Where(t => t.TipAktivnosti.ToLower().Contains(tip));

            if (!rezultat.Any())
            {
                Console.WriteLine("Nema rezultata.");
            }
            else
            {
                foreach (var t in rezultat)
                {
                    Console.WriteLine($"{t.TipAktivnosti} | {t.Trajanje} min | {t.Kalorije} kcal | {t.Datum.ToShortDateString()}");
                }
            }

            Console.ReadKey();
        }

        static void Napredak()
        {
            var grupisano = treninzi
                .GroupBy(t => t.Datum.Date)
                .Select(g => new
                {
                    Datum = g.Key,
                    Kalorije = g.Sum(x => x.Kalorije),
                    Trajanje = g.Sum(x => x.Trajanje)
                });

            foreach (var g in grupisano)
            {
                Console.WriteLine($"{g.Datum.ToShortDateString()} - {g.Kalorije} kcal | {g.Trajanje} min");
            }

            Console.ReadKey();
        }
    }
}
