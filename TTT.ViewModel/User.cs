using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTT.ViewModel
{
    public class User : Player
    {

        public User(Board board, string key)
        {
            this.Board = board;
            this.Key = key;
            this.Name = "Player " + key; // Key = X or O (e.g. Player X)
        }

    }
}
