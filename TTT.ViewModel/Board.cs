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
        public int Draw { get; set; } = 0;

        // Hold players on board
        public Player PlayersTurn { get; set; }// Hold player whos turn it is
        private User user;
        private Computer cpu;

        public event EventHandler NewGame;
        public event EventHandler<WinLossOrDrawEventArgs> WinLossOrDraw;

        // Each button represents a tile on the board
        // A00 = top left corner, A22 = bottom right corner
        public ArrayList tiles = new ArrayList();
        internal Button A00;
        internal Button A01;
        internal Button A02;
        internal Button A10;
        internal Button A11;
        internal Button A12;
        internal Button A20;
        internal Button A21;
        internal Button A22;

        // Instead of buttons, tiles represents board
        internal Tile T00;
        internal Tile T01;
        internal Tile T02;
        internal Tile T10;
        internal Tile T11;
        internal Tile T12;
        internal Tile T20;
        internal Tile T21;
        internal Tile T22;

        public Board(ArrayList buttons)
        {
            AssignButtons(buttons);
            NewGame += StartNewGame;
        }

        public void SetMembers(User user, Computer computer)
        {
            this.user = user;
            this.cpu = computer;
            this.PlayersTurn = user;
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
            PlayersTurn = user;
            Turns = 0;
            A00.Text = A01.Text = A02.Text = A10.Text = A11.Text = A12.Text = A20.Text = A21.Text = A22.Text = "";
        }

        public void OnWinLossOrDraw(string result)
        {
            // Short hand: if event 'WinLossOrDraw' is not null, invoke it
            WinLossOrDraw?.Invoke(this, new WinLossOrDrawEventArgs(result));
        }

        // When board tile clicked
        public void PlayTurn(Button tile)
        {
            Turns++;
            tiles.Remove(tile);
            PlayersTurn.MakeMove(tile);

            if (CheckWinner() == true)
            {
                OnWinLossOrDraw(PlayersTurn.ToString());
                PlayersTurn.Points++;
                OnNewGame();
                return;
            }

            if ((CheckDraw() == true) && (CheckWinner() == false))
            {
                OnWinLossOrDraw("Draw!");
                Draw++;
                OnNewGame();
                return;
            }

            // Change who's turn it is
            if (PlayersTurn == user)
                PlayersTurn = cpu;
            else
                PlayersTurn = user;

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
