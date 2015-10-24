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
            bool[] valids = Enumerable.Repeat(false, 64).ToArray();
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++ )
                    if (!isTaken(x, y))
                    {
                        for (int i = -1; i < 2; i++)
                        {
                            for (int j = -1; i < 2; i++)
                            {
                                valids[i] = adjacentOpponent(x, y, i, j, player);
                            }
                        }
                    }
            }
            return valids;
        }

        private bool isTaken(int x, int y)
        {
            return getSpace(x, y) != "-";
        }

        private bool adjacentOpponent(int location, int direction, string player, int length=0)
        {
            if (squares[location + direction] == "-")
            {
                return false;
            }
            if (squares[location + direction] == player)
            {
                return length>0;
            }
            return adjacentOpponent(location + direction, direction, player, length+1);
        }

        private bool adjacentOpponent(int x, int y, int dx, int dy, string player, int length = 0)
        {
            if (getSpace(x + dx, y + dy) == "-")
            {
                return false;
            }
            if (getSpace(x + dx, y + dy) == player)
            {
                return length > 0;
            }
            return adjacentOpponent(x, y, dx, dy, player, length + 1);
        }

        public string getSpace(int x, int y){
            if (x > 7 || x < 0 || y > 7 || y < 0)
            {
                return "I";
            }
            return squares[x + y * 8];
        }
    }
}
