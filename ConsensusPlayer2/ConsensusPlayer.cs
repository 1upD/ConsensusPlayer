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
            /*bool[] moves = board.ValidMoves(Color);
            int bestMove = 0;
                      int bestScore = -9999999;
                        for (int i = 0; i < 64; i++)
                        {
                            if (moves[i])
                            {
                                Board newBoard = board.move(i, Color);
                                int score = board.evaluate(Color);
                                if (score > bestScore)
                                {
                                    bestScore = score;
                                    bestMove = i;
                                }
                            }
                        }
             */
            Drd26Result best = minimaxVal(board, 4, Color);
            return best.getMove();
        }

// Mancala stuff

        private int myLimit;
        private bool timeUp;
        private int turn;

        private Drd26Result minimaxVal(Board b, int d, string color)
        {    // d is depth
            int bestVal = 0;
            int bestMove = 0;
            if (d == 0)
                return new Drd26Result(0, b.evaluate(Color));
            if (color == "b")
            {     //TOP is MAX
                bestVal = -1000000;
                minOrMax(b, d, ref bestVal, ref bestMove, "b", true);
            }
            else
            {  // similarly for BOTTOM’s move
                //BOTTOM is MIN
                bestVal = 1000000;
                minOrMax(b, d, ref bestVal, ref bestMove, "w", false);
                    
                }

            return new Drd26Result(bestMove, bestVal);
        }

        private void minOrMax(Board b, int d, ref int bestVal, ref int bestMove, String color, bool Max)
        {
            string opponent = "b";
            if (color == "b")
            {
                opponent = "w";
            }
            bool[] validMoves = b.ValidMoves(color);
            for (int move = 0; move < 64; move++)
            {
                if (validMoves[move])
                {
                    Board b1 = b.move(move, color); // Make a copy of the move by moving
                    int val = minimaxVal(b1, d - 1, opponent).getVal();   //find its value
                    if ((val > bestVal && Max) || (val < bestVal && !Max))
                    {        //remember if best
                        bestVal = val;
                        bestMove = move;
                    }
                }
            }
        }


//Class to hold returned moves and values
public class Drd26Result
{
    private int myMove;
    private int myVal;
    public Drd26Result(int move, int val)
    {
        myMove = move;
        myVal = val;
    }
    public int getVal()
    {
        return myVal;
    }

    public int getMove()
    {
        return myMove;
    }
}

// End of Mancala stuff


    }
}
