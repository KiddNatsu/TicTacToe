using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TTT.LOGIC
{
    public class Board
    {
        // Count turns
        public int Turns { get; set; } = 0;

        // Keep track of player wins and draws
        public int P1 { get; set; } = 0;
        public int P2 { get; set; } = 0;
        public int Draw { get; set; } = 0;

        // True = player turn, false = CPU turn
        public bool OnTurn { get; set; } = true;

        public event EventHandler NewGame;
        public event EventHandler<WinLossOrDrawEventArgs> WinLossOrDraw;

        // Each button represents a tile on the board
        // A00 = top left corner, A22 = bottom right corner
        private ArrayList tiles = new ArrayList();
        internal Button A00;
        internal Button A01;
        internal Button A02;
        internal Button A10;
        internal Button A11;
        internal Button A12;
        internal Button A20;
        internal Button A21;
        internal Button A22;

        public Board(ArrayList buttons)
        {
            AssignButtons(buttons);
            NewGame += StartNewGame;
        }

        public void AssignButtons(ArrayList buttons)
        {
            tiles.Clear();
            A00 = (Button)buttons[0];
            A01 = (Button)buttons[1];
            A02 = (Button)buttons[2];
            A10 = (Button)buttons[3];
            A11 = (Button)buttons[4];
            A12 = (Button)buttons[5];
            A20 = (Button)buttons[6];
            A21 = (Button)buttons[7];
            A22 = (Button)buttons[8];
            tiles.Add(A00);
            tiles.Add(A01);
            tiles.Add(A02);
            tiles.Add(A10);
            tiles.Add(A11);
            tiles.Add(A12);
            tiles.Add(A20);
            tiles.Add(A21);
            tiles.Add(A22);
        }

        public void OnNewGame()
        {
            NewGame?.Invoke(this, EventArgs.Empty);
        }

        private void StartNewGame(Object sender, EventArgs e)
        {
            OnTurn = true;
            Turns = 0;
            A00.Text = A01.Text = A02.Text = A10.Text = A11.Text = A12.Text = A20.Text = A21.Text = A22.Text = "";
        }

        public void OnWinLossOrDraw(string result)
        {
            WinLossOrDraw?.Invoke(this, new WinLossOrDrawEventArgs(result));
        }

        public void PlayTurn(Button tile)
        {
            tiles.Remove(tile);

            if (tile.Text == "")
            {
      
                if (OnTurn)
                    tile.Text = "X";
                else
                    tile.Text = "O";

                Turns++;
                OnTurn = !OnTurn;

                if (CheckWinner() == true)
                {
                    if (OnTurn == false)
                    {
                        OnWinLossOrDraw("You win!");
                        P1++;
                        OnNewGame();
                    }
                    else
                    {
                        OnWinLossOrDraw("CPU win!");
                        P2++;
                        OnNewGame();
                    }
                }

                if ((CheckDraw() == true) && (CheckWinner() == false))
                {
                    OnWinLossOrDraw("Draw!");
                    Draw++;
                    OnNewGame();
                }
            }
        }

        private bool CheckWinner()
        {
            // Horizontal checks
            if ((A00.Text == A01.Text) && (A01.Text == A02.Text) && (A02.Text != ""))
                return true;
            else if ((A10.Text == A11.Text) && (A11.Text == A12.Text) && (A12.Text != ""))
                return true;
            else if ((A20.Text == A21.Text) && (A21.Text == A22.Text) && (A22.Text != ""))
                return true;

            // Verticle checks
            if ((A00.Text == A10.Text) && (A10.Text == A20.Text) && (A20.Text != ""))
                return true;
            else if ((A01.Text == A11.Text) && (A11.Text == A21.Text) && (A21.Text != ""))
                return true;
            else if ((A02.Text == A12.Text) && (A12.Text == A22.Text) && (A22.Text != ""))
                return true;

            // Diagonal checks
            if ((A00.Text == A11.Text) && (A11.Text == A22.Text) && (A22.Text != ""))
                return true;
            else if ((A02.Text == A11.Text) && (A11.Text == A20.Text) && (A20.Text != ""))
                return true;

            // If no matches
            return false;
        }

        public bool CheckDraw()
        {
            if (Turns == 9)
                return true;
            else
                return false;
        }
    }
}
