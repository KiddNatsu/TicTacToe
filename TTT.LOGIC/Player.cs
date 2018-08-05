using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TTT.LOGIC
{
    public class Player
    {
        // base class for all players (computer, user)
        public int Points { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        protected Board Board { get; set; }

        public void MakeMove(Button tile)
        {
            tile.Text = Key;
        }

        public override string ToString()
        {
            return (this.Name + " wins!");
        }
    }
        
}
