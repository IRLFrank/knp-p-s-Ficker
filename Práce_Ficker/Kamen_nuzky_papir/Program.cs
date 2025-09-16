namespace Kamen_nuzky_papir
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Kámen, Nůžky, Papír!");

            while (true)
            {
                Console.WriteLine("Chtěl by jsi ještě hrát? (ano/ne):");
                string odpovedhrace = Console.ReadLine();

                if (odpovedhrace?.Trim().ToLower() == "ne")
                {
                    Console.WriteLine("Díky za hru! Nashledanou!");
                    break;
                }

                // Sem můžeš přidat další logiku hry, pokud chceš
            }
        }
    }
}
