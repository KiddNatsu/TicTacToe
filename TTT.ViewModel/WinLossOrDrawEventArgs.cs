using System;

namespace TTT.LOGIC
{
    public class WinLossOrDrawEventArgs : EventArgs
    {
        public string Result { get; set; }

        public WinLossOrDrawEventArgs(string result)
        {
            this.Result = result;
        }
    }
}