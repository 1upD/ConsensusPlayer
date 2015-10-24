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
            return minimaxVal(board, 7, Color).getMove();
        }

        private Result minimaxVal(Board b, int d, string color)
        {    // d is depth
            int bestVal = 0;
            if (d == 0)
                return new Result(0, b.evaluate(Color));
            bool Max = true;
            if (color == "b")
            {     //TOP is MAX
                bestVal = -1000000;
            }
            else
            {  // similarly for BOTTOM’s move
                //BOTTOM is MIN
                bestVal = 1000000;
                Max = false;
                }
            string opponent = "b";
            if (color == "b")
            {
                opponent = "w";
            }

            int bestMove = 0;
            bool[] validMoves = b.ValidMoves(color);
            // Make sure the default value is valid
            for (int i = 0; i < 64; i++)
            {
                if (validMoves[i])
                {
                    bestMove = i;
                    break;
                } 
            }
            // Loop through all possible moves
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

            return new Result(bestMove, bestVal);
        }


//Class to hold returned moves and values
public class Result
{
    private int myMove;
    private int myVal;
    public Result(int move, int val)
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

    }
}
