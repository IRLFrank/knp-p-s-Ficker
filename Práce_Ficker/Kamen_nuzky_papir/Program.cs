namespace Kamen_nuzky_papir
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Kámen, Nůžky, Papír!");

            string[] moznosti = { "kámen", "nůžky", "papír" };
            Random random = new Random();

            while (true)
            {
                // Zadání a kontrola platnosti tahu hráče
                string hracovavolba;
                while (true)
                {
                    Console.WriteLine("Zadej svůj tah (kámen, nůžky, papír):");
                    hracovavolba = Console.ReadLine();
                    hracovavolba = hracovavolba?.Trim().ToLower();
                    if (Array.Exists(moznosti, element => element == hracovavolba))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Neplatný tah! Zkus to znovu.");
                    }
                }

                // Náhodná volba počítače
                string pcVolba = moznosti[random.Next(moznosti.Length)];
                Console.WriteLine($"Počítač si vybral: {pcVolba}");

                // Vyhodnocení výsledku
                if (hracovavolba == pcVolba)
                {
                    Console.WriteLine("Remíza! Zkus to znovu.");
                }
                else if ((hracovavolba == "kámen" && pcVolba == "nůžky") ||
                         (hracovavolba == "nůžky" && pcVolba == "papír") ||
                         (hracovavolba == "papír" && pcVolba == "kámen"))
                {
                    Console.WriteLine("Vyhrál jsi!");
                }
                else
                {
                    Console.WriteLine("Prohrál jsi! Zkus to znovu.");
                }

                // Dotaz na pokračování až po kole
                Console.WriteLine("Chtěl by jsi ještě hrát? (ano/ne):");
                string odpovedhrace = Console.ReadLine();

                if (odpovedhrace?.Trim().ToLower() == "ne")
                {
                    Console.WriteLine("Díky za hru! Nashledanou!");
                    break;
                }
            }
        }
    }
}
