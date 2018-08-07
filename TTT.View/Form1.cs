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
        Thread mainThread;

        // Redirect to main thread delegates
        private delegate void UpdateTilesOnMainThread(object sender, EventArgs e);
        private delegate void StartNewGameOnMainThread(object sender, EventArgs e);

        public MainForm()
        {
            InitializeComponent();
        }

        private void buttonClick(object sender, EventArgs e)
        {
            optionsGroupBox.Enabled = false;

            Button button = (Button)sender;
            if (button.Text == "")
            {
                // TODO: Handle logic on another thread
                Thread playerThread = new Thread(() => board.PlayTurn(button.Name));
                playerThread.Start();

                // Board handles the turn
                // Uncomment if running on single thread
                //board.PlayTurn(button.Name);

                playerThread.Join();
                if ((singlePlayer.Checked) && (board.PlayersTurn is Computer))
                {
                    // If computers turn
                    // TODO: Same as above, logic on another thread
                    Thread opponentThread = new Thread(() => board.PlayTurn(computer.MakeMove()));
                    opponentThread.Start();

                    // Uncomment if running on single thread
                    //board.PlayTurn(computer.MakeMove());
                }
            }
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
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
            mainThread = Thread.CurrentThread;
        }

        private void NewGameBtn_Click(object sender, EventArgs e)
        {
            board.OnNewGame();
        }

        private void NewGame(Object sender, EventArgs e)
        {
            // If worker thread attempting to start new game, return
            if (!EnsureMainThread(new StartNewGameOnMainThread(NewGame), sender, e))
            {
                return;
            }
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

        // Subscriber to changes in tiles from the ViewModel
        private void TileValueChanged(object sender, EventArgs e)
        {

            Tile t = (Tile)sender;

            // If worker thread attempting to change values, rediect to main thread
            if (!EnsureMainThread(new UpdateTilesOnMainThread(TileValueChanged), sender, e))
            {
                return;
            }

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

        // Make sure updating UI operations only run on main UI thread
        private bool EnsureMainThread(Delegate del, Object sender, EventArgs e)
        {
            if (Thread.CurrentThread != mainThread)
            {
                this.BeginInvoke(del, new object[] { sender, e });
                return false;
            }
            return true;
        }
    }
}
