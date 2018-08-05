using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTT.ViewModel
{
    class TileChangedEventArgs : EventArgs
    {
        public string RowColumn { get; set; }

        public TileChangedEventArgs(string rowColumn)
        {
            this.RowColumn = rowColumn;
        }
    }
}
