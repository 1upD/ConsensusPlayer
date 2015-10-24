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
    public int height {get; set;}
    public string[] squares { get; set; }

    public int[] ValidMoves()
    {
        return null;
    }

    }

    boolean validMoves(Board b)
    {
    boolean valids[64] = Enumerable.Repeat(false, 64).toArray();
		for ( i =0; i < 64; i++){
			if isTaken() valids[i] = false;
			else
				valids[i] = adjacentOpponent(i, direction)
				
		}
	}

	adjacentOpponent(Board b, location, direction, length=0) {
		if (b[location+direction] is opponents
			length++;
			return adjacentOpponent(location+direction, direction, length)
		else if is ours
			if length >0
				return true
		return false
	}
}
