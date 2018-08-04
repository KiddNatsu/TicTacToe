using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TTT.LOGIC;

namespace TTT.VM
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
            buttons.Add(A00);
            buttons.Add(A01);
            buttons.Add(A02);
            buttons.Add(A10);
            buttons.Add(A11);
            buttons.Add(A12);
            buttons.Add(A20);
            buttons.Add(A21);
            buttons.Add(A22);
        }

        private void buttonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            buttons.Remove(button);

            // Board handles the turn
            board.PlayTurn(button);

            // If computers turn
            if (!board.OnTurn)
            {
                board.PlayTurn(computer.MakeMove(buttons));
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
            user = new User(board);
            computer = new Computer(board);
            board.NewGame += NewGame;
            board.WinLossOrDraw += GameWinLossOrDraw;
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
        }

        private void UpdateLabels()
        {
            playerWins.Text = $"X (Player): { board.P1 }";
            cpuWins.Text = $"O (Opponent): { board.P2 }";
            draws.Text = $"Draw: { board.Draw }";
        }

        private void ResetGame()
        {
            board.P1 = 0;
            board.P2 = 0;
            board.Draw = 0;
            board.OnNewGame();
        }

        private void ResetBtn_Click(object sender, EventArgs e)
        {
            ResetGame();
        }

        private void GameWinLossOrDraw(object sender, WinLossOrDrawEventArgs e)
        {
            MessageBox.Show(e.Result);
        }
    }
}
