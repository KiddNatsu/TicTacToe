using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TTT.LOGIC
{
    // Computer operations, moves etc.
    public class Computer : Player
    {
        public Computer(Board board)
        {
            // Board inherited from player class
            this.Board = board;
        }

        // Checks if either player or CPU has a potential win, 
        // if yes then return the button that will cause it
        private Button TryWinOrDefend(string s)
        {
            // Check horizontal
            // Top row
            if ((Board.A00.Text == Board.A01.Text) && (Board.A01.Text == s) && (Board.A02.Text == ""))
                return Board.A02;
            else if ((Board.A00.Text == Board.A02.Text) && (Board.A02.Text == s) && (Board.A01.Text == ""))
                return Board.A01;
            else if ((Board.A01.Text == Board.A02.Text) && (Board.A02.Text == s) && (Board.A00.Text == ""))
                return Board.A00;
            // Middle row
            else if ((Board.A10.Text == Board.A11.Text) && (Board.A11.Text == s) && (Board.A12.Text == ""))
                return Board.A12;
            else if ((Board.A10.Text == Board.A12.Text) && (Board.A12.Text == s) && (Board.A11.Text == ""))
                return Board.A11;
            else if ((Board.A11.Text == Board.A12.Text) && (Board.A12.Text == s) && (Board.A10.Text == ""))
                return Board.A10;
            // Bottom row
            else if ((Board.A20.Text == Board.A21.Text) && (Board.A21.Text == s) && (Board.A22.Text == ""))
                return Board.A22;
            else if ((Board.A20.Text == Board.A22.Text) && (Board.A22.Text == s) && (Board.A21.Text == ""))
                return Board.A21;
            else if ((Board.A21.Text == Board.A22.Text) && (Board.A22.Text == s) && (Board.A20.Text == ""))
                return Board.A20;
            // Vertical Check
            // First column
            else if ((Board.A00.Text == Board.A10.Text) && (Board.A10.Text == s) && (Board.A20.Text == ""))
                return Board.A20;
            else if ((Board.A00.Text == Board.A20.Text) && (Board.A20.Text == s) && (Board.A10.Text == ""))
                return Board.A10;
            else if ((Board.A20.Text == Board.A10.Text) && (Board.A10.Text == s) && (Board.A00.Text == ""))
                return Board.A00;
            // Second column
            else if ((Board.A01.Text == Board.A11.Text) && (Board.A11.Text == s) && (Board.A21.Text == ""))
                return Board.A21;
            else if ((Board.A01.Text == Board.A21.Text) && (Board.A21.Text == s) && (Board.A11.Text == ""))
                return Board.A11;
            else if ((Board.A21.Text == Board.A11.Text) && (Board.A11.Text == s) && (Board.A01.Text == ""))
                return Board.A01;
            // Third column
            else if ((Board.A02.Text == Board.A12.Text) && (Board.A12.Text == s) && (Board.A22.Text == ""))
                return Board.A22;
            else if ((Board.A02.Text == Board.A22.Text) && (Board.A22.Text == s) && (Board.A12.Text == ""))
                return Board.A12;
            else if ((Board.A22.Text == Board.A12.Text) && (Board.A12.Text == s) && (Board.A02.Text == ""))
                return Board.A02;
            // Diagonal checks
            // Top left to bottom right
            else if ((Board.A00.Text == Board.A11.Text) && (Board.A11.Text == s) && (Board.A22.Text == ""))
                return Board.A22;
            else if ((Board.A00.Text == Board.A22.Text) && (Board.A22.Text == s) && (Board.A11.Text == ""))
                return Board.A11;
            else if ((Board.A11.Text == Board.A22.Text) && (Board.A22.Text == s) && (Board.A00.Text == ""))
                return Board.A00;
            // Top right to bottom left
            else if ((Board.A02.Text == Board.A11.Text) && (Board.A11.Text == s) && (Board.A20.Text == ""))
                return Board.A20;
            else if ((Board.A20.Text == Board.A02.Text) && (Board.A02.Text == s) && (Board.A11.Text == ""))
                return Board.A11;
            else if ((Board.A11.Text == Board.A20.Text) && (Board.A20.Text == s) && (Board.A02.Text == ""))
                return Board.A02;
            // If no potential winner next turn
            else
                return null;
        }

        // When computers turn
        public Button MakeMove(ArrayList buttons)
        {
            Button b = null;
            b = TryWinOrDefend("O");
            if (b != null)
                return b;
            else
            {
                b = TryWinOrDefend("X");
                if (b != null)
                    return b;
                else
                    return MoveRandom(buttons);
            }
        }

        // Computer makes a random move
        private Button MoveRandom(ArrayList buttons)
        {
            Random r = new Random();
            return (Button)buttons[r.Next(buttons.Count - 1)];
        }
    }
}
