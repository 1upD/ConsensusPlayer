﻿using System;
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
                            for (int j = -1; j < 2; j++)
                            {
                                if (!(i == 0 && j == 0))
                                {
                                    if (!valids[x + y * 8])
                                    {
                                        valids[x + y * 8] = adjacentOpponent(x, y, i, j, player);
                                    }
                                }
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

        private bool adjacentOpponent(int x, int y, int dx, int dy, string player, int length = 0)
        {
            if (getSpace(x + dx, y + dy) == "-" || getSpace(x + dx, y + dy) == "I")
            {
                return false;
            }
            if (getSpace(x + dx, y + dy) == player)
            {
                return length > 0;
            }
            return adjacentOpponent(x + dx, y + dy, dx, dy, player, length + 1);
        }

        public string getSpace(int x, int y){
            if (x > 7 || x < 0 || y > 7 || y < 0)
            {
                return "I";
            }
            return squares[x + y * 8].ToLower();
        }

    public int evaluate(String player)
{
        int us = 0;
        int them = 0;
        int empty = 0;
        for ( int i=0; i<64; i++)
        {
	        if ( player == squares[i] ) us++;
	        else if ( "-" == squares[i] ) empty++;
	        else them++;
        }
        return us-them;
}

    }
}
