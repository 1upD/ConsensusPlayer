using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsensusPlayer2
{
    public class ConsensusPlayer
    {
        public string Color { get; set; }
        public static int Main(string[] args)
        {
            try
            {
                var input = System.IO.File.ReadAllText(@"input.json");
                var boardstring = System.Text.RegularExpressions.Regex.Match(input, @"\{.*\}").Value;
                Console.Write("Hello");
                ConsensusPlayer player = new ConsensusPlayer();
                Board board = JsonConvert.DeserializeObject<Board>(boardstring);
                for (int i = 0; i < args.Length; i++)
                {
                    if (args[i] == "-p")
                    {
                        player.Color = args[i + 1].ElementAt(0).ToString();
                    }
                }
                System.IO.File.WriteAllText(@"playercolor.txt", player.Color);
                System.IO.File.WriteAllLines(@"board.txt", board.squares);

                return player.chooseMove(board);
            }
            catch (Exception e)
            {
                System.IO.File.WriteAllText(@"Exception.txt", e.Message);
                return 0;
            }
            }

        public int chooseMove(Board board)
        {
            bool[] moves = board.ValidMoves(Color);
            int bestMove = 0;
            int bestScore = -9999999;
            for (int i = 0; i < 64; i++)
            {
                if (moves[i])
                {
                    int score = board.evaluate(Color);
                    if (score > bestScore)
                    {
                        bestScore = score;
                        bestMove = i;
                    }
                }
            }
            return bestMove;
        }
    }
}
