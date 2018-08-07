using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TTT.ViewModel
{
    // Computer operations, moves etc.
    public class Computer : Player
    {
        public Computer(Board board, string key)
        {
            // Board inherited from player class
            this.Board = board;
            this.Key = key;
            this.Name = "Player " + key;
        }

        // Checks if either player or CPU has a potential win, 
        // if yes then return the button that will cause it
        private string TryWinOrDefend(string s)
        {
            // Check horizontal
            // Top row
            if ((Board.T00.Value == Board.T01.Value) && (Board.T01.Value == s) && (Board.T02.Value == ""))
                return "B02";
            else if ((Board.T00.Value == Board.T02.Value) && (Board.T02.Value == s) && (Board.T01.Value == ""))
                return "B01";
            else if ((Board.T01.Value == Board.T02.Value) && (Board.T02.Value == s) && (Board.T00.Value == ""))
                return "B00";
            // Middle row
            else if ((Board.T10.Value == Board.T11.Value) && (Board.T11.Value == s) && (Board.T12.Value == ""))
                return "B12";
            else if ((Board.T10.Value == Board.T12.Value) && (Board.T12.Value == s) && (Board.T11.Value == ""))
                return "B11";
            else if ((Board.T11.Value == Board.T12.Value) && (Board.T12.Value == s) && (Board.T10.Value == ""))
                return "B10";
            // Bottom row
            else if ((Board.T20.Value == Board.T21.Value) && (Board.T21.Value == s) && (Board.T22.Value == ""))
                return "B22";
            else if ((Board.T20.Value == Board.T22.Value) && (Board.T22.Value == s) && (Board.T21.Value == ""))
                return "B21";
            else if ((Board.T21.Value == Board.T22.Value) && (Board.T22.Value == s) && (Board.T20.Value == ""))
                return "B20";
            // Vertical Check
            // First column
            else if ((Board.T00.Value == Board.T10.Value) && (Board.T10.Value == s) && (Board.T20.Value == ""))
                return "B20";
            else if ((Board.T00.Value == Board.T20.Value) && (Board.T20.Value == s) && (Board.T10.Value == ""))
                return "B10";
            else if ((Board.T20.Value == Board.T10.Value) && (Board.T10.Value == s) && (Board.T00.Value == ""))
                return "B00";
            // Second column
            else if ((Board.T01.Value == Board.T11.Value) && (Board.T11.Value == s) && (Board.T21.Value == ""))
                return "B21";
            else if ((Board.T01.Value == Board.T21.Value) && (Board.T21.Value == s) && (Board.T11.Value == ""))
                return "B11";
            else if ((Board.T21.Value == Board.T11.Value) && (Board.T11.Value == s) && (Board.T01.Value == ""))
                return "B01";
            // Third column
            else if ((Board.T02.Value == Board.T12.Value) && (Board.T12.Value == s) && (Board.T22.Value == ""))
                return "B22";
            else if ((Board.T02.Value == Board.T22.Value) && (Board.T22.Value == s) && (Board.T12.Value == ""))
                return "B12";
            else if ((Board.T22.Value == Board.T12.Value) && (Board.T12.Value == s) && (Board.T02.Value == ""))
                return "B02";
            // Diagonal checks
            // Top left to bottom right
            else if ((Board.T00.Value == Board.T11.Value) && (Board.T11.Value == s) && (Board.T22.Value == ""))
                return "B22";
            else if ((Board.T00.Value == Board.T22.Value) && (Board.T22.Value == s) && (Board.T11.Value == ""))
                return "B11";
            else if ((Board.T11.Value == Board.T22.Value) && (Board.T22.Value == s) && (Board.T00.Value == ""))
                return "B00";
            // Top right to bottom left
            else if ((Board.T02.Value == Board.T11.Value) && (Board.T11.Value == s) && (Board.T20.Value == ""))
                return "B20";
            else if ((Board.T20.Value == Board.T02.Value) && (Board.T02.Value == s) && (Board.T11.Value == ""))
                return "B11";
            else if ((Board.T11.Value == Board.T20.Value) && (Board.T20.Value == s) && (Board.T02.Value == ""))
                return "B02";
            // If no potential winner next turn
            else
                return null;
        }

        // When computers turn
        public string MakeMove()
        {
            Thread.Sleep(500);
            string tile = null;
            tile = TryWinOrDefend("O");
            if (tile != null)
                return tile;
            else
            {
                tile = TryWinOrDefend("X");

                //if (tile != null)
                //    return tile;
                //else
                //    return MoveRandom();

                // Short hand for the above
                return (tile == null) ? MoveRandom() : tile;
                
            }
        }

        // Computer makes a random move
        private string MoveRandom()
        {
            Random r = new Random();
            Tile t = (Tile)Board.tiles[r.Next(Board.tiles.Count - 1)];
            return "B" + t.RowColumn;
        }
    }
}
