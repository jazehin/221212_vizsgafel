using System.Text;

namespace _221212_vizsgafel
{
    internal class Program
    {
        static List<Game> games = new List<Game>();
        static void Main()
        {
            Readfile();
            F1();
            F2();
            F3();
            Console.ReadKey(false);
        }

        private static void F3()
        {
            string genre = "First-person shooter";
            List<Game> gamesOfGenre = games.Where(game => game.Genre == genre).ToList();
            
            Console.WriteLine($"f3: összesen {gamesOfGenre.Count} FPS került fel a listára.");
            Console.WriteLine($"\tpéldául a(z): {gamesOfGenre[new Random().Next(gamesOfGenre.Count)].Title}");

        }

        private static void F2()
        {
            Console.WriteLine("f2: ezekben az években került több, mint 10 cím a listára:");

            /*
            Dictionary<int, int> gamesPerYear = new();
            foreach (Game game in games)
            {
                if (gamesPerYear.ContainsKey(game.Year)) gamesPerYear[game.Year]++;
                else gamesPerYear.Add(game.Year, 1);
            }
            */

            var manyGamesInAYear = games
                .GroupBy(game => game.Year)
                .OrderByDescending(x => x.Count())
                .Where(x => x.Count() > 10)
                .ToDictionary(x => x.Key, x => x.Count());

            foreach (var group in manyGamesInAYear)
            {
                Console.WriteLine($"\t{group.Key}: {group.Value}db");
            }
        }

        private static void F1()
        {
            Console.WriteLine($"f1: összesen {games.Count} játék szerepel a listában!");
        }

        private static void Readfile()
        {
            using StreamReader sr = new(@"..\\..\\..\\..\\res\bestgames.csv");
            while (!sr.EndOfStream) games.Add(new(sr.ReadLine()));
        }
    }
}