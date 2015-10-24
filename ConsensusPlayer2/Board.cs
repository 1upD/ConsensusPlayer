using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsensusPlayer2
{
    public class Board
    {
        public int width { get; set; }
        public int height { get; set; }
        public string[] squares { get; set; }

        public bool[] ValidMoves(string player)
        {
            int[] directions = { -9, -8, -7, -1, 1, 7, 8, 9 };
            bool[] valids = Enumerable.Repeat(false, 64).ToArray();
            for (int i = 0; i < 64; i++)
            {
                if (isTaken(i))
                {
                    valids[i] = false;
                }
                else
                {
                    foreach (int direction in directions)
                    {
                        valids[i] = adjacentOpponent(i, direction, player);
                    }
                }
            }
            return valids;
        }

        private bool isTaken(int i)
        {
            return squares[i] != "-";
        }

        private bool adjacentOpponent(int location, int direction, string player)
        {
            if (squares[location + direction] == "-")
            {
                return false;
            }
            if (squares[location + direction] == player)
            {
                return true;
            }
            return adjacentOpponent(location + direction, direction, player);
        }

    }
}
