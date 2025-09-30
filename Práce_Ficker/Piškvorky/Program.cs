namespace Piškvorky
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true) // hlavní smyčka pro opakování hry
            {
                char[,] board = new char[3, 3];
                char aktualnihrac = 'X';

                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 3; j++)
                        board[i, j] = ' ';

                int tah = 0;
                bool konecHry = false;

                while (!konecHry)
                {
                    VykresliPole(board);
                    Console.WriteLine($"Teď hraje broski {aktualnihrac}");

                    // zadani tahu
                    int radek, sloupec;
                    while (true)
                    {
                        Console.Write("Zadej řádek: ");
                        if (!int.TryParse(Console.ReadLine(), out radek) || radek < 1 || radek > 3)
                        {
                            Console.WriteLine("Tohle není platný řádek bro zadej znovu:");
                            continue;
                        }

                        Console.Write("Zadej sloupec: ");
                        if (!int.TryParse(Console.ReadLine(), out sloupec) || sloupec < 1 || sloupec > 3)
                        {
                            Console.WriteLine("Tohle zase není sloupec dawg zadej znovu:");
                            continue;
                        }
                        if (board[radek - 1, sloupec - 1] != ' ')
                        {
                            Console.WriteLine("Neni místo kejmo znovu:");
                            continue;
                        }
                        break;
                    }

                    board[radek - 1, sloupec - 1] = aktualnihrac;
                    tah++;

                    // kontrola výhry
                    for (int i = 0; i < 3; i++)
                    {
                        if ((board[i, 0] == aktualnihrac && board[i, 1] == aktualnihrac && board[i, 2] == aktualnihrac) ||
                            (board[0, i] == aktualnihrac && board[1, i] == aktualnihrac && board[2, i] == aktualnihrac))
                        {
                            VykresliPole(board);
                            Console.WriteLine($"Player {aktualnihrac} má výhru! Je to tam!!");
                            konecHry = true;
                            break;
                        }
                    }

                    // kontrola diagonál
                    if (!konecHry && (
                        (board[0, 0] == aktualnihrac && board[1, 1] == aktualnihrac && board[2, 2] == aktualnihrac) ||
                        (board[0, 2] == aktualnihrac && board[1, 1] == aktualnihrac && board[2, 0] == aktualnihrac)))
                    {
                        VykresliPole(board);
                        Console.WriteLine($"Player {aktualnihrac} má výhru! Je to tam!!");
                        konecHry = true;
                    }

                    // kontrola remízy
                    if (!konecHry && tah == 9)
                    {
                        VykresliPole(board);
                        Console.WriteLine("REMÍÍÍZAAAA");
                        konecHry = true;
                    }

                    if (!konecHry)
                        aktualnihrac = (aktualnihrac == 'X') ? 'O' : 'X';
                }

                // Dotaz na pokračování až po skončení hry
                Console.WriteLine("Continue? ANO/NE ?");
                string odpoved = Console.ReadLine()?.Trim().ToUpper();
                if (odpoved != "ANO")
                {
                    Console.WriteLine("Nevadzii tak zase příště!");
                    break;
                }
            }
        }

        static void VykresliPole(char[,] board)
        {
            Console.WriteLine("  1 2 3");
            for (int i = 0; i < 3; i++)
            {
                Console.Write($"{i + 1} ");
                for (int j = 0; j < 3; j++)
                {
                    Console.Write(board[i, j]);
                    if (j < 2) Console.Write("|");
                }
                Console.WriteLine();
                if (i < 2) Console.WriteLine("  -+-+-");
            }
        }
    }
}
