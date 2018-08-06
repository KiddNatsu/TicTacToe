using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TTT.ViewModel
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

        public ArrayList tiles = new ArrayList();
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
            SetTiles();
            AssignTiles();
            NewGame += StartNewGame;
        }

        public void SetMembers(User user, Computer computer)
        {
            this.user = user;
            this.cpu = computer;
            this.PlayersTurn = user;
        }

        public void AssignTiles()
        {
            tiles.Clear();
            tiles.Add(T00);
            tiles.Add(T01);
            tiles.Add(T02);
            tiles.Add(T10);
            tiles.Add(T11);
            tiles.Add(T12);
            tiles.Add(T20);
            tiles.Add(T21);
            tiles.Add(T22);
        }

        public void SetTiles()
        {
            T00 = new Tile("0", "0");
            T01 = new Tile("0", "1");
            T02 = new Tile("0", "2");
            T10 = new Tile("1", "0");
            T11 = new Tile("1", "1");
            T12 = new Tile("1", "2");
            T20 = new Tile("2", "0");
            T21 = new Tile("2", "1");
            T22 = new Tile("2", "2");
        }

        public void SubscribeToTiles(EventHandler eh)
        {
            T00.TileValueChanged += eh;
            T01.TileValueChanged += eh;
            T02.TileValueChanged += eh;
            T10.TileValueChanged += eh;
            T11.TileValueChanged += eh;
            T12.TileValueChanged += eh;
            T20.TileValueChanged += eh;
            T21.TileValueChanged += eh;
            T22.TileValueChanged += eh;
        }

        public void OnNewGame()
        {
            NewGame?.Invoke(this, EventArgs.Empty);
        }

        private void StartNewGame(Object sender, EventArgs e)
        {
            PlayersTurn = user;
            Turns = 0;
            T00.Value = T01.Value = T02.Value = T10.Value = T11.Value = T12.Value = T20.Value = T21.Value = T22.Value = "";
            AssignTiles();
        }

        public void OnWinLossOrDraw(string result)
        {
            // Short hand: if event 'WinLossOrDraw' is not null, invoke it
            WinLossOrDraw?.Invoke(this, new WinLossOrDrawEventArgs(result));
        }

        // When board tile clicked
        // Change to pass row and column which will allow find the right tile
        public void PlayTurn(string tile)
        {
            Turns++;
            Tile t = SelectTile(tile);
            tiles.Remove(t);
            // Pass the correct tile to player to make move
            PlayersTurn.MakeMove(t);

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

        // Depending on the button clicked, corrosponding tile should be used
        private Tile SelectTile(string tileName)
        {
            switch (tileName)
            {
                case "B00":
                    return T00;
                case "B01":
                    return T01;
                case "B02":
                    return T02;
                case "B10":
                    return T10;
                case "B11":
                    return T11;
                case "B12":
                    return T12;
                case "B20":
                    return T20;
                case "B21":
                    return T21;
                case "B22":
                    return T22;
                default:
                    return null;
            }
        }

        private bool CheckWinner()
        {
            // Horizontal checks
            if ((T00.Value == T01.Value) && (T01.Value == T02.Value) && (T02.Value != ""))
                return true;
            else if ((T10.Value == T11.Value) && (T11.Value == T12.Value) && (T12.Value != ""))
                return true;
            else if ((T20.Value == T21.Value) && (T21.Value == T22.Value) && (T22.Value != ""))
                return true;

            // Verticle checks
            if ((T00.Value == T10.Value) && (T10.Value == T20.Value) && (T20.Value != ""))
                return true;
            else if ((T01.Value == T11.Value) && (T11.Value == T21.Value) && (T21.Value != ""))
                return true;
            else if ((T02.Value == T12.Value) && (T12.Value == T22.Value) && (T22.Value != ""))
                return true;

            // Diagonal checks
            if ((T00.Value == T11.Value) && (T11.Value == T22.Value) && (T22.Value != ""))
                return true;
            else if ((T02.Value == T11.Value) && (T11.Value == T20.Value) && (T20.Value != ""))
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
