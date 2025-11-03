using System.Linq;
using System.IO;

namespace Kamen_nuzky_papir
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Kámen, Nůžky, Papír, Ještěrka, Spock!");

            string[] moznosti = { "kámen", "nůžky", "papír", "ještěrka", "spock" };
            string[] motivace = {
                "Dobrá práce!",
                "Jen tak dál!",
                "To bylo těsné!",
                "Zkus změnit taktiku!",
                "Jsi na dobré cestě!",
                "Počítač se učí z tvých tahů..."
            };
            Random random = new Random();

            int celkoveSkoreHrac = 0;
            int celkoveSkorePC = 0;
            int celkoveRemizy = 0;
            int pocetHer = 0;
            int soucetKol = 0;

            string jmeno = null;

            while (true) // hlavní smyčka pro opakované hraní
            {
                // Možnost změnit jméno hráče
                Console.Write("Chceš změnit jméno hráče? (ano/ne): ");
                string zmenaJmena = Console.ReadLine()?.Trim().ToLower();
                if (zmenaJmena == "ano" || string.IsNullOrEmpty(jmeno))
                {
                    Console.Write("Zadej své jméno: ");
                    jmeno = Console.ReadLine()?.Trim();
                    if (string.IsNullOrEmpty(jmeno)) jmeno = "Hráč";
                }

                // Volba proti komu hrát
                Console.Write("Chceš hrát proti počítači (PC) nebo proti hráči (HRAC)? ");
                string protivnik = Console.ReadLine()?.Trim().ToLower();
                bool protiPC = protivnik != "hrac";

                // Volba počtu vítězství
                Console.Write("Na kolik vítězství chceš hrát? ");
                int maxVitezstvi;
                while (!int.TryParse(Console.ReadLine(), out maxVitezstvi) || maxVitezstvi < 1)
                {
                    Console.WriteLine("Zadej kladné číslo.");
                }

                int skoreHrac = 0;
                int skorePC = 0;
                int remizy = 0;
                int pocetKol = 0;

                var historieHrac = new List<string>();
                var historiePC = new List<string>();
                var statistikaHrac = new Dictionary<string, int>();
                foreach (var moznost in moznosti)
                    statistikaHrac[moznost] = 0;

                Console.WriteLine("\nNapiš 'pravidla' kdykoliv pro zobrazení pravidel hry.\n");

                while (skoreHrac < maxVitezstvi && skorePC < maxVitezstvi)
                {
                    // Zadání a kontrola platnosti tahu hráče
                    string hracovavolba;
                    while (true)
                    {
                        Console.WriteLine($"Zadej svůj tah ({string.Join(", ", moznosti)}), nebo napiš 'konec' pro ukončení:");
                        hracovavolba = Console.ReadLine();
                        hracovavolba = hracovavolba?.Trim().ToLower();
                        if (hracovavolba == "konec")
                        {
                            Console.WriteLine("Hra ukončena hráčem.");
                            return;
                        }
                        if (hracovavolba == "pravidla")
                        {
                            Console.WriteLine("\nPravidla hry:");
                            Console.WriteLine("Kámen rozdrtí nůžky a ještěrku, nůžky přestřihnou papír a ještěrku, papír zabalí kámen a Spocka, ještěrka otráví Spocka a sní papír, Spock rozbije nůžky a rozdrtí kámen.");
                            Console.WriteLine("Zvol jednu z možností. Pokud zvolíš stejnou jako soupeř, je to remíza.\n");
                            continue;
                        }
                        if (Array.Exists(moznosti, element => element == hracovavolba))
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Neplatný tah! Zkus to znovu.");
                        }
                    }

                    // Tah druhého hráče nebo počítače
                    string pcVolba;


                    if (protiPC)
                    {
                        // Animace rozhodování počítače
                        Console.Write("Počítač přemýšlí");
                        for (int i = 0; i < 3; i++)
                        {
                            Thread.Sleep(300);
                            Console.Write(".");
                        }
                        Console.WriteLine();
                        pcVolba = moznosti[random.Next(moznosti.Length)];
                        Console.WriteLine($"Počítač si vybral: {pcVolba}");
                    }
                    else
                    {
                        while (true)
                        {
                            Console.WriteLine($"Druhý hráč, zadej svůj tah ({string.Join(", ", moznosti)}):");
                            pcVolba = Console.ReadLine();
                            pcVolba = pcVolba?.Trim().ToLower();
                            if (Array.Exists(moznosti, element => element == pcVolba))
                                break;
                            else
                                Console.WriteLine("Neplatný tah! Zkus to znovu.");
                        }
                    }

                    // Uložení historie a statistiky
                    historieHrac.Add(hracovavolba);
                    historiePC.Add(pcVolba);
                    statistikaHrac[hracovavolba]++;

                    // Vyhodnocení výsledku
                    string vitezKola;
                    if (hracovavolba == pcVolba)
                    {
                        Console.WriteLine("Remíza!");
                        remizy++;
                        vitezKola = "Remíza";
                    }
                    else if (
                        (hracovavolba == "kámen" && (pcVolba == "nůžky" || pcVolba == "ještěrka")) ||
                        (hracovavolba == "nůžky" && (pcVolba == "papír" || pcVolba == "ještěrka")) ||
                        (hracovavolba == "papír" && (pcVolba == "kámen" || pcVolba == "spock")) ||
                        (hracovavolba == "ještěrka" && (pcVolba == "spock" || pcVolba == "papír")) ||
                        (hracovavolba == "spock" && (pcVolba == "nůžky" || pcVolba == "kámen"))
                    )
                    {
                        Console.WriteLine("Vyhrál jsi!");
                        skoreHrac++;
                        vitezKola = jmeno;
                    }
                    else
                    {
                        Console.WriteLine(protiPC ? "Prohrál jsi!" : "Druhý hráč vyhrál!");
                        skorePC++;
                        vitezKola = protiPC ? "Počítač" : "Druhý hráč";
                    }

                    pocetKol++;
                    // Motivační komentář
                    Console.WriteLine(motivace[random.Next(motivace.Length)]);
                    Console.WriteLine($"Vítěz kola: {vitezKola}");
                    Console.WriteLine($"Skóre: {jmeno} {skoreHrac} - {(protiPC ? "PC" : "Druhý hráč")} {skorePC} (Remíz: {remizy})");
                    Console.WriteLine();
                }

                Console.WriteLine("Konec hry!");
                if (skoreHrac > skorePC)
                    Console.WriteLine($"Gratuluji, {jmeno}, vyhrál jsi!");
                else if (skoreHrac < skorePC)
                    Console.WriteLine(protiPC ? "Počítač vyhrál. Zkus to znovu!" : "Druhý hráč vyhrál!");
                else
                    Console.WriteLine("Remíza v sérii!");

                Console.WriteLine($"Celkem kol: {pocetKol}, Remíz: {remizy}");

                // Výpis historie tahů
                Console.WriteLine("\nHistorie tahů:");
                for (int i = 0; i < historieHrac.Count; i++)
                {
                    Console.WriteLine($"Kolo {i + 1}: {jmeno} - {historieHrac[i]}, {(protiPC ? "PC" : "Druhý hráč")} - {historiePC[i]}");
                }

                // Statistiky tahů hráče
                Console.WriteLine("\nStatistika tahů hráče:");
                foreach (var moznost in moznosti)
                {
                    Console.WriteLine($"{moznost}: {statistikaHrac[moznost]}x");
                }

                // Nejčastější volba hráče
                var nejcastejsiTah = statistikaHrac.OrderByDescending(x => x.Value).First().Key;
                Console.WriteLine($"\nNejčastější volba hráče: {nejcastejsiTah}");

                // Procentuální úspěšnost hráče
                double uspesnost = pocetKol > 0 ? (double)skoreHrac / pocetKol * 100 : 0;
                Console.WriteLine($"\nÚspěšnost hráče {jmeno}: {uspesnost:F1}%");

                // Celkové skóre
                celkoveSkoreHrac += skoreHrac;
                celkoveSkorePC += skorePC;
                celkoveRemizy += remizy;
                pocetHer++;
                soucetKol += pocetKol;

                Console.WriteLine($"\nCelkové skóre po {pocetHer} hrách:");
                Console.WriteLine($"{jmeno}: {celkoveSkoreHrac} | {(protiPC ? "PC" : "Druhý hráč")}: {celkoveSkorePC} | Remíz: {celkoveRemizy}");

                // Průměrná délka série
                double prumernaDelka = pocetHer > 0 ? (double)soucetKol / pocetHer : 0;
                Console.WriteLine($"\nPrůměrná délka série: {prumernaDelka:F2} kol na hru");

                // Uložení výsledků do souboru
                Console.Write("\nChceš uložit statistiky do souboru? (ano/ne): ");
                string ulozit = Console.ReadLine()?.Trim().ToLower();
                if (ulozit == "ano")
                {
                    string nazevSouboru = $"statistika_{jmeno}_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
                    using (var sw = new StreamWriter(nazevSouboru))
                    {
                        sw.WriteLine($"Statistika hry Kámen, Nůžky, Papír, Ještěrka, Spock ({DateTime.Now})");
                        sw.WriteLine($"Hráč: {jmeno}");
                        sw.WriteLine($"Celkové skóre: {celkoveSkoreHrac} | {(protiPC ? "PC" : "Druhý hráč")}: {celkoveSkorePC} | Remíz: {celkoveRemizy}");
                        sw.WriteLine($"Průměrná délka série: {prumernaDelka:F2} kol na hru");
                        sw.WriteLine("\nStatistika tahů hráče:");
                        foreach (var moznost in moznosti)
                            sw.WriteLine($"{moznost}: {statistikaHrac[moznost]}x");
                        sw.WriteLine($"\nNejčastější volba hráče: {nejcastejsiTah}");
                        sw.WriteLine($"\nÚspěšnost hráče: {uspesnost:F1}%");
                        sw.WriteLine("\nHistorie tahů:");
                        for (int i = 0; i < historieHrac.Count; i++)
                            sw.WriteLine($"Kolo {i + 1}: {jmeno} - {historieHrac[i]}, {(protiPC ? "PC" : "Druhý hráč")} - {historiePC[i]}");
                    }
                    Console.WriteLine($"Statistika byla uložena do souboru: {nazevSouboru}");
                }

                Console.Write("\nChceš hrát znovu? (ano/ne): ");
                string odpoved = Console.ReadLine()?.Trim().ToLower();
                if (odpoved != "ano")
                {
                    Console.WriteLine("Díky za hru!");
                    break;
                }
            }
        }
    }
}
