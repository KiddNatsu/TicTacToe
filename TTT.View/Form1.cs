using System;
using System.Collections;
using System.Threading;
using System.Windows.Forms;
using TTT.ViewModel;

namespace TTT.View
{
    public partial class MainForm : Form
    {
        ArrayList buttons = new ArrayList();
        Board board;
        User user;
        Computer computer;

        public MainForm()
        {
            InitializeComponent();
        }
        
        private void PopulateButtonArray()
        {
            buttons.Clear();
            buttons.Add(B00);
            buttons.Add(B01);
            buttons.Add(B02);
            buttons.Add(B10);
            buttons.Add(B11);
            buttons.Add(B12);
            buttons.Add(B20);
            buttons.Add(B21);
            buttons.Add(B22);
        }

        private void buttonClick(object sender, EventArgs e)
        {
            optionsGroupBox.Enabled = false;

            Button button = (Button)sender;
            if (button.Text == "")
            {
                buttons.Remove(button);

                // Board handles the turn
                board.PlayTurn(button);

                // TODO: Handle logic on another thread
                // Thread playerThread = new Thread(() => board.PlayTurn(button));
                // playerThread.Start();

                if ((singlePlayer.Checked) && (board.PlayersTurn is Computer))
                {
                    // If computers turn
                    // TODO: Same as above, logic on another thread
                    // Thread opponentThread = new Thread(() => computer.MakeMove(buttons));
                    // opponentThread.Start();
                    board.PlayTurn(computer.MakeMove(buttons));
                }
            }
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            PopulateButtonArray();
            InitialiseMembers();
            UpdateLabels();
        }

        private void InitialiseMembers()
        {
            board = new Board(buttons);
            user = new User(board, "X");
            computer = new Computer(board, "O");
            board.SetMembers(user, computer);
            board.NewGame += NewGame;
            board.WinLossOrDraw += GameWinLossOrDraw;
            board.SubscribeToTiles(TileValueChanged);
        }

        private void NewGameBtn_Click(object sender, EventArgs e)
        {
            board.OnNewGame();
        }

        private void NewGame(Object sender, EventArgs e)
        {
            PopulateButtonArray();
            board.AssignButtons(buttons);
            UpdateLabels();
            optionsGroupBox.Enabled = true;
        }

        private void UpdateLabels()
        {
            playerWins.Text = $"X (Player): { user.Points }";
            cpuWins.Text = $"O (Opponent): { computer.Points }";
            draws.Text = $"Draw: { board.Draw }";
        }

        private void ResetGame()
        {
            user.Points = 0;
            computer.Points = 0;
            board.Draw = 0;
            board.OnNewGame();
            optionsGroupBox.Enabled = true;

        }

        private void ResetBtn_Click(object sender, EventArgs e)
        {
            ResetGame();
        }

        private void GameWinLossOrDraw(object sender, WinLossOrDrawEventArgs e)
        {
            MessageBox.Show(e.Result);
        }

        private void TileValueChanged(object sender, EventArgs e)
        {
            Tile t = (Tile)sender;
            switch (t.RowColumn)
            {
                case "00":
                    B00.Text = t.Value;
                    break;
                case "01":
                    B01.Text = t.Value;
                    break;
                case "02":
                    B02.Text = t.Value;
                    break;
                case "10":
                    B10.Text = t.Value;
                    break;
                case "11":
                    B11.Text = t.Value;
                    break;
                case "12":
                    B12.Text = t.Value;
                    break;
                case "20":
                    B20.Text = t.Value;
                    break;
                case "21":
                    B21.Text = t.Value;
                    break;
                case "22":
                    B22.Text = t.Value;
                    break;
            }
        }
    }
}
