using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTT.ViewModel
{
    public class Tile
    {
        public event EventHandler TileValueChanged;

        public string RowColumn { get; set; }
        public string Value
        {
            get
            {
                return Value;
            }
            set
            {
                this.Value = value;
                // trigger event
                // if not empty, invoke event                
                TileValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public Tile(string row, string column)
        {
            this.RowColumn = row + column;
        }
    }
}
