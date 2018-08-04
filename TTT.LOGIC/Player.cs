using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTT.LOGIC
{
    public class Player
    {
        // base class for all players (computer, user)
        public int Points { get; set; }
        protected Board Board { get; set; }
    }
}
