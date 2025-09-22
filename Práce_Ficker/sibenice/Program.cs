using System.IO;

namespace sibenice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // nacteni slova ze souboru
            string[] slova = File.ReadAllLines("slova.txt"); // POZOR: Pokud soubor neexistuje nebo je prázdný, program spadne. Doporučuji ošetřit výjimku a zkontrolovat, že pole není prázdné.

            Random random = new Random();
            // vybrani nahodneho slova
            string slovo = slova[random.Next(slova.Length)].Trim().ToLower(); // POZOR: Pokud je v souboru prázdný řádek, můžeš dostat prázdné slovo.

            int pokusy = 12;
            var uhodnutaPismena = new HashSet<char>();

            while (true)
            {
                // jaky je stav slova?
                Console.Write("Slovo: ");
                foreach (char pismeno in slovo)
                {
                    if (uhodnutaPismena.Contains(pismeno))
                        Console.Write(pismeno + " ");
                    else
                        Console.Write("_ ");
                }
                Console.WriteLine();

                // zobrazeni zbyvajicich pokusu
                Console.WriteLine($"Zbývající pokusy: {pokusy}");

                // ptani se na pismeno
                Console.Write("Tak jake pismenko tam bude?: ");
                string vstup = Console.ReadLine()?.Trim().ToLower();
                if (string.IsNullOrEmpty(vstup) || vstup.Length != 1)
                {
                    Console.WriteLine("zadej jedno pismenko.");
                    continue; // CHYTÁK: Pokud uživatel zadá více znaků nebo nic, správně se vrací na začátek smyčky.
                }
                char tip = vstup[0];

                //  kontrola hadani slova
                if (uhodnutaPismena.Contains(tip))
                {
                    Console.WriteLine("Toto písmeno už jsi hádal.");
                    continue;
                }   

                // pokud bylo pismeno spravne napiseme do uhodnutaPismena
                uhodnutaPismena.Add(tip);
                if (!slovo.Contains(tip))
                {
                    pokusy--;
                    Console.WriteLine("Špatně!");
                    // CHYTÁK: Pokud uživatel zadá speciální znak nebo číslici, bude to považováno za špatný pokus.
                }

                // kontrola vyhry nebo prohry
                bool vyhra = slovo.All(p => uhodnutaPismena.Contains(p));
                if (vyhra)
                {
                    Console.WriteLine($"Gratuluji, uhodl jsi slovo: {slovo}");
                    break;
                }
                if (pokusy == 0)
                {
                    Console.WriteLine($"Prohrál jsi! Hledané slovo bylo: {slovo}");
                    break;
                }
            }
        }
    }
}
